using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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
        //视频接收线程
        private Thread videoReceiveThread;
        //监听的端口和ip，与Gloable相同
        public int port { get; private set; }
        public string ip { get; private set; }
        public IPEndPoint point { get; private set; }
        public UdpClient dispatchUDPClient { get; private set; }
        //调度线程
        public Thread dispatchThread { get; private set; }
        //声明跨线程操作的委托
        //设置日志信息
        private delegate void setSysLog_CALLBACK(string logInfo);
        private setSysLog_CALLBACK setSysLogCallBack;
        //增加无人机上线信息
        private delegate void modifyUVA_CALLBACK(UvaEntity newUVA,bool add);
        private modifyUVA_CALLBACK modifyUVACallBack;
        //存储在线的全部无人机,考虑到线程安全和效率问题，使用hashTable
        public Hashtable allUVA = Hashtable.Synchronized(new Hashtable());

        public Form1()
        {
            InitializeComponent();
            InitSys();
            startListenUVAConnection();
        }
        public void InitSys()
        {
            //初始化系统
            setSysLog(Global.SystemInfo);

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
#if DEBUG
                        textBox_sysLog.Invoke(setSysLogCallBack, ("解析到 start 命令"));

#endif
                        //获取无人机id
                        int uvaId = Convert.ToInt32(commands[2]);
                        //检查是否重复连接
                        if(allUVA.ContainsKey(uvaId))
                        {
                            string sendString = "error;DuplicateConnection;";
                            Byte[] sendBytes = Encoding.UTF8.GetBytes(sendString);
                            try
                            {
                                dispatch.Send(sendBytes, sendBytes.Length, RemoteIpEndPoint);
                                textBox_sysLog.Invoke(setSysLogCallBack, string.Format("向{0}:{1},{2}号无人机发送错误信息{3}", RemoteIpEndPoint.Address, RemoteIpEndPoint.Port, uvaId,sendString));

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
                        while(true)
                        {
                            if (restTimes>0)
                            {
#if DEBUG
                                textBox_sysLog.Invoke(setSysLogCallBack, ("正在为" + RemoteIpEndPoint.Address+":"+RemoteIpEndPoint.Port+ "分配视频接收服务器"));
                                textBox_sysLog.Invoke(setSysLogCallBack, (string.Format("第{0}/{1}次尝试", Global.MAX_RETRY_TIMES - restTimes + 1, Global.MAX_RETRY_TIMES)));
#endif
                            }
                            else
                            {
                                textBox_sysLog.Invoke(setSysLogCallBack, (string.Format("为{0}:{1}分配视频接收服务器失败，系统资源不足", RemoteIpEndPoint.Address,RemoteIpEndPoint.Port)));
                                break;
                            }
                            restTimes--;
                            //获取视频接收服务器的ip地址
                            string video_receive_ip = Global.RECEIVE_VIDEO_SERVER;
                            //生成端口，检查是否可用
                            System.Random a = new Random(System.DateTime.Now.Millisecond); // use System.DateTime.Now.Millisecond as seed
                            int RandKey = a.Next(Global.MINPORT,Global.MAXPORT);
#if DEBUG
                            textBox_sysLog.Invoke(setSysLogCallBack, string.Format("为{0}:{1}尝试分配端口{2}", RemoteIpEndPoint.Address, RemoteIpEndPoint.Port, RandKey));
#endif
                            try
                            {
                                //分配端口成功，开启新的线程，接收视频信息
                                UdpClient videoReceiveUDPClient = new UdpClient(new IPEndPoint(IPAddress.Parse(video_receive_ip),RandKey));
                                //记录无人机
                                UvaEntity tmpUVA = new UvaEntity(RemoteIpEndPoint.Address.ToString(), RemoteIpEndPoint.Port, Convert.ToInt32(commands[2]),videoReceiveUDPClient);
                                allUVA.Add(uvaId, tmpUVA);
                                //开启UDP视频接收线程
                                videoReceiveThread = new Thread(new ParameterizedThreadStart(videoReceiveLoop));
                                videoReceiveThread.IsBackground = true;
                                videoReceiveThread.Start(videoReceiveUDPClient);
                                //暂停0.5秒，等待allUVA更新
                                //System.Threading.Thread.Sleep(500);
                                //分配成功，输出信息
                                textBox_sysLog.Invoke(setSysLogCallBack, (string.Format("为{0}:{1}分配视频接收服务器成功，视频接收地址为{2}:{3}", RemoteIpEndPoint.Address, RemoteIpEndPoint.Port, video_receive_ip, RandKey)));
                                //在全部无人机combbox里面添加一项
                                comboBox_allUVA.Invoke(modifyUVACallBack, tmpUVA, true);
                                break;
                            }
                            catch(Exception e)
                            {
                                Trace.WriteLine(e.StackTrace);
                            }
                        }
                        break;
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
            //throw new NotImplementedException();
            UvaEntity uvaClient = obj as UvaEntity;
            while (true)
            {
                //允许接收任意远端发送的消息
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                //阻塞，只到接收到消息
                Byte[] receiveBytes = uvaClient.videoReceiveClient.Receive(ref RemoteIpEndPoint);
                string receiveData = Encoding.UTF8.GetString(receiveBytes);
                //输出debug信息
                //Trace.WriteLine("接收到消息："+receiveData);
#if DEBUG 
                textBox_sysLog.Invoke(setSysLogCallBack, ("接收到来自" + RemoteIpEndPoint.ToString() + "消息：" + receiveData));
#endif
            }
        }

            private void 系统设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
