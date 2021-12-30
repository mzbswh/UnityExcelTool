using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ExcelTool
{
    public class ByteReader
    {
        static ByteReader()
        {
            utf8 = Encoding.UTF8;
            ReadHelper<bool>.Read = (data, index) => ReadBool(data, index);
            ReadHelper<sbyte>.Read = (data, index) => ReadSByte(data, index);
            ReadHelper<byte>.Read = (data, index) => ReadByte(data, index);
            ReadHelper<ushort>.Read = (data, index) => ReadUShort(data, index);
            ReadHelper<short>.Read = (data, index) => ReadShort(data, index);
            ReadHelper<uint>.Read = (data, index) => ReadUInt(data, index);
            ReadHelper<int>.Read = (data, index) => ReadInt(data, index);
            ReadHelper<ulong>.Read = (data, index) => ReadULong(data, index);
            ReadHelper<long>.Read = (data, index) => ReadLong(data, index);
            ReadHelper<float>.Read = (data, index) => ReadFloat(data, index);
            ReadHelper<double>.Read = (data, index) => ReadDouble(data, index);
            ReadHelper<string>.Read = (data, index) => ReadString(data, index);

            ReadHelper<List<bool>>.Read = (data, index) => ReadListBool(data, index);
            ReadHelper<List<sbyte>>.Read = (data, index) => ReadListSByte(data, index);
            ReadHelper<List<byte>>.Read = (data, index) => ReadListByte(data, index);
            ReadHelper<List<ushort>>.Read = (data, index) => ReadListUShort(data, index);
            ReadHelper<List<short>>.Read = (data, index) => ReadListShort(data, index);
            ReadHelper<List<uint>>.Read = (data, index) => ReadListUInt(data, index);
            ReadHelper<List<int>>.Read = (data, index) => ReadListInt(data, index);
            ReadHelper<List<ulong>>.Read = (data, index) => ReadListULong(data, index);
            ReadHelper<List<long>>.Read = (data, index) => ReadListLong(data, index);
            ReadHelper<List<float>>.Read = (data, index) => ReadListFloat(data, index);
            ReadHelper<List<double>>.Read = (data, index) => ReadListDouble(data, index);
            ReadHelper<List<string>>.Read = (data, index) => ReadListString(data, index);

            ReadHelper<Vector2>.Read = (data, index) => ReadVector2(data, index);
            ReadHelper<Vector3>.Read = (data, index) => ReadVector3(data, index);
            ReadHelper<Vector4>.Read = (data, index) => ReadVector4(data, index);
            ReadHelper<Vector2Int>.Read = (data, index) => ReadVector2Int(data, index);
            ReadHelper<Vector3Int>.Read = (data, index) => ReadVector3Int(data, index);

            #region Dict

            ReadHelper<Dictionary<bool, bool>>.Read = (data, index) => ReadDict<bool, bool>(data, index);
            ReadHelper<Dictionary<bool, sbyte>>.Read = (data, index) => ReadDict<bool, sbyte>(data, index);
            ReadHelper<Dictionary<bool, byte>>.Read = (data, index) => ReadDict<bool, byte>(data, index);
            ReadHelper<Dictionary<bool, ushort>>.Read = (data, index) => ReadDict<bool, ushort>(data, index);
            ReadHelper<Dictionary<bool, short>>.Read = (data, index) => ReadDict<bool, short>(data, index);
            ReadHelper<Dictionary<bool, uint>>.Read = (data, index) => ReadDict<bool, uint>(data, index);
            ReadHelper<Dictionary<bool, int>>.Read = (data, index) => ReadDict<bool, int>(data, index);
            ReadHelper<Dictionary<bool, ulong>>.Read = (data, index) => ReadDict<bool, ulong>(data, index);
            ReadHelper<Dictionary<bool, long>>.Read = (data, index) => ReadDict<bool, long>(data, index);
            ReadHelper<Dictionary<bool, float>>.Read = (data, index) => ReadDict<bool, float>(data, index);
            ReadHelper<Dictionary<bool, double>>.Read = (data, index) => ReadDict<bool, double>(data, index);
            ReadHelper<Dictionary<bool, string>>.Read = (data, index) => ReadDict<bool, string>(data, index);

            ReadHelper<Dictionary<sbyte, bool>>.Read = (data, index) => ReadDict<sbyte, bool>(data, index);
            ReadHelper<Dictionary<sbyte, sbyte>>.Read = (data, index) => ReadDict<sbyte, sbyte>(data, index);
            ReadHelper<Dictionary<sbyte, byte>>.Read = (data, index) => ReadDict<sbyte, byte>(data, index);
            ReadHelper<Dictionary<sbyte, ushort>>.Read = (data, index) => ReadDict<sbyte, ushort>(data, index);
            ReadHelper<Dictionary<sbyte, short>>.Read = (data, index) => ReadDict<sbyte, short>(data, index);
            ReadHelper<Dictionary<sbyte, uint>>.Read = (data, index) => ReadDict<sbyte, uint>(data, index);
            ReadHelper<Dictionary<sbyte, int>>.Read = (data, index) => ReadDict<sbyte, int>(data, index);
            ReadHelper<Dictionary<sbyte, ulong>>.Read = (data, index) => ReadDict<sbyte, ulong>(data, index);
            ReadHelper<Dictionary<sbyte, long>>.Read = (data, index) => ReadDict<sbyte, long>(data, index);
            ReadHelper<Dictionary<sbyte, float>>.Read = (data, index) => ReadDict<sbyte, float>(data, index);
            ReadHelper<Dictionary<sbyte, double>>.Read = (data, index) => ReadDict<sbyte, double>(data, index);
            ReadHelper<Dictionary<sbyte, string>>.Read = (data, index) => ReadDict<sbyte, string>(data, index);

            ReadHelper<Dictionary<byte, bool>>.Read = (data, index) => ReadDict<byte, bool>(data, index);
            ReadHelper<Dictionary<byte, sbyte>>.Read = (data, index) => ReadDict<byte, sbyte>(data, index);
            ReadHelper<Dictionary<byte, byte>>.Read = (data, index) => ReadDict<byte, byte>(data, index);
            ReadHelper<Dictionary<byte, ushort>>.Read = (data, index) => ReadDict<byte, ushort>(data, index);
            ReadHelper<Dictionary<byte, short>>.Read = (data, index) => ReadDict<byte, short>(data, index);
            ReadHelper<Dictionary<byte, uint>>.Read = (data, index) => ReadDict<byte, uint>(data, index);
            ReadHelper<Dictionary<byte, int>>.Read = (data, index) => ReadDict<byte, int>(data, index);
            ReadHelper<Dictionary<byte, ulong>>.Read = (data, index) => ReadDict<byte, ulong>(data, index);
            ReadHelper<Dictionary<byte, long>>.Read = (data, index) => ReadDict<byte, long>(data, index);
            ReadHelper<Dictionary<byte, float>>.Read = (data, index) => ReadDict<byte, float>(data, index);
            ReadHelper<Dictionary<byte, double>>.Read = (data, index) => ReadDict<byte, double>(data, index);
            ReadHelper<Dictionary<byte, string>>.Read = (data, index) => ReadDict<byte, string>(data, index);

            ReadHelper<Dictionary<ushort, bool>>.Read = (data, index) => ReadDict<ushort, bool>(data, index);
            ReadHelper<Dictionary<ushort, sbyte>>.Read = (data, index) => ReadDict<ushort, sbyte>(data, index);
            ReadHelper<Dictionary<ushort, byte>>.Read = (data, index) => ReadDict<ushort, byte>(data, index);
            ReadHelper<Dictionary<ushort, ushort>>.Read = (data, index) => ReadDict<ushort, ushort>(data, index);
            ReadHelper<Dictionary<ushort, short>>.Read = (data, index) => ReadDict<ushort, short>(data, index);
            ReadHelper<Dictionary<ushort, uint>>.Read = (data, index) => ReadDict<ushort, uint>(data, index);
            ReadHelper<Dictionary<ushort, int>>.Read = (data, index) => ReadDict<ushort, int>(data, index);
            ReadHelper<Dictionary<ushort, ulong>>.Read = (data, index) => ReadDict<ushort, ulong>(data, index);
            ReadHelper<Dictionary<ushort, long>>.Read = (data, index) => ReadDict<ushort, long>(data, index);
            ReadHelper<Dictionary<ushort, float>>.Read = (data, index) => ReadDict<ushort, float>(data, index);
            ReadHelper<Dictionary<ushort, double>>.Read = (data, index) => ReadDict<ushort, double>(data, index);
            ReadHelper<Dictionary<ushort, string>>.Read = (data, index) => ReadDict<ushort, string>(data, index);

            ReadHelper<Dictionary<short, bool>>.Read = (data, index) => ReadDict<short, bool>(data, index);
            ReadHelper<Dictionary<short, sbyte>>.Read = (data, index) => ReadDict<short, sbyte>(data, index);
            ReadHelper<Dictionary<short, byte>>.Read = (data, index) => ReadDict<short, byte>(data, index);
            ReadHelper<Dictionary<short, ushort>>.Read = (data, index) => ReadDict<short, ushort>(data, index);
            ReadHelper<Dictionary<short, short>>.Read = (data, index) => ReadDict<short, short>(data, index);
            ReadHelper<Dictionary<short, uint>>.Read = (data, index) => ReadDict<short, uint>(data, index);
            ReadHelper<Dictionary<short, int>>.Read = (data, index) => ReadDict<short, int>(data, index);
            ReadHelper<Dictionary<short, ulong>>.Read = (data, index) => ReadDict<short, ulong>(data, index);
            ReadHelper<Dictionary<short, long>>.Read = (data, index) => ReadDict<short, long>(data, index);
            ReadHelper<Dictionary<short, float>>.Read = (data, index) => ReadDict<short, float>(data, index);
            ReadHelper<Dictionary<short, double>>.Read = (data, index) => ReadDict<short, double>(data, index);
            ReadHelper<Dictionary<short, string>>.Read = (data, index) => ReadDict<short, string>(data, index);

            ReadHelper<Dictionary<uint, bool>>.Read = (data, index) => ReadDict<uint, bool>(data, index);
            ReadHelper<Dictionary<uint, sbyte>>.Read = (data, index) => ReadDict<uint, sbyte>(data, index);
            ReadHelper<Dictionary<uint, byte>>.Read = (data, index) => ReadDict<uint, byte>(data, index);
            ReadHelper<Dictionary<uint, ushort>>.Read = (data, index) => ReadDict<uint, ushort>(data, index);
            ReadHelper<Dictionary<uint, short>>.Read = (data, index) => ReadDict<uint, short>(data, index);
            ReadHelper<Dictionary<uint, uint>>.Read = (data, index) => ReadDict<uint, uint>(data, index);
            ReadHelper<Dictionary<uint, int>>.Read = (data, index) => ReadDict<uint, int>(data, index);
            ReadHelper<Dictionary<uint, ulong>>.Read = (data, index) => ReadDict<uint, ulong>(data, index);
            ReadHelper<Dictionary<uint, long>>.Read = (data, index) => ReadDict<uint, long>(data, index);
            ReadHelper<Dictionary<uint, float>>.Read = (data, index) => ReadDict<uint, float>(data, index);
            ReadHelper<Dictionary<uint, double>>.Read = (data, index) => ReadDict<uint, double>(data, index);
            ReadHelper<Dictionary<uint, string>>.Read = (data, index) => ReadDict<uint, string>(data, index);

            ReadHelper<Dictionary<int, bool>>.Read = (data, index) => ReadDict<int, bool>(data, index);
            ReadHelper<Dictionary<int, sbyte>>.Read = (data, index) => ReadDict<int, sbyte>(data, index);
            ReadHelper<Dictionary<int, byte>>.Read = (data, index) => ReadDict<int, byte>(data, index);
            ReadHelper<Dictionary<int, ushort>>.Read = (data, index) => ReadDict<int, ushort>(data, index);
            ReadHelper<Dictionary<int, short>>.Read = (data, index) => ReadDict<int, short>(data, index);
            ReadHelper<Dictionary<int, uint>>.Read = (data, index) => ReadDict<int, uint>(data, index);
            ReadHelper<Dictionary<int, int>>.Read = (data, index) => ReadDict<int, int>(data, index);
            ReadHelper<Dictionary<int, ulong>>.Read = (data, index) => ReadDict<int, ulong>(data, index);
            ReadHelper<Dictionary<int, long>>.Read = (data, index) => ReadDict<int, long>(data, index);
            ReadHelper<Dictionary<int, float>>.Read = (data, index) => ReadDict<int, float>(data, index);
            ReadHelper<Dictionary<int, double>>.Read = (data, index) => ReadDict<int, double>(data, index);
            ReadHelper<Dictionary<int, string>>.Read = (data, index) => ReadDict<int, string>(data, index);

            ReadHelper<Dictionary<ulong, bool>>.Read = (data, index) => ReadDict<ulong, bool>(data, index);
            ReadHelper<Dictionary<ulong, sbyte>>.Read = (data, index) => ReadDict<ulong, sbyte>(data, index);
            ReadHelper<Dictionary<ulong, byte>>.Read = (data, index) => ReadDict<ulong, byte>(data, index);
            ReadHelper<Dictionary<ulong, ushort>>.Read = (data, index) => ReadDict<ulong, ushort>(data, index);
            ReadHelper<Dictionary<ulong, short>>.Read = (data, index) => ReadDict<ulong, short>(data, index);
            ReadHelper<Dictionary<ulong, uint>>.Read = (data, index) => ReadDict<ulong, uint>(data, index);
            ReadHelper<Dictionary<ulong, int>>.Read = (data, index) => ReadDict<ulong, int>(data, index);
            ReadHelper<Dictionary<ulong, ulong>>.Read = (data, index) => ReadDict<ulong, ulong>(data, index);
            ReadHelper<Dictionary<ulong, long>>.Read = (data, index) => ReadDict<ulong, long>(data, index);
            ReadHelper<Dictionary<ulong, float>>.Read = (data, index) => ReadDict<ulong, float>(data, index);
            ReadHelper<Dictionary<ulong, double>>.Read = (data, index) => ReadDict<ulong, double>(data, index);
            ReadHelper<Dictionary<ulong, string>>.Read = (data, index) => ReadDict<ulong, string>(data, index);

            ReadHelper<Dictionary<long, bool>>.Read = (data, index) => ReadDict<long, bool>(data, index);
            ReadHelper<Dictionary<long, sbyte>>.Read = (data, index) => ReadDict<long, sbyte>(data, index);
            ReadHelper<Dictionary<long, byte>>.Read = (data, index) => ReadDict<long, byte>(data, index);
            ReadHelper<Dictionary<long, ushort>>.Read = (data, index) => ReadDict<long, ushort>(data, index);
            ReadHelper<Dictionary<long, short>>.Read = (data, index) => ReadDict<long, short>(data, index);
            ReadHelper<Dictionary<long, uint>>.Read = (data, index) => ReadDict<long, uint>(data, index);
            ReadHelper<Dictionary<long, int>>.Read = (data, index) => ReadDict<long, int>(data, index);
            ReadHelper<Dictionary<long, ulong>>.Read = (data, index) => ReadDict<long, ulong>(data, index);
            ReadHelper<Dictionary<long, long>>.Read = (data, index) => ReadDict<long, long>(data, index);
            ReadHelper<Dictionary<long, float>>.Read = (data, index) => ReadDict<long, float>(data, index);
            ReadHelper<Dictionary<long, double>>.Read = (data, index) => ReadDict<long, double>(data, index);
            ReadHelper<Dictionary<long, string>>.Read = (data, index) => ReadDict<long, string>(data, index);

            ReadHelper<Dictionary<float, bool>>.Read = (data, index) => ReadDict<float, bool>(data, index);
            ReadHelper<Dictionary<float, sbyte>>.Read = (data, index) => ReadDict<float, sbyte>(data, index);
            ReadHelper<Dictionary<float, byte>>.Read = (data, index) => ReadDict<float, byte>(data, index);
            ReadHelper<Dictionary<float, ushort>>.Read = (data, index) => ReadDict<float, ushort>(data, index);
            ReadHelper<Dictionary<float, short>>.Read = (data, index) => ReadDict<float, short>(data, index);
            ReadHelper<Dictionary<float, uint>>.Read = (data, index) => ReadDict<float, uint>(data, index);
            ReadHelper<Dictionary<float, int>>.Read = (data, index) => ReadDict<float, int>(data, index);
            ReadHelper<Dictionary<float, ulong>>.Read = (data, index) => ReadDict<float, ulong>(data, index);
            ReadHelper<Dictionary<float, long>>.Read = (data, index) => ReadDict<float, long>(data, index);
            ReadHelper<Dictionary<float, float>>.Read = (data, index) => ReadDict<float, float>(data, index);
            ReadHelper<Dictionary<float, double>>.Read = (data, index) => ReadDict<float, double>(data, index);
            ReadHelper<Dictionary<float, string>>.Read = (data, index) => ReadDict<float, string>(data, index);

            ReadHelper<Dictionary<double, bool>>.Read = (data, index) => ReadDict<double, bool>(data, index);
            ReadHelper<Dictionary<double, sbyte>>.Read = (data, index) => ReadDict<double, sbyte>(data, index);
            ReadHelper<Dictionary<double, byte>>.Read = (data, index) => ReadDict<double, byte>(data, index);
            ReadHelper<Dictionary<double, ushort>>.Read = (data, index) => ReadDict<double, ushort>(data, index);
            ReadHelper<Dictionary<double, short>>.Read = (data, index) => ReadDict<double, short>(data, index);
            ReadHelper<Dictionary<double, uint>>.Read = (data, index) => ReadDict<double, uint>(data, index);
            ReadHelper<Dictionary<double, int>>.Read = (data, index) => ReadDict<double, int>(data, index);
            ReadHelper<Dictionary<double, ulong>>.Read = (data, index) => ReadDict<double, ulong>(data, index);
            ReadHelper<Dictionary<double, long>>.Read = (data, index) => ReadDict<double, long>(data, index);
            ReadHelper<Dictionary<double, float>>.Read = (data, index) => ReadDict<double, float>(data, index);
            ReadHelper<Dictionary<double, double>>.Read = (data, index) => ReadDict<double, double>(data, index);
            ReadHelper<Dictionary<double, string>>.Read = (data, index) => ReadDict<double, string>(data, index);

            ReadHelper<Dictionary<string, bool>>.Read = (data, index) => ReadDict<string, bool>(data, index);
            ReadHelper<Dictionary<string, sbyte>>.Read = (data, index) => ReadDict<string, sbyte>(data, index);
            ReadHelper<Dictionary<string, byte>>.Read = (data, index) => ReadDict<string, byte>(data, index);
            ReadHelper<Dictionary<string, ushort>>.Read = (data, index) => ReadDict<string, ushort>(data, index);
            ReadHelper<Dictionary<string, short>>.Read = (data, index) => ReadDict<string, short>(data, index);
            ReadHelper<Dictionary<string, uint>>.Read = (data, index) => ReadDict<string, uint>(data, index);
            ReadHelper<Dictionary<string, int>>.Read = (data, index) => ReadDict<string, int>(data, index);
            ReadHelper<Dictionary<string, ulong>>.Read = (data, index) => ReadDict<string, ulong>(data, index);
            ReadHelper<Dictionary<string, long>>.Read = (data, index) => ReadDict<string, long>(data, index);
            ReadHelper<Dictionary<string, float>>.Read = (data, index) => ReadDict<string, float>(data, index);
            ReadHelper<Dictionary<string, double>>.Read = (data, index) => ReadDict<string, double>(data, index);
            ReadHelper<Dictionary<string, string>>.Read = (data, index) => ReadDict<string, string>(data, index);

            #endregion
        }
        static readonly Encoding utf8;

        public static class ReadHelper<T>
        {
            public static Func<byte[], int, T> Read;
        }

        public static T Read<T>(byte[] data, int index) => ReadHelper<T>.Read(data, index);
        public static bool ReadBool(byte[] data, int index) => data[index] > 0;
        public static byte ReadByte(byte[] data, int index) => data[index];
        public static sbyte ReadSByte(byte[] data, int index) => (sbyte)data[index];
        public static byte[] ReadBytes(byte[] data, int index, int count)
        {
            var ret = new byte[count];
            Buffer.BlockCopy(data, index, ret, 0, count);
            return ret;
        }
        public static short ReadShort(byte[] data, int index) => FastBitConverter.ToInt16(data, index);
        public static ushort ReadUShort(byte[] data, int index) => FastBitConverter.ToUInt16(data, index);
        public static int ReadInt(byte[] data, int index) => FastBitConverter.ToInt32(data, index); 
        public static uint ReadUInt(byte[] data, int index) => FastBitConverter.ToUInt32(data, index);
        public static long ReadLong(byte[] data, int index) => FastBitConverter.ToInt64(data, index);
        public static ulong ReadULong(byte[] data, int index) => FastBitConverter.ToUInt64(data, index);
        public static float ReadFloat(byte[] data, int index) => FastBitConverter.ToSingle(data, index);
        public static double ReadDouble(byte[] data, int index) => FastBitConverter.ToDouble(data, index);
        public static string ReadString(byte[] data, int index, bool indexIsAddr = true)
        {
            if (indexIsAddr)
            {
                index = ReadInt(data, index);
                if (index < 0) return string.Empty;
            }
            var count = ReadUShort(data, index);
            index += 2;
            return utf8.GetString(data, index, count);
            //return FastBitConverter.ToString(data, index, count);
        }

        public static List<T> ReadList<T>(byte[] data, int index)
        {
            return ReadHelper<List<T>>.Read(data, index);
        }
        public static List<bool> ReadListBool(byte[] data, int index, bool indexIsAddr = true)
        {
            if (indexIsAddr)
            {
                index = ReadInt(data, index);
                if (index < 0) return new List<bool>();
            }
            ushort count = ReadUShort(data, index);
            index += 2;
            List<bool> ls = new List<bool>(count);
            for (ushort i = 0; i < count; i++)
            {
                ls.Add(ReadBool(data, index));
                index++;
            }
            return ls;
        }
        public static List<sbyte> ReadListSByte(byte[] data, int index, bool indexIsAddr = true)
        {
            if (indexIsAddr)
            {
                index = ReadInt(data, index);
                if (index < 0) return new List<sbyte>();
            }
            ushort count = ReadUShort(data, index);
            index += 2;
            List<sbyte> ls = new List<sbyte>(count);
            for (ushort i = 0; i < count; i++)
            {
                ls.Add(ReadSByte(data, index));
                index++;
            }
            return ls;
        }
        public static List<byte> ReadListByte(byte[] data, int index, bool indexIsAddr = true)
        {
            if (indexIsAddr)
            {
                index = ReadInt(data, index);
                if (index < 0) return new List<byte>();
            }
            ushort count = ReadUShort(data, index);
            index += 2;
            List<byte> ls = new List<byte>(count);
            for (ushort i = 0; i < count; i++)
            {
                ls.Add(ReadByte(data, index));
                index++;
            }
            return ls;
        }
        public static List<ushort> ReadListUShort(byte[] data, int index, bool indexIsAddr = true)
        {
            if (indexIsAddr)
            {
                index = ReadInt(data, index);
                if (index < 0) return new List<ushort>();
            }
            ushort count = ReadUShort(data, index);
            index += 2;
            List<ushort> ls = new List<ushort>(count);
            for (ushort i = 0; i < count; i++)
            {
                ls.Add(ReadUShort(data, index));
                index += 2;
            }
            return ls;
        }
        public static List<short> ReadListShort(byte[] data, int index, bool indexIsAddr = true)
        {
            if (indexIsAddr)
            {
                index = ReadInt(data, index);
                if (index < 0) return new List<short>();
            }
            ushort count = ReadUShort(data, index);
            index += 2;
            List<short> ls = new List<short>(count);
            for (ushort i = 0; i < count; i++)
            {
                ls.Add(ReadShort(data, index));
                index += 2;
            }
            return ls;
        }
        public static List<uint> ReadListUInt(byte[] data, int index, bool indexIsAddr = true)
        {
            if (indexIsAddr)
            {
                index = ReadInt(data, index);
                if (index < 0) return new List<uint>();
            }
            ushort count = ReadUShort(data, index);
            index += 2;
            List<uint> ls = new List<uint>(count);
            for (ushort i = 0; i < count; i++)
            {
                ls.Add(ReadUInt(data, index));
                index += 4;
            }
            return ls;
        }
        public static List<int> ReadListInt(byte[] data, int index, bool indexIsAddr = true)
        {
            if (indexIsAddr)
            {
                index = ReadInt(data, index);
                if (index < 0) return new List<int>();
            }
            ushort count = ReadUShort(data, index);
            index += 2;
            List<int> ls = new List<int>(count);
            for (ushort i = 0; i < count; i++)
            {
                ls.Add(ReadInt(data, index));
                index += 4;
            }
            return ls;
        }
        public static List<float> ReadListFloat(byte[] data, int index, bool indexIsAddr = true)
        {
            if (indexIsAddr)
            {
                index = ReadInt(data, index);
                if (index < 0) return new List<float>();
            }
            ushort count = ReadUShort(data, index);
            index += 2;
            List<float> ls = new List<float>(count);
            for (ushort i = 0; i < count; i++)
            {
                ls.Add(ReadFloat(data, index));
                index += 4;
            }
            return ls;
        }
        public static List<ulong> ReadListULong(byte[] data, int index, bool indexIsAddr = true)
        {
            if (indexIsAddr)
            {
                index = ReadInt(data, index);
                if (index < 0) return new List<ulong>();
            }
            ushort count = ReadUShort(data, index);
            index += 2;
            List<ulong> ls = new List<ulong>(count);
            for (ushort i = 0; i < count; i++)
            {
                ls.Add(ReadULong(data, index));
                index += 8;
            }
            return ls;
        }
        public static List<long> ReadListLong(byte[] data, int index, bool indexIsAddr = true)
        {
            if (indexIsAddr)
            {
                index = ReadInt(data, index);
                if (index < 0) return new List<long>();
            }
            ushort count = ReadUShort(data, index);
            index += 2;
            List<long> ls = new List<long>(count);
            for (ushort i = 0; i < count; i++)
            {
                ls.Add(ReadLong(data, index));
                index += 8;
            }
            return ls;
        }
        public static List<double> ReadListDouble(byte[] data, int index, bool indexIsAddr = true)
        {
            if (indexIsAddr)
            {
                index = ReadInt(data, index);
                if (index < 0) return new List<double>();
            }
            ushort count = ReadUShort(data, index);
            index += 2;
            List<double> ls = new List<double>(count);
            for (ushort i = 0; i < count; i++)
            {
                ls.Add(ReadDouble(data, index));
                index += 8;
            }
            return ls;
        }
        public static List<string> ReadListString(byte[] data, int index, bool indexIsAddr = true)
        {
            if (indexIsAddr)
            {
                index = ReadInt(data, index);
                if (index < 0) return new List<string>();
            }
            ushort count = ReadUShort(data, index);
            index += 2;
            List<string> ls = new List<string>(count);
            for (ushort i = 0; i < count; i++)
            {
                ushort len = ReadUShort(data, index);
                ls.Add(ReadString(data, index, false));
                index += len + 2;
            }
            return ls;
        }

        public static Dictionary<K, V> ReadDict<K, V>(byte[] data, int index, TypeToken keyToken, TypeToken valToken, bool indexIsAddr = true)
        {
            if (indexIsAddr)
            {
                index = ReadInt(data, index);
                if (index < 0) return new Dictionary<K, V>();
            }
            int count = ReadUShort(data, index);
            index += 2;
            Dictionary<K, V> dict = new Dictionary<K, V>(count);
            for (int i = 0; i < count; i++)
            {
                K key = keyToken == TypeToken.String ? (K)(object)ReadString(data, index, false) : ReadHelper<K>.Read(data, index);
                index += GetReadLen(keyToken, key);
                V val = valToken == TypeToken.String ? (V)(object)ReadString(data, index, false) : ReadHelper<V>.Read(data, index);
                index += GetReadLen(valToken, val);
                dict.Add(key, val);
            }
            return dict;
        }
        public static Dictionary<K, V> ReadDict<K, V>(byte[] data, int index, bool indexIsAddr = true)
        {
            TypeToken keyToken = GetTypeToken<K>();
            TypeToken valToken = GetTypeToken<V>();
            return ReadDict<K, V>(data, index, keyToken, valToken, indexIsAddr);
        }

        public static Vector2 ReadVector2(byte[] data, int index)
        {
            float x = ReadFloat(data, index);
            float y = ReadFloat(data, index + 4);
            return new Vector2(x, y);
        }
        public static Vector2Int ReadVector2Int(byte[] data, int index)
        {
            int x = ReadInt(data, index);
            int y = ReadInt(data, index + 4);
            return new Vector2Int(x, y);
        }
        public static Vector3 ReadVector3(byte[] data, int index)
        {
            float x = ReadFloat(data, index);
            float y = ReadFloat(data, index + 4);
            float z = ReadFloat(data, index + 8);
            return new Vector3(x, y, z);
        }
        public static Vector3Int ReadVector3Int(byte[] data, int index)
        {
            int x = ReadInt(data, index);
            int y = ReadInt(data, index + 4);
            int z = ReadInt(data, index + 8);
            return new Vector3Int(x, y, z);
        }
        public static Vector4 ReadVector4(byte[] data, int index)
        {
            float x = ReadFloat(data, index);
            float y = ReadFloat(data, index + 4);
            float z = ReadFloat(data, index + 8);
            float w = ReadFloat(data, index + 12);
            return new Vector4(x, y, z, w);
        }

        static int GetReadLen<T>(TypeToken token, T val)
        {
            switch (token)
            {
                case TypeToken.Bool:
                case TypeToken.Sbyte:
                case TypeToken.Byte:
                    return 1;
                case TypeToken.UShort:
                case TypeToken.Short:
                    return 2;
                case TypeToken.UInt:
                case TypeToken.Int:
                case TypeToken.Float:
                    return 4;
                case TypeToken.ULong:
                case TypeToken.Long:
                case TypeToken.Double:
                    return 8;
                case TypeToken.String:
                    return val.ToString().Length + 2; // +2是长度信息
                default: return 0;
            }
        }

        public static TypeToken GetTypeToken<T>()
        {
            Type t = typeof(T);
            if (t == typeof(bool)) return TypeToken.Bool;
            else if (t == typeof(sbyte)) return TypeToken.Sbyte;
            else if (t == typeof(byte)) return TypeToken.Byte;
            else if (t == typeof(ushort)) return TypeToken.UShort;
            else if (t == typeof(short)) return TypeToken.Short;
            else if (t == typeof(uint)) return TypeToken.UInt;
            else if (t == typeof(int)) return TypeToken.Int;
            else if (t == typeof(ulong)) return TypeToken.ULong;
            else if (t == typeof(long)) return TypeToken.Long;
            else if (t == typeof(float)) return TypeToken.Float;
            else if (t == typeof(double)) return TypeToken.Double;
            else if (t == typeof(string)) return TypeToken.String;
            else return TypeToken.Null;
        }
    }
}


public enum TypeToken
{
	Null = 0,
	Sbyte = 1,
	Byte = 2,
	Bool = 3,
	UShort = 4,
	Short = 5,
	UInt = 6,
	Int = 7,
	Float = 8,
	Double = 9,
	ULong = 10,
	Long = 11,
	String = 12,
	List = 100,
	Dictionary = 10000,
	Vector = 20000
}
