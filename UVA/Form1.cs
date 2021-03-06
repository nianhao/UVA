﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UVA
{
    public partial class Form1 : Form
    {
        static Semaphore sem = new Semaphore(1, 1);
        static Semaphore fileSem = new Semaphore(1, 1);
        //视频接收线程
        /// <summary>
        /// 视频接收线程（目前只能处理一个，将其设置为一个容器，可以接收多个）
        /// </summary>
        private Thread videoReceiveThread;
        //监听的端口和ip，与Gloable相同
        /// <summary>
        /// 调度服务器端口，从Global类中读取
        /// </summary>
        public int port { get; private set; }
        /// <summary>
        /// 调度服务器的ip，从Global类中读取
        /// </summary>
        public string ip { get; private set; }
        /// <summary>
        /// 调度服务器point
        /// </summary>
        public IPEndPoint point { get; private set; }
        /// <summary>
        /// 调度UDPClient，接收UDP消息
        /// </summary>
        public UdpClient dispatchUDPClient { get; private set; }
        //调度线程
        /// <summary>
        /// 调度线程，用于处理接收到的不同参数
        /// </summary>
        public Thread dispatchThread { get; private set; }
        //声明跨线程操作的委托
        //设置日志信息
        private delegate void setSysLog_CALLBACK(string logInfo);
        /// <summary>
        /// 向界面上写入日志信息的回调函数
        /// </summary>
        private setSysLog_CALLBACK setSysLogCallBack;
        //增加无人机上线信息
        private delegate void modifyUVA_CALLBACK(UvaEntity newUVA,bool add);
        /// <summary>
        /// 无人机信息改变后，界面的回调函数
        /// </summary>
        private modifyUVA_CALLBACK modifyUVACallBack;
        /// <summary>
        /// 委托，更新界面的GPS信息
        /// </summary>
        /// <param name="GPS"></param>
        private delegate void updateGPS_CALLBACK(string GPS);
        private updateGPS_CALLBACK updateGPSCAllBack;
        //存储在线的全部无人机,考虑到线程安全和效率问题，使用hashTable
        /// <summary>
        /// 存放系统中保存过的无人机实体
        /// </summary>
        public Hashtable allUVA = Hashtable.Synchronized(new Hashtable());
        /// <summary>
        /// 存放所有的panel-handle映射
        /// </summary>
        public Hashtable allPanel = Hashtable.Synchronized(new Hashtable());
        /// <summary>
        /// 存放所有的panel-UVA映射
        /// </summary>
        public Hashtable panel_UVA = Hashtable.Synchronized(new Hashtable());
        /// <summary>
        /// 展示链路状态的类实例
        /// </summary>
        public linkInfo linkInfoBoard; 

        Size _beforeDialogSize;
        /// <summary>
        /// 构造函数
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            _beforeDialogSize = this.Size;
            InitSys();
            //设置按钮的颜色          this.button4.ForeColor = Control.DefaultForeColor;
            this.button4.BackColor = Control.DefaultBackColor;
            //test.test_copy();
        }
        /// <summary>
        /// 初始化整个系统
        /// </summary>
        public void InitSys()
        {
            //初始化系统
            setSysLog(Global.SystemInfo);
            linkInfoBoard= new linkInfo();

            /*try
            {
                axVLCPlugin21.playlist.add("file:///E:/Workspaces/VisualStudio/assets/test.h264");


                axVLCPlugin21.playlist.play();

            }
            catch (Exception e)
            {

                Trace.WriteLine(e.ToString());
            }*/
            //获取全部的panel
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Panel)
                {
                    //相关操作代码
                    Trace.WriteLine(ctrl.Name);
                    allPanel[ctrl.Name] = ctrl;
                }

            }
        }
        /// <summary>
        /// 设置系统日志信息
        /// </summary>
        /// <param name="info"></param>
        public void setSysLog(String info)
        {
            String nowString= DateTime.Now.ToLocalTime().ToString();
            textBox_sysLog.AppendText(nowString+"\r\n"+info+"\r\n");
        }
        public void setSysLog(byte[] info)
        {
            String nowString = DateTime.Now.ToLocalTime().ToString();
            textBox_sysLog.AppendText("\r\n"+nowString +" 总长度为" + info.Length + "\r\n");
            textBox_sysLog.AppendText(info[0].ToString("X2")+" "+info[1].ToString("X2") + " "+info[2].ToString("X2") + " "+info[3].ToString("X2") + " " + "\r\n");
        }
        /// <summary>
        /// 修改界面上，在线无人机的回调
        /// </summary>
        /// <param name="UVAInfo">无人机的信息编号</param>
        /// <param name="add">如果为true，则是增加这条记录，否则是删除记录</param>
        public void modifyUVA(UvaEntity newUVA,bool add=true)
        {
            if (add)
            {
                string uvaName = newUVA.uvaName;
                this.comboBox_allUVA.Items.Add(uvaName);
                int uvaNum = allUVA.Count;
                this.label_uvaNum.Text = Convert.ToString(uvaNum);
                newUVA.setVLCPlayer();
                //绑定窗体并播放
                newUVA.setRenderWindow(allPanel, panel_UVA);
                newUVA.vlcPlayer.Play();
                //发送ready信息 
                newUVA.sendReady();
                this.comboBox_allUVA.SelectedIndex = this.comboBox_allUVA.FindString(uvaName);
            }
            else
            {
                //执行无人机下线操作
                newUVA.logOut();
                //在全部在线的无人机中删掉下线的无人机
                allUVA.Remove(newUVA.id);
                //释放panel
                panel_UVA.Remove(newUVA.panelName);
                //更改界面信息
                string uvaName = newUVA.uvaName;
                this.comboBox_allUVA.Items.Remove(uvaName);          
                int uvaNum = allUVA.Count;
                //如果删除了最后一条项目，则清除一下combbox
                if(uvaNum==0)
                {
                    this.comboBox_allUVA.Text = "";
                }
                this.label_uvaNum.Text = Convert.ToString(uvaNum);
                //检查panel是否需要更换
                if(Global.MAIN_PANEL==newUVA.panelName)
                {
                    int onlineUVANum = comboBox_allUVA.Items.Count;
                    if (onlineUVANum > 0)
                    {
                        string lastUVAName = comboBox_allUVA.Items[onlineUVANum - 1].ToString();

                        foreach (UvaEntity tmpUVA in allUVA.Values)
                        {
                            if (tmpUVA.uvaName == lastUVAName)
                            {
                                changePanel(newUVA.panelName, tmpUVA.panelName);
                                this.comboBox_allUVA.SelectedIndex = this.comboBox_allUVA.FindString(tmpUVA.uvaName);
                                Trace.WriteLine(string.Format("{0}下线，{1}替换到大图", newUVA.uvaName, tmpUVA.uvaName));
                                break;
                            }
                        }

                    }
                    else
                    {
                        Trace.WriteLine("全部无人机都下线了");
                    }
                    //Trace.WriteLine(lastUVAName);
                }
                
            }
        }
        private void updateGPS(string gps)
        {
            this.labelGPS.Text = gps;
        }
        /// <summary>
        /// 开启UVA监听连接。在这里实例化跨线程操作，开启调度线程
        /// </summary>
        public void startListenUVAConnection()
        {
            try
            {
                //获取信息接收端口
                port = Global.CONNECTION_PORT;
                //获取监听IP
                ip = Global.CONNECTION_IP;
                //创建节点
                point = new IPEndPoint(IPAddress.Parse(ip), port);
                //实例化调度UDP服务器
                dispatchUDPClient = new UdpClient(point);
                //实例化日志信息设置回调
                setSysLogCallBack = new setSysLog_CALLBACK(setSysLog);
                //实例化修改无人机在线信息
                modifyUVACallBack = new modifyUVA_CALLBACK(modifyUVA);
                //实例化GPS更新信息
                updateGPSCAllBack = new updateGPS_CALLBACK(updateGPS);
                //开启UDP监听
                dispatchThread = new Thread(new ParameterizedThreadStart(dispatchLoop));
                dispatchThread.IsBackground = true;
                dispatchThread.Start(dispatchUDPClient);
                //return true;
            }
            catch (Exception ee)
            {

                Trace.Write(ee.Message);
                MessageBox.Show("请不要重复点击开始按钮");
            }

           

        }
        /// <summary>
        /// 调度循环，用于接收命令，完成调度。
        /// </summary>
        /// <param name="obj"></param>
        private void dispatchLoop(object obj)
        {
            //throw new NotImplementedException();
            UdpClient dispatch = obj as UdpClient;
            BytesManager bmanager = null;
#if DEBUG
            textBox_sysLog.Invoke(setSysLogCallBack, "启动成功");
#endif            
            while (true)
            {
                //阻塞，只到接收到消息
                Byte[] receiveBytes = null;// dispatch.Receive(ref RemoteIpEndPoint);
                string receiveData = null;// Encoding.UTF8.GetString(receiveBytes);

                //允许接收任意远端发送的消息
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                try
                {
                    //阻塞，只到接收到消息
                    receiveBytes = dispatch.Receive(ref RemoteIpEndPoint);
                    bmanager = new BytesManager(receiveBytes);
                    receiveData = Encoding.UTF8.GetString(receiveBytes);
                }
                catch (Exception e)
                {

                    Trace.WriteLine(e.ToString());
                    Trace.WriteLine(e.Message);
                    continue;
                }

                //输出debug信息
                //Trace.WriteLine("接收到消息："+receiveData);
#if DEBUG
                textBox_sysLog.Invoke(setSysLogCallBack,("接收到来自"+RemoteIpEndPoint.ToString()+"消息：" +receiveBytes));
                //textBox_sysLog.Invoke(setSysLogCallBack, (receiveBytes));
#endif
                string[] commands = receiveData.Split(';');
                if(bmanager.msgForm==(int)Global.msgFromType.uva|| bmanager.msgForm == (int)Global.msgFromType.helmet)
                {
                    switch (bmanager.uvaMsg.sendType)
                    {
                        case '\u0001':
                            {
#if DEBUG
                                textBox_sysLog.Invoke(setSysLogCallBack, ("解析到 begin 命令"));

#endif
                                //获取无人机id
                                int uvaId = bmanager.uvaMsg.cliNum;
                                //检查是否重复连接
                                if (allUVA.ContainsKey(uvaId))
                                {
                                    string sendString = "error;DuplicateConnection;";
                                    //Byte[] sendBytes = Encoding.UTF8.GetBytes(sendString);
                                    UvaEntity tmpUva = allUVA[uvaId] as UvaEntity;
                                    Byte[] sendBytes = Command.DuplicateConnection(tmpUva.videoIp,tmpUva.videoPort);
                                    try
                                    {
                                        dispatch.Send(sendBytes, sendBytes.Length, RemoteIpEndPoint);
                                        textBox_sysLog.Invoke(setSysLogCallBack, string.Format("向{0}:{1},{2}号无人机发送错误信息{3}", RemoteIpEndPoint.Address, RemoteIpEndPoint.Port, uvaId, sendString));

                                    }
                                    catch (Exception e)
                                    {
#if DEBUG
                                        textBox_sysLog.Invoke(setSysLogCallBack, e.ToString());
#endif


                                        textBox_sysLog.Invoke(setSysLogCallBack, string.Format("向{0}:{1},{2}号无人机发送错误信息失败", RemoteIpEndPoint.Address, RemoteIpEndPoint.Port, uvaId));
                                    }
                                    break;

                                }
                                //接收到连接信息，输出日志信息
                                textBox_sysLog.Invoke(setSysLogCallBack, ("接收到来自" + RemoteIpEndPoint + "的连接请求"));
                                //为新的UVA分配接收视频信息的udpClient
                                int restTimes = Global.MAX_RETRY_TIMES;
                                while (true)
                                {
                                    if (restTimes > 0)
                                    {
#if DEBUG
                                        textBox_sysLog.Invoke(setSysLogCallBack, ("正在为" + RemoteIpEndPoint.Address + ":" + RemoteIpEndPoint.Port + "分配视频接收服务器"));
                                        textBox_sysLog.Invoke(setSysLogCallBack, (string.Format("第{0}/{1}次尝试", Global.MAX_RETRY_TIMES - restTimes + 1, Global.MAX_RETRY_TIMES)));
#endif
                                    }
                                    else
                                    {
                                        textBox_sysLog.Invoke(setSysLogCallBack, (string.Format("为{0}:{1}分配视频接收服务器失败，系统资源不足", RemoteIpEndPoint.Address, RemoteIpEndPoint.Port)));
                                        break;
                                    }
                                    restTimes--;
                                    //获取视频接收服务器的ip地址
                                    string video_receive_ip = Global.RECEIVE_VIDEO_SERVER;
                                    //生成端口，检查是否可用
                                    System.Random a = new Random(System.DateTime.Now.Millisecond); // use System.DateTime.Now.Millisecond as seed
                                    int RandKey = a.Next(Global.MINPORT, Global.MAXPORT);
#if DEBUG
                                    textBox_sysLog.Invoke(setSysLogCallBack, string.Format("为{0}:{1}尝试分配端口{2}", RemoteIpEndPoint.Address, RemoteIpEndPoint.Port, RandKey));
#endif
                                    try
                                    {
                                        //分配端口成功，开启新的线程，接收视频信息
                                        UdpClient videoReceiveUDPClient = new UdpClient(new IPEndPoint(IPAddress.Parse("0.0.0.0"), RandKey));
                                        videoReceiveUDPClient.Close();
                                        //记录无人机
                                        UvaEntity tmpUVA = new UvaEntity(RemoteIpEndPoint.Address.ToString(), RemoteIpEndPoint.Port, bmanager.uvaMsg.cliNum, video_receive_ip, RandKey,RemoteIpEndPoint);
                                        allUVA.Add(uvaId, tmpUVA);
                                        //开启UDP视频接收线程
                                        //videoReceiveThread = new Thread(new ParameterizedThreadStart(videoReceiveLoop));
                                        //videoReceiveThread.IsBackground = true;
                                        //videoReceiveThread.Start(tmpUVA);
                                        //暂停0.5秒，等待allUVA更新
                                        //System.Threading.Thread.Sleep(500);
                                        //分配成功，输出信息
                                        textBox_sysLog.Invoke(setSysLogCallBack, (string.Format("为{0}:{1}分配视频接收服务器成功，视频接收地址为{2}:{3}", RemoteIpEndPoint.Address, RemoteIpEndPoint.Port, video_receive_ip, RandKey)));
                                        //在全部无人机combbox里面添加一项
                                        comboBox_allUVA.Invoke(modifyUVACallBack, tmpUVA, true);
                                        break;
                                    }
                                    catch (Exception e)
                                    {
                                        Trace.WriteLine(e.StackTrace);
                                        textBox_sysLog.Invoke(setSysLogCallBack, e.StackTrace.ToString());
                                        textBox_sysLog.Invoke(setSysLogCallBack, e.Message.ToString());
                                    }
                                }
                                break;
                            }

                        case '\u0003':
                        case '\u0004':
                            {
#if DEBUG
                                textBox_sysLog.Invoke(setSysLogCallBack, string.Format("接收到{0}信息",bmanager.uvaMsg.sendType=='\u0003'?"end":"ok"));
#endif
                                //获取无人机id
                                //int uvaId = Convert.ToInt32(commands[2]);
                                //获取无人机id
                                int uvaId = bmanager.uvaMsg.cliNum;
                                //检查是否重复注销
                                if (!allUVA.ContainsKey(uvaId))
                                {
                                    string sendString = "error;DuplicatedeDisconnect;";
                                    //Byte[] sendBytes = Encoding.UTF8.GetBytes(sendString);
                                    Byte[] sendBytes = Command.Error(Global.DuplicatedeDisconnect);
                                    try
                                    {
                                        dispatch.Send(sendBytes, sendBytes.Length, RemoteIpEndPoint);
                                        textBox_sysLog.Invoke(setSysLogCallBack, string.Format("向{0}:{1},{2}号无人机发送错误信息{3}", RemoteIpEndPoint.Address, RemoteIpEndPoint.Port, uvaId, sendString));

                                    }
                                    catch (Exception e)
                                    {
#if DEBUG
                                        textBox_sysLog.Invoke(setSysLogCallBack, e.ToString());
#endif


                                        textBox_sysLog.Invoke(setSysLogCallBack, string.Format("向{0}:{1},{2}号无人机发送错误信息失败", RemoteIpEndPoint.Address, RemoteIpEndPoint.Port, uvaId));
                                    }
                                    break;

                                }
                                //获取无人机实例，进行销毁操作
                                try
                                {
                                    UvaEntity tmpUva = allUVA[uvaId] as UvaEntity;
                                    comboBox_allUVA.Invoke(modifyUVACallBack, tmpUva, false);
                                }
                                catch (Exception ee)
                                {

                                    Trace.WriteLine(ee.Message);
                                }
                                
                                break;
                            }
                        //收到心跳信息
                        case '\u0002':
                            {
                                int id = -1;
                                string type = null;
                                string info = null;
                                try
                                {
                                    //获取设备的id号
                                    //id = Convert.ToInt32(commands[2]);
                                    id = bmanager.uvaMsg.cliNum;
                                    //获取设备类型
                                    type = bmanager.uvaMsg.cliType=='\u0001'?"uva": "helmet";
                                    //type = commands[1];
                                    //info = commands[3];
                                    Trace.WriteLine("心跳命令解析完毕" + type + id.ToString() );
                                    //string[] infos = info.Split('&');
                                    if (!allUVA.Contains(id))
                                    {
                                        Trace.WriteLine(string.Format("{0} {1}已经下线", type, id.ToString()));
                                        break;
                                    }
                                    string x, y, heartTime;
                                    try
                                    {
                                        //x = infos[0];
                                        //y = infos[1];
                                        //heartTime = infos[2];
                                        x = string.Format("{0}_{1}_{2}", bmanager.uvaMsg.latDeg, bmanager.uvaMsg.latMin, bmanager.uvaMsg.latSec);
                                        y = string.Format("{0}_{1}_{2}", bmanager.uvaMsg.lonDeg, bmanager.uvaMsg.lonMin, bmanager.uvaMsg.lonSec);
                                        heartTime = string.Format("{0}_{1}_{2}", bmanager.uvaMsg.hour, bmanager.uvaMsg.minute, bmanager.uvaMsg.second);
                                        UvaEntity tmpUVA = allUVA[id] as UvaEntity;
                                        //tmpUVA.receiveHeartAsync(x, y, heartTime);
                                        byte[] sendBytes = Command.HeratResponse(tmpUVA);
                                        string gpsString = string.Format("{0}{1}{2}{3},{4}{5}{6}{7}",
                                            bmanager.uvaMsg.latDeg, bmanager.uvaMsg.latMin, bmanager.uvaMsg.latSec, bmanager.uvaMsg.latDir,
                                            bmanager.uvaMsg.lonDeg, bmanager.uvaMsg.lonMin, bmanager.uvaMsg.lonSec, bmanager.uvaMsg.lonDir);
                                        dispatch.Send(sendBytes, sendBytes.Length,RemoteIpEndPoint);
#if DEBUG
                                        Trace.WriteLine(string.Format("Terminal {8} GPS Info:\r\n{0}{1}{2}{3},{4}{5}{6}{7}\r\n",
                                            bmanager.uvaMsg.latDeg, bmanager.uvaMsg.latMin, bmanager.uvaMsg.latSec, bmanager.uvaMsg.latDir,
                                            bmanager.uvaMsg.lonDeg, bmanager.uvaMsg.lonMin, bmanager.uvaMsg.lonSec, bmanager.uvaMsg.lonDir,
                                            id));
                                        textBox_sysLog.Invoke(setSysLogCallBack, string.Format("解析到心跳命令\r\n" +
                                            "终端 {0} GPS:{1}\r\n" +
                                            "时间 {2}",id,gpsString,heartTime));
#endif
                                        if ((allUVA[id] as UvaEntity).panelName==Global.MAIN_PANEL )
                                        {
                                            labelGPS.Invoke(updateGPSCAllBack, string.Format("{0}{1}{2}{3},{4}{5}{6}{7}",
                                            bmanager.uvaMsg.latDeg, bmanager.uvaMsg.latMin, bmanager.uvaMsg.latSec, bmanager.uvaMsg.latDir,
                                            bmanager.uvaMsg.lonDeg, bmanager.uvaMsg.lonMin, bmanager.uvaMsg.lonSec, bmanager.uvaMsg.lonDir));
                                        }
                                        
                                    }
                                    catch (Exception e)
                                    {
                                        Trace.WriteLine("info解析失败");
                                        Trace.WriteLine(e.StackTrace);
                                        Trace.WriteLine(e.Message);
#if DEBUG
                                        textBox_sysLog.Invoke(setSysLogCallBack, e.Message);
#endif
                                        //throw;
                                    }

                                }
                                catch (Exception e)
                                {
                                    Trace.WriteLine(e.StackTrace);
                                    Trace.WriteLine(e.Message);

                                    //throw;
                                }
                                break;
                            }
                        default:
                            break;
                    }
                }
                else if(bmanager.msgForm==(int)Global.msgFromType.linkInfo)
                {
                    linkInfoBoard.setByLinkInfoMsg(bmanager);
                }
                

            }
        }
        /// <summary>
        /// 视频接收，循环接收视频信息，并保存
        /// </summary>
        /// <param name="obj">UDPclient 用于接收无人机传输视频</param>
        /*private void videoReceiveLoop(object obj)
        {
            //获取无人机实例
            UvaEntity uvaClient = obj as UvaEntity;
            //获取文件写入名
            String savaFileName = Global.getSavaFileName(uvaClient);
            uvaClient.videoFileName = savaFileName;
            
            videoReceiveThread.IsBackground = true;
             在这个线程中写入文件，会使得文件无法实时更新，而无法播放
            FileStream savaFileStream = null;
            try
            {
                savaFileStream = new FileStream(savaFileName, FileMode.Create);
            }
            catch (Exception e)
            {

                //throw;
                Trace.WriteLine(e.ToString());
            }
            int sum = 0;
            //设置定时器，用于释放资源
            System.Threading.Timer Timer_deleteVideoReceiveLoop = new System.Threading.Timer(deleteVideoReceiveLoop, null, 0, 5000);
            while (true)
            {
                Byte[] receiveBytes;
                string receiveData;
                if(!uvaClient.fileIsWriting && uvaClient.getActivateTmpVideoQueue().Count>10)
                {
                    try
                    {
                        //视频写入线程
                        Thread writeToFileThread = new Thread(new ParameterizedThreadStart(sava2FileThread));
                        uvaClient.fileIsWriting = true;
                        uvaClient.changeIndex();
                        writeToFileThread.Start(uvaClient);
                    }
                    catch (Exception e)
                    {

                        Trace.WriteLine("写入文件线程开启失败"+e.ToString());
                    }
                    
                }
                //允许接收任意远端发送的消息
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                //阻塞，只到接收到消息
                try
                {
                    //输出收到的消息总条数
                    Trace.WriteLine("总计收到" + sum.ToString());
                    receiveBytes = uvaClient.videoReceiveClient.Receive(ref RemoteIpEndPoint);
                    //写入缓存队列
                    uvaClient.addVideSegment(receiveBytes);
                    //savaFileStream.Write(receiveBytes, 0, receiveBytes.Length);
                    sum += receiveBytes.Length;
                    //savaFileStream.Flush();
                    receiveData = Encoding.UTF8.GetString(receiveBytes);
                    //输出debug信息
                    //Trace.WriteLine("接收到消息："+receiveData);

#if DEBUG
                    textBox_sysLog.Invoke(setSysLogCallBack, ("接收到来自" + RemoteIpEndPoint.ToString() + "消息：" + receiveData));
#endif
                }
                catch (Exception e)
                {

                    //throw;
                    Trace.WriteLine(e.ToString());
                    continue;
                }
                

            }
        }*/

        /// <summary>
        /// 将缓存队列中的视频写入文件
        /// </summary>
        /// <param name="obj"></param>
        public void sava2FileThread(object obj)
        {
            //获取无人机实例
            UvaEntity uvaClient = obj as UvaEntity;
            FileStream videoSavaFileStream=null;
            string videoSavaFile = uvaClient.videoFileName;
            try
            {
                //Trace.WriteLine("写入文件");
                fileSem.WaitOne();
                videoSavaFileStream = new FileStream(videoSavaFile, FileMode.Append);
                foreach (Byte[] videoSG in uvaClient.getUnactivateTmpVideoQueue())
                {
                    videoSavaFileStream.Write(videoSG, 0, videoSG.Length);
                }
                videoSavaFileStream.Flush();
                videoSavaFileStream.Close();
                videoSavaFileStream = null;
                fileSem.Release();
                uvaClient.clearQueue();

            }
            catch (Exception e)
            {


                Trace.WriteLine(e.ToString());
            }
            finally
            {
                if (videoSavaFileStream != null)
                {
                    videoSavaFileStream.Flush();
                    videoSavaFileStream.Close();
                    
                }
            }

            sem.WaitOne();
            uvaClient.fileIsWriting = false;
            sem.Release();

        }

        /// <summary>
        /// 视频接收完成后，删除视频接收循环
        /// </summary>
        private void deleteVideoReceiveLoop(object state)
        {

            //throw new NotImplementedException();
            Trace.WriteLine("检查接收任务是否完成，如果是，则准备删除线程");
        }

        /// <summary>
        /// 菜单栏--系统设置
        /// </summary>
        private void 系统设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SysSetting sysSet = new SysSetting();
            sysSet.Show();
        }

        private void button_showAllVideos_Click(object sender, EventArgs e)
        {
            All_UVA_Videos videos = new All_UVA_Videos();
            videos.Show();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 将panelOne和panelTwo 的位置互换，PanelOne为之前的大图，panelTwo为小图。
        /// 互换后，panelTwo为大图，panelTwo为小图。并且更新Global.MAIN_PANEL的值
        /// </summary>
        /// <param name="panelOneName">大的panel的Name</param>
        /// <param name="panelTwoName">小的panel的Name</param>
        /// <returns></returns>
        private bool changePanel(string panelOneName,string panelTwoName)
        {
            Panel panelOne = allPanel[panelOneName] as Panel;
            Panel panelTwo = allPanel[panelTwoName] as Panel;

            Point panelOne_Location = panelOne.Location;
            Size panelOne_Size = panelOne.Size;
            Point panelTwo_Location = panelTwo.Location;
            Size panelTwo_Size = panelTwo.Size;

            panelOne.Location = panelTwo_Location;
            panelOne.Size = panelTwo_Size;

            panelTwo.Location = panelOne_Location;
            panelTwo.Size = panelOne_Size;

            Global.MAIN_PANEL = panelTwo.Name;
            return true;
        }
        private void comboBox_allUVA_SelectedValueChanged(object sender, EventArgs e)
        {
            Trace.WriteLine(comboBox_allUVA.Text);
            if(((UvaEntity)panel_UVA[Global.MAIN_PANEL]).uvaName==comboBox_allUVA.Text)
            {
                Trace.WriteLine("无需交换");
            }
            else
            {
                Trace.WriteLine("需要交换");
                foreach(string panelName in panel_UVA.Keys)
                {
                    UvaEntity uvaS = panel_UVA[panelName] as UvaEntity;
                    if(uvaS.uvaName==comboBox_allUVA.Text)
                    {
                        changePanel(Global.MAIN_PANEL, panelName);
                        break;
                    }
                }
            }
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 发送close命令，主动断开连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_close_Click(object sender, EventArgs e)
        {
            if (comboBox_allUVA.Text == "") return;
            UvaEntity tmpUVA;
            bool flag = false;
            foreach (DictionaryEntry dtmpUVA in allUVA)
            {
                tmpUVA = dtmpUVA.Value as UvaEntity;
                if(comboBox_allUVA.Text==tmpUVA.uvaName)
                {
                    tmpUVA.sendClose();
                    try
                    {
                        comboBox_allUVA.Invoke(modifyUVACallBack, tmpUVA, false);
                    }
                    catch (Exception ee)
                    {

                        Trace.WriteLine(ee.Message);
                    }
                    break;
                }
            }

        }
        /// <summary>
        /// 开始监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_start_Click(object sender, EventArgs e)
        {
            startListenUVAConnection();
        }
        /// <summary>
        /// 关闭监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_stop_Click(object sender, EventArgs e)
        {
            //首先下线全部无人机
            if (comboBox_allUVA.Text != "")
            {
                Trace.WriteLine("首先断开全部无人机连接，并且保存视频");
                UvaEntity tmpUVA;
                bool flag = false;
                foreach (DictionaryEntry dtmpUVA in allUVA)
                {
                    tmpUVA = dtmpUVA.Value as UvaEntity;
                    tmpUVA.sendClose();
                    try
                    {
                        comboBox_allUVA.Invoke(modifyUVACallBack, tmpUVA, false);
                    }
                    catch (Exception ee)
                    {

                        Trace.WriteLine(ee.Message);
                    }

                }
            }
            //关闭监听线程
            try
            {
                if(dispatchThread.IsAlive)
                {
                    dispatchUDPClient.Close();
                    dispatchThread.Abort();
                    textBox_sysLog.Invoke(setSysLogCallBack, ("已关闭程序！"));
                }
                
            }
            catch (Exception ee)
            {

                //throw;
                Trace.WriteLine(ee.Message);
            }
            


        }
        private void clearButtonColor()
        {
            this.button1.ForeColor = Control.DefaultForeColor;
            this.button1.BackColor = Control.DefaultBackColor;
            this.button2.ForeColor = Control.DefaultForeColor;
            this.button2.BackColor = Control.DefaultBackColor;
            this.button3.ForeColor = Control.DefaultForeColor;
            this.button3.BackColor = Control.DefaultBackColor;
            this.button4.ForeColor = Control.DefaultForeColor;
            this.button4.BackColor = Control.DefaultBackColor;
            this.button5.ForeColor = Control.DefaultForeColor;
            this.button5.BackColor = Control.DefaultBackColor;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            clearButtonColor();
            this.button1.ForeColor = Color.Red;
            this.button1.BackColor = Color.Yellow;
            if (comboBox_allUVA.Text == "") return;
            foreach (DictionaryEntry dtmpUVA in allUVA)
            {
                UvaEntity tmpUVA = dtmpUVA.Value as UvaEntity;
                if (comboBox_allUVA.Text == tmpUVA.uvaName)
                {
                    tmpUVA.setBandWidth(40000);
                    break;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clearButtonColor();
            this.button3.ForeColor = Color.Red;
            this.button3.BackColor = Color.Yellow;
            if (comboBox_allUVA.Text == "") return;
            foreach (DictionaryEntry dtmpUVA in allUVA)
            {
                UvaEntity tmpUVA = dtmpUVA.Value as UvaEntity;
                if (comboBox_allUVA.Text == tmpUVA.uvaName)
                {
                    tmpUVA.setBandWidth(100000);
                    break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clearButtonColor();
            this.button2.ForeColor = Color.Red;
            this.button2.BackColor = Color.Yellow;
            if (comboBox_allUVA.Text == "") return;
            foreach (DictionaryEntry dtmpUVA in allUVA)
            {
                UvaEntity tmpUVA = dtmpUVA.Value as UvaEntity;
                if (comboBox_allUVA.Text == tmpUVA.uvaName)
                {
                    tmpUVA.setBandWidth(800000);
                    break;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clearButtonColor();
            this.button4.ForeColor = Color.Red;
            this.button4.BackColor = Color.Yellow;
            if (comboBox_allUVA.Text == "") return;
            foreach (DictionaryEntry dtmpUVA in allUVA)
            {
                UvaEntity tmpUVA = dtmpUVA.Value as UvaEntity;
                if (comboBox_allUVA.Text == tmpUVA.uvaName)
                {
                    tmpUVA.setBandWidth(2000000);
                    break;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            clearButtonColor();
            this.button5.ForeColor = Color.Red;
            this.button5.BackColor = Color.Yellow;
            if (comboBox_allUVA.Text == "") return;
            foreach (DictionaryEntry dtmpUVA in allUVA)
            {
                UvaEntity tmpUVA = dtmpUVA.Value as UvaEntity;
                if (comboBox_allUVA.Text == tmpUVA.uvaName)
                {
                    tmpUVA.setBandWidth(6000000);
                    break;
                }
            }
        }

        private void buttonshowLinkInfo_Click(object sender, EventArgs e)
        {

            try
            {
                if(linkInfoBoard.WindowState==FormWindowState.Minimized)
                {
                    linkInfoBoard.WindowState = FormWindowState.Normal;
                }
                linkInfoBoard.Show();
            }
            catch (Exception ee)
            {
                Trace.WriteLine(ee.Message);
                Trace.WriteLine(ee.StackTrace);
                
            }
            
        }

        /// <summary>
        /// 响应式布局
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected  void sizeChanged(object sender ,EventArgs e)
        {
            base.OnResizeEnd(e);
            Size endSize = this.Size;
            float percentWidth = (float)endSize.Width / _beforeDialogSize.Width;
            float percentHeight = (float)endSize.Height / _beforeDialogSize.Height;

            foreach (Control control in this.Controls)
            {
                if (control is DataGridView)
                    continue;
                //按比例改变控件大小
                if (!(control is Button))
                {
                    control.Width = (int)(control.Width * percentWidth);
                    control.Height = (int)(control.Height * percentHeight);
                }
                

                //为了不使控件之间覆盖 位置也要按比例变化
                control.Left = (int)(control.Left * percentWidth);
                control.Top = (int)(control.Top * percentHeight);
            }
            _beforeDialogSize = endSize;
        }


    }
}
