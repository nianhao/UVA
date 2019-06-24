﻿ using System;
using System.Collections;
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
        public static string CONNECTION_IP = "0.0.0.0";
        //分配视频接收服务时的端口范围
        /// <summary>
        /// 视频接收服务器，分配的最小端口号
        /// </summary>
        public static int MINPORT = 11470;
        /// <summary>
        /// 视频接收服务器，分配的最大端口号
        /// </summary>
        public static int MAXPORT = 11470;
        //设置视频接收服务器的IP
        /// <summary>
        /// 视频接收服务器的IP地址
        /// </summary>
        public static string RECEIVE_VIDEO_SERVER = "192.168.1.236";
        /// <summary>
        /// VLC监听的地址
        /// </summary>
        public static string SERVERIP = "192.168.1.236";
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
        public static String videoSavePath = "c:\\";
        public static string PLUGINPATH = "";
        /// <summary>
        /// 生成文件签名
        /// </summary>
        public static string getSavaFileName(UvaEntity uvaClient)
        {
            //throw new NotImplementedException();
            return videoSavePath + "UVA_"+uvaClient.id + "_" + DateTime.Now.ToLocalTime().ToString("yyyy_MM_dd_hh_mm_ss") + ".h264";
        }
        public static string [] PANEL_NAMES= { "panel1","panel2","panel3", "panel4", "panel5", "panel6" , "panel_VLCPlayer" };
        /// <summary>
        /// 初始的大的panel的名字
        /// </summary>
        public static string MAIN_PANEL = "panel_VLCPlayer";
        /// <summary>
        /// 服务器存放位置的post地址
        /// </summary>
        public static string POSTION_POST_URL = "http://62.234.120.220:9090/YJ_NH/processFootPrint";
        /// <summary>
        /// 接收到的消息来源
        /// </summary>
        public  enum msgFromType {uva, helmet };
        /// <summary>
        /// 配置发送出去的命令对应的表示
        /// </summary>
        public static char DuplicatedeDisconnect = '\u0003';
        public static char cmdTypeREADY = '\u0001';
        public static char cmdTypeHEART = '\u0002';
        public static char cmdTypeERROR = '\u0003';
        public static char cmdTypeOK = '\u0004';
        public static char cmdTypeCLOSE = '\u0005';
        /// <summary>
        /// 配置无人机发送的，我们接收到的命令表示
        /// </summary>
        public static char recTypeBEGIN = '\u0001';
        public static char recTypeHEART = '\u0002';
        public static char recTypeEND = '\u0003';
        public static char recTypeOK = '\u0004';
        //public static char cmdTypeCLOSE = '\u0005';

    }
}
