using AxWMPLib;
using System;
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
        //存储在线的全部无人机,考虑到线程安全和效率问题，使用hashTable
        /// <summary>
        /// 存放系统中保存过的无人机实体
        /// </summary>
        public Hashtable allUVA = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// 构造函数
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            InitSys();
            startListenUVAConnection();
            //test.test_copy();
        }
        /// <summary>
        /// 初始化整个系统
        /// </summary>
        public void InitSys()
        {
            //初始化系统
            setSysLog(Global.SystemInfo);

            /*try
            {
                axVLCPlugin21.playlist.add("file:///E:/Workspaces/VisualStudio/assets/test.h264");


                axVLCPlugin21.playlist.play();

            }
            catch (Exception e)
            {

                Trace.WriteLine(e.ToString());
            }*/

        }
        /// <summary>
        /// 设置系统日志信息
        /// </summary>
        /// <param name="info"></param>
        public void setSysLog(String info)
        {
            String nowString= DateTime.Now.ToLocalTime().ToString();
            textBox_sysLog.AppendText(nowString+"\r\n"+info+"\n");
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
                this.comboBox_allUVA.SelectedIndex = this.comboBox_allUVA.FindString(uvaName);
                int uvaNum = allUVA.Count;
                this.label_uvaNum.Text = Convert.ToString(uvaNum);
            }
        }
        /// <summary>
        /// 开启UVA监听连接。在这里实例化跨线程操作，开启调度线程
        /// </summary>
        public void startListenUVAConnection()
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
            //开启UDP监听
            dispatchThread = new Thread(new ParameterizedThreadStart(dispatchLoop));
            dispatchThread.IsBackground = true;
            dispatchThread.Start(dispatchUDPClient);
            //return true;
           

        }
        /// <summary>
        /// 调度循环，用于接收命令，完成调度。
        /// </summary>
        /// <param name="obj"></param>
        private void dispatchLoop(object obj)
        {
            //throw new NotImplementedException();
            UdpClient dispatch = obj as UdpClient;
            
            
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
                    receiveData = Encoding.UTF8.GetString(receiveBytes);
                }
                catch (Exception e)
                {

                    Trace.WriteLine(e.ToString());
                    continue;
                }

                //输出debug信息
                //Trace.WriteLine("接收到消息："+receiveData);
#if DEBUG 
                textBox_sysLog.Invoke(setSysLogCallBack,("接收到来自"+RemoteIpEndPoint.ToString()+"消息：" + receiveData));
#endif
                string[] commands = receiveData.Split(';');
                switch(commands[0])
                {
                    case "start":
                        {
#if DEBUG
                            textBox_sysLog.Invoke(setSysLogCallBack, ("解析到 start 命令"));

#endif
                            //获取无人机id
                            int uvaId = Convert.ToInt32(commands[2]);
                            //检查是否重复连接
                            if (allUVA.ContainsKey(uvaId))
                            {
                                string sendString = "error;DuplicateConnection;";
                                Byte[] sendBytes = Encoding.UTF8.GetBytes(sendString);
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
                                    UdpClient videoReceiveUDPClient = new UdpClient(new IPEndPoint(IPAddress.Parse(video_receive_ip), RandKey));
                                    //记录无人机
                                    UvaEntity tmpUVA = new UvaEntity(RemoteIpEndPoint.Address.ToString(), RemoteIpEndPoint.Port, Convert.ToInt32(commands[2]), videoReceiveUDPClient);
                                    allUVA.Add(uvaId, tmpUVA);
                                    //开启UDP视频接收线程
                                    videoReceiveThread = new Thread(new ParameterizedThreadStart(videoReceiveLoop));
                                    videoReceiveThread.IsBackground = true;
                                    videoReceiveThread.Start(tmpUVA);
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
                                }
                            }
                            break;
                        }

                    case "endTransform":
                        {
                            //获取无人机id
                            int uvaId = Convert.ToInt32(commands[2]);
                            break;
                        }
                    case "hart":
                        {
                            break;
                        }
                    default:
                        break;
                }

            }
        }
        /// <summary>
        /// 视频接收，循环接收视频信息，并保存
        /// </summary>
        /// <param name="obj">UDPclient 用于接收无人机传输视频</param>
        private void videoReceiveLoop(object obj)
        {
            //获取无人机实例
            UvaEntity uvaClient = obj as UvaEntity;
            //获取文件写入名
            String savaFileName = Global.getSavaFileName(uvaClient);
            uvaClient.videoFileName = savaFileName;
            
            videoReceiveThread.IsBackground = true;
            /* 在这个线程中写入文件，会使得文件无法实时更新，而无法播放
            FileStream savaFileStream = null;
            try
            {
                savaFileStream = new FileStream(savaFileName, FileMode.Create);
            }
            catch (Exception e)
            {

                //throw;
                Trace.WriteLine(e.ToString());
            }*/
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
        }
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

        }

  
    }
}
