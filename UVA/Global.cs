using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UVA
{
    class Global
    {
        //调度服务器的端口和IP
        public static int CONNECTION_PORT = 9090;
        public static string CONNECTION_IP = "127.0.0.1";
        //分配视频接收服务时的端口范围
        public static int MINPORT = 10000;
        public static int MAXPORT = 60000;
        //设置视频接收服务器的IP
        public static string RECEIVE_VIDEO_SERVER = "127.0.0.1";
        //设置分配端口的最大重试次数
        public static int MAX_RETRY_TIMES = 100;
        public static String SystemInfo = "无人机通信系统V1.0\r\n默认监听地址" + CONNECTION_IP + ":" + CONNECTION_PORT;

       

    }
}
