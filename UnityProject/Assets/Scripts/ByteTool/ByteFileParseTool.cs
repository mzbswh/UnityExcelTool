using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class ByteFileParseTool
{
	public static class ReadHelper<T>
    {
		public static Func<byte[], int, T> Read;
		public static Func<byte[], int, List<T>> ReadList;
    }

	public static void Init()
    {
		ReadHelper<bool>.Read = (data, index) => { return ReadBool(data, index); };
		ReadHelper<sbyte>.Read = (data, index) => { return ReadSByte(data, index); };
		ReadHelper<byte>.Read = (data, index) => { return ReadByte(data, index); };
		ReadHelper<ushort>.Read = (data, index) => { return ReadUShort(data, index); };
		ReadHelper<short>.Read = (data, index) => { return ReadShort(data, index); };
		ReadHelper<uint>.Read = (data, index) => { return ReadUInt(data, index); };
		ReadHelper<int>.Read = (data, index) => { return ReadInt(data, index); };
		ReadHelper<ulong>.Read = (data, index) => { return ReadULong(data, index); };
		ReadHelper<long>.Read = (data, index) => { return ReadLong(data, index); };
		ReadHelper<float>.Read = (data, index) => { return ReadFloat(data, index); };
		ReadHelper<double>.Read = (data, index) => { return ReadDouble(data, index); };
		ReadHelper<string>.Read = (data, index) => { return ReadString(data, index); };

		ReadHelper<bool>.ReadList = (data, index) => { return ReadListBool(data, index); };
		ReadHelper<sbyte>.ReadList = (data, index) => { return ReadListSByte(data, index); };
		ReadHelper<byte>.ReadList = (data, index) => { return ReadListByte(data, index); };
		ReadHelper<ushort>.ReadList = (data, index) => { return ReadListUShort(data, index); };
		ReadHelper<short>.ReadList = (data, index) => { return ReadListShort(data, index); };
		ReadHelper<uint>.ReadList = (data, index) => { return ReadListUInt(data, index); };
		ReadHelper<int>.ReadList = (data, index) => { return ReadListInt(data, index); };
		ReadHelper<ulong>.ReadList = (data, index) => { return ReadListULong(data, index); };
		ReadHelper<long>.ReadList = (data, index) => { return ReadListLong(data, index); };
		ReadHelper<float>.ReadList = (data, index) => { return ReadListFloat(data, index); };
		ReadHelper<double>.ReadList = (data, index) => { return ReadListDouble(data, index); };
		ReadHelper<string>.ReadList = (data, index) => { return ReadListString(data, index); };
	}

	public static T Read<T>(byte[] data, int index)
    {
		return ReadHelper<T>.Read(data, index);
    }
	public static bool ReadBool(byte[] data, int index)
    {
		return data[index] == 1;
	}
	public static byte ReadByte(byte[] data, int index)
    {
		return data[index];
    }
	public static sbyte ReadSByte(byte[] data, int index)
    {
		return (sbyte)data[index];
    }
    public static byte[] ReadBytes(byte[] data, int index, int count)
	{
		var ret = new byte[count];
		Buffer.BlockCopy(data, index, ret, 0, count);
		return ret;
	}
	public static short ReadShort(byte[] data, int index)
	{
		return BitConverter.ToInt16(data, index);
	}
	public static ushort ReadUShort(byte[] data, int index)
	{
		return BitConverter.ToUInt16(data, index);
	}
	public static int ReadInt(byte[] data, int index)
	{
		return BitConverter.ToInt32(data, index);
	}
	public static uint ReadUInt(byte[] data, int index)
	{
		return BitConverter.ToUInt32(data, index);
	}
	public static long ReadLong(byte[] data, int index)
	{
		return BitConverter.ToInt64(data, index);
	}
	public static ulong ReadULong(byte[] data, int index)
	{
		return BitConverter.ToUInt64(data, index);
	}
	public static float ReadFloat(byte[] data, int index)
	{
		return BitConverter.ToSingle(data, index);
	}
	public static double ReadDouble(byte[] data, int index)
	{
		return BitConverter.ToDouble(data, index);
	}
	public static string ReadString(byte[] data, int index, bool indexIsAddr = true)
	{
		if (indexIsAddr) index = ReadInt(data, index);
		int count = ReadUShort(data, index);
		index += 2;
		return Encoding.UTF8.GetString(data, index, count);
	}

	public static List<T> ReadList<T>(byte[] data, int index)
    {
		return ReadHelper<T>.ReadList(data, index);
    }
	public static List<bool> ReadListBool(byte[] data, int index)
    {
		index = ReadInt(data, index);
		int count = ReadUShort(data, index);
		index += 2;
		List<bool> ls = new List<bool>(count);
		for (int i = 0; i < count; i++)
        {
			ls.Add(ReadBool(data, index));
			index++;
        }
		return ls;
    }
	public static List<sbyte> ReadListSByte(byte[] data, int index)
	{
		index = ReadInt(data, index);
		int count = ReadUShort(data, index);
		index += 2;
		List<sbyte> ls = new List<sbyte>(count);
		for (int i = 0; i < count; i++)
		{
			ls.Add(ReadSByte(data, index));
			index++;
		}
		return ls;
	}
	public static List<byte> ReadListByte(byte[] data, int index)
	{
		index = ReadInt(data, index);
		int count = ReadUShort(data, index);
		index += 2;
		List<byte> ls = new List<byte>(count);
		for (int i = 0; i < count; i++)
		{
			ls.Add(ReadByte(data, index));
			index++;
		}
		return ls;
	}
	public static List<ushort> ReadListUShort(byte[] data, int index)
	{
		index = ReadInt(data, index);
		int count = ReadUShort(data, index);
		index += 2;
		List<ushort> ls = new List<ushort>(count);
		for (int i = 0; i < count; i++)
		{
			ls.Add(ReadUShort(data, index));
			index += 2;
		}
		return ls;
	}
	public static List<short> ReadListShort(byte[] data, int index)
	{
		index = ReadInt(data, index);
		int count = ReadUShort(data, index);
		index += 2;
		List<short> ls = new List<short>(count);
		for (int i = 0; i < count; i++)
		{
			ls.Add(ReadShort(data, index));
			index += 2;
		}
		return ls;
	}
	public static List<uint> ReadListUInt(byte[] data, int index)
	{
		index = ReadInt(data, index);
		int count = ReadUShort(data, index);
		index += 2;
		List<uint> ls = new List<uint>(count);
		for (int i = 0; i < count; i++)
		{
			ls.Add(ReadUInt(data, index));
			index += 4;
		}
		return ls;
	}
	public static List<int> ReadListInt(byte[] data, int index)
	{
		index = ReadInt(data, index);
		int count = ReadUShort(data, index);
		index += 2;
		List<int> ls = new List<int>(count);
		for (int i = 0; i < count; i++)
		{
			ls.Add(ReadInt(data, index));
			index += 4;
		}
		return ls;
	}
	public static List<float> ReadListFloat(byte[] data, int index)
	{
		index = ReadInt(data, index);
		int count = ReadUShort(data, index);
		index += 2;
		List<float> ls = new List<float>(count);
		for (int i = 0; i < count; i++)
		{
			ls.Add(ReadFloat(data, index));
			index += 4;
		}
		return ls;
	}
	public static List<ulong> ReadListULong(byte[] data, int index)
	{
		index = ReadInt(data, index);
		int count = ReadUShort(data, index);
		index += 2;
		List<ulong> ls = new List<ulong>(count);
		for (int i = 0; i < count; i++)
		{
			ls.Add(ReadULong(data, index));
			index += 8;
		}
		return ls;
	}
	public static List<long> ReadListLong(byte[] data, int index)
	{
		index = ReadInt(data, index);
		int count = ReadUShort(data, index);
		index += 2;
		List<long> ls = new List<long>(count);
		for (int i = 0; i < count; i++)
		{
			ls.Add(ReadLong(data, index));
			index += 8;
		}
		return ls;
	}
	public static List<double> ReadListDouble(byte[] data, int index)
	{
		index = ReadInt(data, index);
		int count = ReadUShort(data, index);
		index += 2;
		List<double> ls = new List<double>(count);
		for (int i = 0; i < count; i++)
		{
			ls.Add(ReadDouble(data, index));
			index += 8;
		}
		return ls;
	}
	public static List<string> ReadListString(byte[] data, int index)
	{
		index = ReadInt(data, index);
		int count = ReadUShort(data, index);
		index += 2;
		List<string> ls = new List<string>(count);
		for (int i = 0; i < count; i++)
		{
			int len = ReadUShort(data, index);
			ls.Add(ReadString(data, index, false));
			index += len + 2;
		}
		return ls;
	}

	public static Dictionary<K, V> ReadDict<K, V>(byte[] data, int index, TypeToken keyToken, TypeToken valToken)
    {
		Dictionary<K, V> dict = new Dictionary<K, V>();
		index = ReadInt(data, index);
		int count = ReadUShort(data, index);
		index += 2;
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
	public static Dictionary<K, V> ReadDict<K, V>(byte[] data, int index)
	{
		TypeToken keyToken = GetTypeToken<K>();
		TypeToken valToken = GetTypeToken<V>();
		return ReadDict<K, V>(data, index, keyToken, valToken);
	}

	static int GetReadLen<T>(TypeToken token, T val)
    {
		switch(token)
        {
			case TypeToken.Bool: 
			case TypeToken.SByte:
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

	static TypeToken GetTypeToken<T>()
    {
		Type t = typeof(T);
		if (t == typeof(bool)) return TypeToken.Bool;
		else if (t == typeof(sbyte)) return TypeToken.SByte;
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

public enum TypeToken
{
	Null = 0,
	SByte = 1,
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
	Dictionary = 10000
}