using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UVA
{

    class linkInfoManager
    {
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct tlinkinfo_band
        {
            Byte tkType;
            UInt64 tkUsedband;
            UInt64 tkMaxband;
        }
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct clinkinfo_band
        {
            Byte ckType;
            UInt64 ckUsedband;
            UInt64 ckMaxband;
        }
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct tlinkinfo
        {
            UInt16 tag;
            UInt64 termIP;
            Byte tkSection;
            Byte tkMaxNum;
            ArrayList tkband;
        }
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct clinkinfo
        {
            UInt16 tag;
            Byte CarID;
            Byte ckSection;
            Byte ckMaxNum;
            ArrayList ckband;
            ArrayList ttlinkinfo;
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
    }
}
