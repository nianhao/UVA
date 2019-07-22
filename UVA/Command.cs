using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UVA
{
    class Command
    {
        //注意这个属性不能少
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct UVA_RESPONSE
        {
            public char sendType;
            public Int32 IPFirst;
            public Int32 IPSecond;
            public Int32 IPThird;
            public Int32 IPFourth;
            public Int32 Port;
            public char errorType;
            public int bandWidth;
        }
        /// <summary>
        /// 已经弃用
        /// </summary>
        public static string READY_COMMAND(string ip, string port)
        {
            string command = string.Format("ready;{0};{1}", ip, port);
            return command;
        }
        /// <summary>
        /// 生成心跳响应的信息
        /// </summary>
        public static Byte[] HeratResponse(UvaEntity uvaT)
        {
            UVA_RESPONSE repMsg = new UVA_RESPONSE();
            string ip = Global.cpeIP;
            string[] ips = ip.Split('.');
            int ipFirst = Convert.ToInt32(ips[0]);
            int ipSecond = Convert.ToInt32(ips[1]);
            int ipThird = Convert.ToInt32(ips[2]);
            int ipFourth = Convert.ToInt32(ips[3]);
            repMsg.IPFirst = ipFirst;
            repMsg.IPSecond = ipSecond;
            repMsg.IPThird = ipThird;
            repMsg.IPFourth = ipFourth;
            repMsg.Port = uvaT.videoPort;
            repMsg.sendType = '\u0002';
            repMsg.bandWidth = uvaT.bandWidth;
            return StructToBytes(repMsg);
        }
        /// <summary>
        /// 生成ready的信息
        /// </summary>
        public static Byte[] Ready(string ip, int port)
        {
            string[] ips = ip.Split('.');
            int ipFirst = Convert.ToInt32(ips[0]);
            int ipSecond = Convert.ToInt32(ips[1]);
            int ipThird = Convert.ToInt32(ips[2]);
            int ipFourth = Convert.ToInt32(ips[3]);

            UVA_RESPONSE repMsg = new UVA_RESPONSE();
            repMsg.sendType = Global.cmdTypeREADY;
            repMsg.IPFirst = ipFirst;
            repMsg.IPSecond = ipSecond;
            repMsg.IPThird = ipThird;
            repMsg.IPFourth = ipFourth;
            repMsg.Port = port;
            return StructToBytes(repMsg);
        }
        /// <summary>
        /// 生成重复连接错误的报文字节码
        /// </summary>
        /// <returns>Byte[] struct UVA_RESPONSE转换出的字节码</returns>
        public static Byte [] DuplicateConnection(string ip, int port)
        {
            UVA_RESPONSE repMsg = new UVA_RESPONSE();
            //'\u0003':error
            //'\u0001':DuplicateConnection
            //更多错误消息的定义，请查看通信协议
            repMsg.sendType = Global.cmdTypeERROR;
            repMsg.errorType = '\u0001';
            string[] ips = ip.Split('.');
            int ipFirst = Convert.ToInt32(ips[0]);
            int ipSecond = Convert.ToInt32(ips[1]);
            int ipThird = Convert.ToInt32(ips[2]);
            int ipFourth = Convert.ToInt32(ips[3]);

           // UVA_RESPONSE repMsg = new UVA_RESPONSE();
           // repMsg.sendType = '\u0001';
            repMsg.IPFirst = ipFirst;
            repMsg.IPSecond = ipSecond;
            repMsg.IPThird = ipThird;
            repMsg.IPFourth = ipFourth;
            repMsg.Port = port;
            return StructToBytes(repMsg);
        }
        /// <summary>
        /// 生成重复连接错误的报文字节码
        /// </summary>
        /// <returns>Byte[] struct UVA_RESPONSE转换出的字节码</returns>
        public static Byte[] DuplicateDisconnect()
        {
            UVA_RESPONSE repMsg = new UVA_RESPONSE();
            //'\u0003':error
            //'\u0001':DuplicateConnection
            //更多错误消息的定义，请查看通信协议
            repMsg.sendType = Global.cmdTypeERROR;
            repMsg.errorType = '\u0003';
            return StructToBytes(repMsg);
        }
        /// <summary>
        /// 生成重复连接错误的报文字节码
        /// </summary>
        /// <returns>Byte[] struct UVA_RESPONSE转换出的字节码</returns>
        public static Byte[] Error(char errorType)
        {
            UVA_RESPONSE repMsg = new UVA_RESPONSE();
            //'\u0003':error
            //'\u0001':DuplicateConnection
            //更多错误消息的定义，请查看通信协议
            repMsg.sendType = Global.cmdTypeERROR;
            repMsg.errorType = errorType;
            return StructToBytes(repMsg);
        }
        /// <summary>
        /// 结构体转byte数组
        /// </summary>
        /// <param name="structObj">要转换的结构体</param>
        /// <returns>转换后的byte数组</returns>
        public static Byte[] StructToBytes(object structObj)
        {
            //得到结构体的大小
            int size = Marshal.SizeOf(structObj);
            //创建byte数组
            Byte[] bytes = new byte[size];
            //分配结构体大小的内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将结构体拷到分配好的内存空间
            Marshal.StructureToPtr(structObj, structPtr, false);
            //从内存空间拷到byte数组
            Marshal.Copy(structPtr, bytes, 0, size);
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);
            //返回byte数组
            return bytes;
        }
        /// <summary>
        /// byte数组转结构体
        /// </summary>
        /// <param name="bytes">byte数组</param>
        /// <param name="type">结构体类型</param>
        /// <returns>转换后的结构体</returns>
        public static object BytesToStuct(byte[] bytes, Type type)
        {
            //得到结构体的大小
            int size = Marshal.SizeOf(type);
            //byte数组长度小于结构体的大小
            if (size > bytes.Length)
            {
                //返回空
                return null;
            }
            //分配结构体大小的内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将byte数组拷到分配好的内存空间
            Marshal.Copy(bytes, 0, structPtr, size);
            //将内存空间转换为目标结构体
            object obj = Marshal.PtrToStructure(structPtr, type);
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);
            //返回结构体
            return obj;
        }
        /// <summary>
        /// 生成close命令
        /// </summary>
        /// <returns></returns>
        public static byte[] Close()
        {
            //throw new NotImplementedException();
            UVA_RESPONSE repMsg = new UVA_RESPONSE();
            //'\u0003':error
            //'\u0001':DuplicateConnection
            //更多错误消息的定义，请查看通信协议
            repMsg.sendType = Global.cmdTypeCLOSE;
            //repMsg.errorType = errorType;
            return StructToBytes(repMsg);
        }
    }
}
