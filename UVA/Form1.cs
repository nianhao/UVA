using System;
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
        private Thread videoReceiveThread;
        public int port { get; private set; }
        public string ip { get; private set; }
        public IPEndPoint point { get; private set; }
        public UdpClient dispatchUDPClient { get; private set; }
        public Thread dispatchThread { get; private set; }

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
            textBox_sysLog.AppendText(nowString+"\r\n"+info);
        }
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
                //允许接收任意远端发送的消息
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                //阻塞，只到接收到消息
                Byte[] receiveBytes = dispatch.Receive(ref RemoteIpEndPoint);
                string receiveData = Encoding.ASCII.GetString(receiveBytes);
                //输出debug信息
                //Trace.WriteLine("接收到消息："+receiveData);
#if DEBUG 
                setSysLog("接收到来自"+RemoteIpEndPoint.ToString()+"消息：" + receiveData);
#endif
                string[] commands = receiveData.Split(';');
                switch(commands[1])
                {
                    case "start":
#if DEBUG
                        setSysLog("解析到 start 命令");
#endif
                        //接收到连接信息，输出日志信息
                        setSysLog("接收到来自" + RemoteIpEndPoint + "的连接请求");
                        //为新的UVA分配接收视频信息的udpClient
                        int restTimes = Global.MAX_RETRY_TIMES;
                        while(true)
                        {
                            if (restTimes>0)
                            {
#if DEBUG
                                setSysLog("正在为" + RemoteIpEndPoint.ToString() + "分配视频接收服务器");
                                setSysLog(string.Format("第{0}/{1}次尝试", Global.MAX_RETRY_TIMES - restTimes + 1, Global.MAX_RETRY_TIMES));
#endif
                            }
                            else
                            {
                                setSysLog(string.Format("为{0}分配视频接收服务器失败，系统资源不足", RemoteIpEndPoint.ToString()));
                                break;
                            }
                            //获取视频接收服务器的ip地址
                            string video_receive_ip = Global.RECEIVE_VIDEO_SERVER;
                            //生成端口，检查是否可用
                            System.Random a = new Random(System.DateTime.Now.Millisecond); // use System.DateTime.Now.Millisecond as seed
                            int RandKey = a.Next(Global.MINPORT,Global.MAXPORT);
                            try
                            {
                                //分配端口成功，开启新的线程，接收视频信息
                                UdpClient videoReceiveUDPClient = new UdpClient(new IPEndPoint(IPAddress.Parse(video_receive_ip),port));
                                //开启UDP视频接收线程
                                videoReceiveThread = new Thread(new ParameterizedThreadStart(videoReceiveLoop));
                                videoReceiveThread.IsBackground = true;
                                videoReceiveThread.Start(videoReceiveUDPClient);
                                //分配成功，输出信息
                                setSysLog(string.Format("为{0}分配视频接收服务器成功，{1}", RemoteIpEndPoint.ToString(), videoReceiveUDPClient.ToString()));
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
            UdpClient videoReceiveUDPClient = obj as UdpClient;
            while (true)
            {
                //允许接收任意远端发送的消息
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                //阻塞，只到接收到消息
                Byte[] receiveBytes = videoReceiveUDPClient.Receive(ref RemoteIpEndPoint);
                string receiveData = Encoding.ASCII.GetString(receiveBytes);
                //输出debug信息
                //Trace.WriteLine("接收到消息："+receiveData);
#if DEBUG 
                setSysLog("接收到来自" + RemoteIpEndPoint.ToString() + "消息：" + receiveData);
#endif
            }
        }

            private void 系统设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
