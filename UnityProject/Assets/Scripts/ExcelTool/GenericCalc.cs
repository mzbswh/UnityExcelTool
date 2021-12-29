using System;

namespace ExcelTool
{
    static class GenericCalc
    {
        public static class MathHelper<T>
        {
            public static Func<T, T, T> Add;
            public static Func<T, int, T> AddInt;
            public static Func<T, T, T> Sub;
            public static Func<T, T, int> SubToInt;
            public static Func<T, T, T> Mul;
            public static Func<T, T, T> Div;
            public static Func<T, T, int> DivToInt;
            public static Func<T, int, int> DivInt;
        }

        static GenericCalc()
        {
            MathHelper<sbyte>.Add = (a, b) => AddSbyte(a, b);
            MathHelper<byte>.Add = (a, b) => AddByte(a, b);
            MathHelper<ushort>.Add = (a, b) => AddUShort(a, b);
            MathHelper<short>.Add = (a, b) => AddShort(a, b);
            MathHelper<uint>.Add = (a, b) => AddUInt(a, b);
            MathHelper<int>.Add = (a, b) => AddInt(a, b);
            MathHelper<ulong>.Add = (a, b) => AddULong(a, b);
            MathHelper<long>.Add = (a, b) => AddLong(a, b);

            MathHelper<sbyte>.AddInt = (a, b) => AddSbyteInt(a, b);
            MathHelper<byte>.AddInt = (a, b) => AddByteInt(a, b);
            MathHelper<ushort>.AddInt = (a, b) => AddUShortInt(a, b);
            MathHelper<short>.AddInt = (a, b) => AddShortInt(a, b);
            MathHelper<uint>.AddInt = (a, b) => AddUIntInt(a, b);
            MathHelper<int>.AddInt = (a, b) => AddIntInt(a, b);
            MathHelper<ulong>.AddInt = (a, b) => AddULongInt(a, b);
            MathHelper<long>.AddInt = (a, b) => AddLongInt(a, b);

            MathHelper<sbyte>.Sub = (a, b) => SubSbyte(a, b);
            MathHelper<byte>.Sub = (a, b) => SubByte(a, b);
            MathHelper<ushort>.Sub = (a, b) => SubUShort(a, b);
            MathHelper<short>.Sub = (a, b) => SubShort(a, b);
            MathHelper<uint>.Sub = (a, b) => SubUInt(a, b);
            MathHelper<int>.Sub = (a, b) => SubInt(a, b);
            MathHelper<ulong>.Sub = (a, b) => SubULong(a, b);
            MathHelper<long>.Sub = (a, b) => SubLong(a, b);

            MathHelper<sbyte>.SubToInt = (a, b) => SubSbyteToInt(a, b);
            MathHelper<byte>.SubToInt = (a, b) => SubByteToInt(a, b);
            MathHelper<ushort>.SubToInt = (a, b) => SubUShortToInt(a, b);
            MathHelper<short>.SubToInt = (a, b) => SubShortToInt(a, b);
            MathHelper<uint>.SubToInt = (a, b) => SubUIntToInt(a, b);
            MathHelper<int>.SubToInt = (a, b) => SubIntToInt(a, b);
            MathHelper<ulong>.SubToInt = (a, b) => SubULongToInt(a, b);
            MathHelper<long>.SubToInt = (a, b) => SubLongToInt(a, b);

            MathHelper<sbyte>.DivToInt = (a, b) => DivSbyteToInt(a, b);
            MathHelper<byte>.DivToInt = (a, b) => DivByteToInt(a, b);
            MathHelper<ushort>.DivToInt = (a, b) => DivUShortToInt(a, b);
            MathHelper<short>.DivToInt = (a, b) => DivShortToInt(a, b);
            MathHelper<uint>.DivToInt = (a, b) => DivUIntToInt(a, b);
            MathHelper<int>.DivToInt = (a, b) => DivIntInt(a, b);
            MathHelper<ulong>.DivToInt = (a, b) => DivULongToInt(a, b);
            MathHelper<long>.DivToInt = (a, b) => DivLongToInt(a, b);

            MathHelper<sbyte>.DivInt = (a, b) => DivSbyteInt(a, b);
            MathHelper<byte>.DivInt = (a, b) => DivByteInt(a, b);
            MathHelper<ushort>.DivInt = (a, b) => DivUShortInt(a, b);
            MathHelper<short>.DivInt = (a, b) => DivShortInt(a, b);
            MathHelper<uint>.DivInt = (a, b) => DivUIntInt(a, b);
            MathHelper<int>.DivInt = (a, b) => DivIntInt(a, b);
            MathHelper<ulong>.DivInt = (a, b) => DivULongInt(a, b);
            MathHelper<long>.DivInt = (a, b) => DivLongInt(a, b);
        }

        public static T Add<T>(T a, T b) => MathHelper<T>.Add(a, b);
        public static T AddInt<T>(T a, int b) => MathHelper<T>.AddInt(a, b);
        public static T Sub<T>(T a, T b) => MathHelper<T>.Sub(a, b);
        public static int SubToInt<T>(T a, T b) => MathHelper<T>.SubToInt(a, b);
        public static int DivToInt<T>(T a, T b) => MathHelper<T>.DivToInt(a, b);
        public static int DivInt<T>(T a, int b) => MathHelper<T>.DivInt(a, b);

        static sbyte AddSbyte(sbyte a, sbyte b) => (sbyte)(a + b);
        static byte AddByte(byte a, byte b) => (byte)(a + b);
        static ushort AddUShort(ushort a, ushort b) => (ushort)(a + b);
        static short AddShort(short a, short b) => (short)(a + b);
        static uint AddUInt(uint a, uint b) => a + b;
        static int AddInt(int a, int b) => a + b;
        static ulong AddULong(ulong a, ulong b) => a + b;
        static long AddLong(long a, long b) => a + b;

        static sbyte AddSbyteInt(sbyte a, int b) => (sbyte)(a + b);
        static byte AddByteInt(byte a, int b) => (byte)(a + b);
        static ushort AddUShortInt(ushort a, int b) => (ushort)(a + b);
        static short AddShortInt(short a, int b) => (short)(a + b);
        static uint AddUIntInt(uint a, int b) => a + (uint)b;
        static int AddIntInt(int a, int b) => a + b;
        static ulong AddULongInt(ulong a, int b) => a + (ulong)b;
        static long AddLongInt(long a, int b) => a + b;

        static sbyte SubSbyte(sbyte a, sbyte b) => (sbyte)(a - b);
        static byte SubByte(byte a, byte b) => (byte)(a - b);
        static ushort SubUShort(ushort a, ushort b) => (ushort)(a - b);
        static short SubShort(short a, short b) => (short)(a - b);
        static uint SubUInt(uint a, uint b) => a - b;
        static int SubInt(int a, int b) => a - b;
        static ulong SubULong(ulong a, ulong b) => a - b;
        static long SubLong(long a, long b) => a - b;

        static int SubSbyteToInt(sbyte a, sbyte b) => (sbyte)(a - b);
        static int SubByteToInt(byte a, byte b) => (byte)(a - b);
        static int SubUShortToInt(ushort a, ushort b) => (ushort)(a - b);
        static int SubShortToInt(short a, short b) => (short)(a - b);
        static int SubUIntToInt(uint a, uint b) => (int)(a - b);
        static int SubIntToInt(int a, int b) => a - b;
        static int SubULongToInt(ulong a, ulong b) => (int)(a - b);
        static int SubLongToInt(long a, long b) => (int)(a - b);

        static int DivSbyteToInt(sbyte a, sbyte b) => a / b;
        static int DivByteToInt(byte a, byte b) => a / b;
        static int DivUShortToInt(ushort a, ushort b) => a / b;
        static int DivShortToInt(short a, short b) => a / b;
        static int DivUIntToInt(uint a, uint b) => (int)(a / b);
        static int DivIntInt(int a, int b) => a / b;
        static int DivULongToInt(ulong a, ulong b) => (int)(a / b);
        static int DivLongToInt(long a, long b) => (int)(a / b);

        static int DivSbyteInt(sbyte a, int b) => a / b;
        static int DivByteInt(byte a, int b) => a / b;
        static int DivUShortInt(ushort a, int b) => a / b;
        static int DivShortInt(short a, int b) => a / b;
        static int DivUIntInt(uint a, int b) => (int)(a / b);
        static int DivULongInt(ulong a, int b) => (int)(a / (ulong)b);
        static int DivLongInt(long a, int b) => (int)(a / b);
    }
}