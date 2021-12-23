using System.Text;
using System;
using System.IO;

namespace ExcelTool
{
    static class FastBitConverter
    {
        public unsafe static short ToInt16(byte[] value, int startIndex)
        {
            fixed (byte* ptr = &value[startIndex])
            {
                return *(short*)ptr;
            }
        }

        public unsafe static int ToInt32(byte[] value, int startIndex)
        {
            fixed (byte* ptr = &value[startIndex])
            {
                return *(int*)ptr;
            }
        }

        public unsafe static long ToInt64(byte[] value, int startIndex)
        {
            fixed (byte* ptr = &value[startIndex])
            {
                return *(long*)ptr;
            }
        }

        public static ushort ToUInt16(byte[] value, int startIndex) => (ushort)ToInt16(value, startIndex);

        public static uint ToUInt32(byte[] value, int startIndex) => (uint)ToInt32(value, startIndex);

        public static ulong ToUInt64(byte[] value, int startIndex) => (ulong)ToInt64(value, startIndex);

        public unsafe static float ToSingle(byte[] value, int startIndex)
        {
            int num = ToInt32(value, startIndex);
            return *(float*)(&num);
        }

        public unsafe static double ToDouble(byte[] value, int startIndex)
        {
            long num = ToInt64(value, startIndex);
            return *(double*)(&num);
        }
    }
}