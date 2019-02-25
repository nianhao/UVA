using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UVA
{
    public class UvaEntity
    {
        //无人机的编号
        public int id { get; private set; }
        //无人机的名字
        public string uvaName { get; private set; }
        //无人机的ip与端口号
        public string ip { get; private set; }
        public int port { get; private set; }
        //无人机的在线状态
        public bool online { get; private set; }
        //无人机的上线时间
        public string loginTime { get; private set; }
        //无人机的消息
        public ArrayList messages { get; private set; }
        //无人机上次发送心跳的时间
        public string lastHeartBeatTime { get; private set; }

        //构造函数
        public UvaEntity(string ip,int port,int id)
        {
            this.ip = ip;
            this.port = port;
            this.id = id;
            this.uvaName = string.Format("{0}号无人机", id);
        }
        
    }
}
