using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelToByteFile
{
    class ByteUtil
    {
		#region 从字节读取数据
		/// <summary>
		/// 检查从起始索引开始是否能够读取n个字节的数据
		/// </summary>
		/// <param name="length"></param>
		private void CheckReaderIndex(List<byte> data, int startIndex, int length)
		{
			if (startIndex + length > data.Count)
			{
				throw new IndexOutOfRangeException();
			}
		}

		public byte[] ReadBytes(byte[] srcData, int startIndex, int count)
		{
			var data = new byte[count];
			Buffer.BlockCopy(srcData, startIndex, data, 0, count);
			return data;
		}
		public bool ReadBool(byte[] data, int startIndex)
		{
			return data[startIndex] == 1;
		}
		public byte ReadByte(byte[] data, int startIndex)
		{
			return data[startIndex];
		}
		public sbyte ReadSbyte(byte[] data, int startIndex)
		{
			return (sbyte)ReadByte(data, startIndex);
		}
		public short ReadShort(byte[] data, int startIndex)
		{
			short result = BitConverter.ToInt16(data, startIndex);
			return result;
		}
		public ushort ReadUShort(byte[] data, int startIndex)
		{
			ushort result = BitConverter.ToUInt16(data, startIndex);
			return result;
		}
		public int ReadInt(byte[] data, int startIndex)
		{
			int result = BitConverter.ToInt32(data, startIndex);
			return result;
		}
		public uint ReadUInt(byte[] data, int startIndex)
		{
			uint result = BitConverter.ToUInt32(data, startIndex);
			return result;
		}
		public long ReadLong(byte[] data, int startIndex)
		{
			long result = BitConverter.ToInt64(data, startIndex);
			return result;
		}
		public ulong ReadULong(byte[] data, int startIndex)
		{
			ulong result = BitConverter.ToUInt64(data, startIndex);
			return result;
		}
		public float ReadFloat(byte[] data, int startIndex)
		{
			float result = BitConverter.ToSingle(data, startIndex);
			return result;
		}
		public double ReadDouble(byte[] data, int startIndex)
		{
			double result = BitConverter.ToDouble(data, startIndex);
			return result;
		}
		public string ReadString(byte[] data, int startIndex, int length)
		{
			string result = Encoding.UTF8.GetString(data, startIndex, length); // 注意：读取的时候忽略字符串末尾写入结束符
			return result;
		}


		public List<int> ReadList_Int(byte[] data, int startIndex, int count)
		{
			List<int> ret = new List<int>(count);
			for (int i = 0; i < count; i++)
			{
				ret.Add(ReadInt(data, startIndex + (i * 4)));
			}
			return ret;
		}
		public List<long> ReadList_Long(byte[] data, int startIndex, int count)
		{
			List<long> result = new List<long>();
			for (int i = 0; i < count; i++)
			{
				result.Add(ReadLong(data, startIndex + (i * 8)));
			}
			return result;
		}
		public List<float> ReadList_Float(byte[] data, int startIndex, int count)
		{
			List<float> result = new List<float>();
			for (int i = 0; i < count; i++)
			{
				result.Add(ReadFloat(data, startIndex + (i * 4)));
			}
			return result;
		}
		public List<double> ReadListDouble(byte[] data, int startIndex, int count)
		{
			List<double> result = new List<double>();
			for (int i = 0; i < count; i++)
			{
				result.Add(ReadDouble(data, startIndex + (i * 8)));
			}
			return result;
		}
		public List<string> ReadListString(byte[] data, int startIndex, int count)
		{
			List<string> result = new List<string>();
			for (int i = 0; i < count; i++)
			{
				//result.Add(ReadUTF());
			}
			return result;
		}

        //public Vector2 ReadVector2()
        //{
        //	float x = ReadFloat();
        //	float y = ReadFloat();
        //	return new Vector2(x, y);
        //}
        //public Vector3 ReadVector3()
        //{
        //	float x = ReadFloat();
        //	float y = ReadFloat();
        //	float z = ReadFloat();
        //	return new Vector3(x, y, z);
        //}
        //public Vector4 ReadVector4()
        //{
        //	float x = ReadFloat();
        //	float y = ReadFloat();
        //	float z = ReadFloat();
        //	float w = ReadFloat();
        //	return new Vector4(x, y, z, w);
        //}
        #endregion

        #region 将数据转为字节
		//public void WriteBytes(byte[] data)
		//{
		//	WriteBytes(data, 0, data.Length);
		//}
		//public void WriteBytes(byte[] data, int offset, int count)
		//{
		//	Buffer.BlockCopy(data, offset, _buffer, _writerIndex, count);
		//	_writerIndex += count;
		//}
		public byte FromBool(bool value)
		{
			return ((byte)(value ? 1 : 0));
		}
		public byte FromSbyte(sbyte value)
		{
			// 注意：从sbyte强转到byte不会有数据变化或丢失
			return ((byte)value);
		}
		public byte[] FromShort(short value)
		{
			return BitConverter.GetBytes(value);
		}
		public byte[] FormUShort(ushort value)
		{
			return BitConverter.GetBytes(value);
		}
		public byte[] FromInt(int value)
		{
			return BitConverter.GetBytes(value);
		}
		public byte[] FromUInt(uint value)
		{
			return BitConverter.GetBytes(value);
		}
		public byte[] FromLong(long value)
		{
			return BitConverter.GetBytes(value);
		}
		public byte[] FromULong(ulong value)
		{
			return BitConverter.GetBytes(value);
		}
		public byte[] FromFloat(float value)
		{
			return BitConverter.GetBytes(value);
		}
		public byte[] FromDouble(double value)
		{
			return BitConverter.GetBytes(value);
		}
		//public void FromString(string value)
		//{
		//	byte[] bytes = Encoding.UTF8.GetBytes(value);
		//	int num = bytes.Length; // 注意：字符串末尾写入结束符
		//	if (num > ushort.MaxValue)
		//		throw new FormatException($"String length cannot be greater than {ushort.MaxValue} !");
		//	FormUShort(Convert.ToUInt16(num));
		//	WriteBytes(bytes);
		//	WriteByte((byte)'\0');
		//}

		//public void FromList_Int(List<int> values)
		//{
		//	int count = 0;
		//	if (values != null)
		//		count = values.Count;

		//	FromInt(count);
		//	for (int i = 0; i < count; i++)
		//	{
		//		FromInt(values[i]);
		//	}
		//}
		//public void WriteListLong(List<long> values)
		//{
		//	int count = 0;
		//	if (values != null)
		//		count = values.Count;

		//	FromInt(count);
		//	for (int i = 0; i < count; i++)
		//	{
		//		FromLong(values[i]);
		//	}
		//}
		//public void WriteListFloat(List<float> values)
		//{
		//	int count = 0;
		//	if (values != null)
		//		count = values.Count;

		//	FromInt(count);
		//	for (int i = 0; i < count; i++)
		//	{
		//		WriteFloat(values[i]);
		//	}
		//}
		//public void WriteListDouble(List<double> values)
		//{
		//	int count = 0;
		//	if (values != null)
		//		count = values.Count;

		//	FromInt(count);
		//	for (int i = 0; i < count; i++)
		//	{
		//		FromDouble(values[i]);
		//	}
		//}
		//public void WriteListUTF(List<string> values)
		//{
		//	int count = 0;
		//	if (values != null)
		//		count = values.Count;

		//	FromInt(count);
		//	for (int i = 0; i < count; i++)
		//	{
		//		WriteUTF(values[i]);
		//	}
		//}

		//public void WriteVector2(Vector2 value)
		//{
		//	WriteFloat(value.x);
		//	WriteFloat(value.y);
		//}
		//public void WriteVector3(Vector3 value)
		//{
		//	WriteFloat(value.x);
		//	WriteFloat(value.y);
		//	WriteFloat(value.z);
		//}
		//public void WriteVector4(Vector4 value)
		//{
		//	WriteFloat(value.x);
		//	WriteFloat(value.y);
		//	WriteFloat(value.z);
		//	WriteFloat(value.w);
		//}
		#endregion

		/// <summary>
		/// 大小端转换
		/// </summary>
		public static void ReverseOrder(byte[] data)
		{
			ReverseOrder(data, 0, data.Length);
		}
		public static void ReverseOrder(byte[] data, int offset, int length)
		{
			if (length <= 1)
				return;

			int end = offset + length - 1;
			int max = offset + length / 2;
			byte temp;
			for (int index = offset; index < max; index++, end--)
			{
				temp = data[end];
				data[end] = data[index];
				data[index] = temp;
			}
		}
	}
}
