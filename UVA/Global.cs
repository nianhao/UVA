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
        /// <summary>
        /// 服务器连接端口
        /// </summary>
        public static int CONNECTION_PORT = 9090;
        /// <summary>
        /// 服务器连接IP
        /// </summary>
        public static string CONNECTION_IP = "127.0.0.1";
        //分配视频接收服务时的端口范围
        /// <summary>
        /// 视频接收服务器，分配的最小端口号
        /// </summary>
        public static int MINPORT = 10000;
        /// <summary>
        /// 视频接收服务器，分配的最大端口号
        /// </summary>
        public static int MAXPORT = 60000;
        //设置视频接收服务器的IP
        /// <summary>
        /// 视频接收服务器的IP地址
        /// </summary>
        public static string RECEIVE_VIDEO_SERVER = "127.0.0.1";
        //设置分配端口的最大重试次数
        /// <summary>
        /// 视频接收服务器，最大创建重试次数
        /// </summary>
        public static int MAX_RETRY_TIMES = 100;
        /// <summary>
        /// 系统欢迎信息
        /// </summary>
        public static String SystemInfo = "无人机通信系统V1.0\r\n默认监听地址" + CONNECTION_IP + ":" + CONNECTION_PORT;
        //设置最大失联时间(单位为s)
        /// <summary>
        /// 最大心跳间隔时间
        /// </summary>
        public static int MAX_LOST_TIME = 60 * 10;//设置为10分钟
                                                  //设置视频保存路径
                                                  /// <summary>
                                                  /// 视频保存路径
                                                  /// </summary>
        public static String videoSavePath = "e:\\UVA\\videos\\";
        /// <summary>
        /// 生成文件签名
        /// </summary>
        public static string getSavaFileName(UvaEntity uvaClient)
        {
            //throw new NotImplementedException();
            return videoSavePath + uvaClient.uvaName + "_" + DateTime.Now.ToLocalTime().ToString("yyyy_MM_dd_hh_mm_ss") + ".h264";
        }
    }
}
