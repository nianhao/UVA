using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UVA
{
    public class UvaEntity
    {
        //无人机的编号
        /// <summary>
        /// 无人机id
        /// </summary>
        public int id { get; private set; }
        //无人机的名字
        /// <summary>
        /// 无人机显示名字
        /// </summary>
        public string uvaName { get; private set; }
        //无人机的ip与端口号
        /// <summary>
        /// 无人机ip
        /// </summary>
        public string ip { get; private set; }
        /// <summary>
        /// 无人机端口号
        /// </summary>
        public int port { get; private set; }
        //无人机的在线状态
        /// <summary>
        /// 无人机在线状态
        /// </summary>
        public bool online { get; private set; }
        //无人机的上线时间
        /// <summary>
        /// 无人机上线时间
        /// </summary>
        public string loginTime { get; private set; }
        //无人机的消息
        /// <summary>
        /// 无人机消息记录
        /// </summary>
        public ArrayList messages { get; private set; }
        //无人机上次发送心跳的时间
        /// <summary>
        /// 无人机上一次心跳发送时间
        /// </summary>
        public string lastHeartBeatTime { get; private set; }
        //用于接收视频的socket
        /// <summary>
        /// 接收无人机视频的UDPClient
        /// </summary>
        public UdpClient videoReceiveClient { get; private set; }

        //构造函数
        /// <summary>
        /// 构造方法
        /// </summary>
        public UvaEntity(string ip, int port, int id, UdpClient videoReceiveClient)
        {
            this.ip = ip;
            this.port = port;
            this.id = id;
            this.videoReceiveClient = videoReceiveClient;
            this.loginTime = DateTime.Now.ToLocalTime().ToString();
            this.uvaName = string.Format("{0}号无人机", id);
        }

    }
}
