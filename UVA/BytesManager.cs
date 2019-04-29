using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace UVA
{
    
    class BytesManager
    {
        //注意这个属性不能少
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct UVA_RECEIVE
        {
            public char sendType;
            public char cliType;
            public char lonDir;
            public char latDir;
            public Int32 cliNum;
            public Int32 lonDeg;
            public Int32 lonMin;
            public Int32 lonSec;
            public Int32 latDeg;
            public Int32 latMin;
            public Int32 latSec;
            public Int32 year;
            public Int32 month;
            public Int32 day;
            public Int32 hour;
            public Int32 minute;
            public Int32 second;
            public Int32 IPFirst;
            public Int32 IPSecond;
            public Int32 IPThird;
            public Int32 IPFourth;
        }
        public UVA_RECEIVE uvaMsg;
        public int msgForm = -1;

        public BytesManager(byte [] message)
        {
            switch(message[0])
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                    UVA_RECEIVE m = new UVA_RECEIVE();
                    msgForm = (int)Global.msgFromType.uva;
                    uvaMsg = (UVA_RECEIVE)BytesToStuct(message, m.GetType());
                    //Console.WriteLine("s");
                    break;
                default:
                    break;
            }
            
            
        }
        /// <summary>
        /// 结构体转byte数组
        /// </summary>
        /// <param name="structObj">要转换的结构体</param>
        /// <returns>转换后的byte数组</returns>
        public static byte[] StructToBytes(object structObj)
        {
            //得到结构体的大小
            int size = Marshal.SizeOf(structObj);
            //创建byte数组
            byte[] bytes = new byte[size];
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
    }
}
