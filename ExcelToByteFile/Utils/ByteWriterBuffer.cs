using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

namespace ExcelToByteFile
{
	public static class WriteHelper<T>
    {
		public static Action<T, bool> Write;
    }

	/// <summary>
	/// 字节写入缓冲区
	/// </summary>
	public class ByteWriterBuffer
	{
		private readonly byte[] buffer;
		private int alignIndex = 0;
		private int nonAlignIndex = 0;  // 类似于引用类型存于堆空间中，固定长度的地址存放在对齐空间，实际变长的内容存于非对齐空间
		private int nonAlignStart = 0;	// 非对齐空间的起始位置

		/// <summary>
		/// 字节缓冲区
		/// </summary>
		public ByteWriterBuffer(int capacity)
		{
			buffer = new byte[capacity];

            WriteHelper<bool>.Write = (ls, b) => WriteBool(ls, b);
            WriteHelper<sbyte>.Write = (ls, b) => WriteSbyte(ls, b);
            WriteHelper<byte>.Write = (ls, b) => WriteByte(ls, b);
            WriteHelper<ushort>.Write = (ls, b) => WriteUShort(ls, b);
            WriteHelper<short>.Write = (ls, b) => WriteShort(ls, b);
            WriteHelper<uint>.Write = (ls, b) => WriteUInt(ls, b);
            WriteHelper<int>.Write = (ls, b) => WriteInt(ls, b);
            WriteHelper<float>.Write = (ls, b) => WriteFloat(ls, b);
            WriteHelper<ulong>.Write = (ls, b) => WriteULong(ls, b);
            WriteHelper<long>.Write = (ls, b) => WriteLong(ls, b);
            WriteHelper<double>.Write = (ls, b) => WriteDouble(ls, b);
            WriteHelper<string>.Write = (ls, b) => WriteString(ls, b);

            WriteHelper<List<bool>>.Write = (ls, b) => WriteListBool(ls, b);
            WriteHelper<List<sbyte>>.Write = (ls, b) => WriteListSbyte(ls, b);
            WriteHelper<List<byte>>.Write = (ls, b) => WriteListByte(ls, b);
            WriteHelper<List<ushort>>.Write = (ls, b) => WriteListUShort(ls, b);
            WriteHelper<List<short>>.Write = (ls, b) => WriteListShort(ls, b);
            WriteHelper<List<uint>>.Write = (ls, b) => WriteListUInt(ls, b);
            WriteHelper<List<int>>.Write = (ls, b) => WriteListInt(ls, b);
            WriteHelper<List<float>>.Write = (ls, b) => WriteListFloat(ls, b);
            WriteHelper<List<ulong>>.Write = (ls, b) => WriteListULong(ls, b);
            WriteHelper<List<long>>.Write = (ls, b) => WriteListLong(ls, b);
            WriteHelper<List<double>>.Write = (ls, b) => WriteListDouble(ls, b);
            WriteHelper<List<string>>.Write = (ls, b) => WriteListString(ls, b);
        }

        /// <summary>
        /// 缓冲区容量
        /// </summary>
        public int Capacity => buffer.Length;

        /// <summary>
        /// 当前可读数据量
        /// </summary>
        public int ReadableBytes => nonAlignIndex;

        /// <summary>
        /// 写入的下标位置
        /// </summary>
        public int WriterIndex => alignIndex;

		/// <summary>
		/// 获取数据
		/// </summary>
		public byte[] GetBuffer()
		{
			return buffer;
		}

		/// <summary>
		/// 清空缓冲区
		/// </summary>
		public void Clear()
		{
			alignIndex = 0;
		}

		/// <summary>
		/// 设置引用类型索引起始位置
		/// </summary>
		/// <param name="pos"></param>
		public void SetNonAlignStartPos(int pos)
        {
			nonAlignIndex = pos;
			nonAlignStart = pos;
		}

		#region 写入操作

		[Conditional("DEBUG")]
		private void CheckWriterIndex(int length, bool writeToNonAlign = false)
		{
			if (writeToNonAlign)
            {
				if (nonAlignIndex + length > Capacity)
                {
                    StackTrace st = new StackTrace(true);
                    Log.Error("错误！超出容量限制：" + st.ToString());
                }
			}
			else if (alignIndex + length > nonAlignStart)
			{
                StackTrace st = new StackTrace(true);
                Log.Error("错误！超出对齐字节的范围: " + st.ToString());
            }
		}

		public void WriteBytes(byte[] data, bool writeToNonAlign = false)
		{
			WriteBytes(data, 0, data.Length, writeToNonAlign);
		}
		public void WriteBytes(byte[] data, int offset, int count, bool writeToNonAlign = false)
		{
			if (count <= 0) return;
			CheckWriterIndex(count, writeToNonAlign);
			if (writeToNonAlign)
            {
				Buffer.BlockCopy(data, offset, buffer, nonAlignIndex, count);
				nonAlignIndex += count;
			}
			else
            {
				Buffer.BlockCopy(data, offset, buffer, alignIndex, count);
				alignIndex += count;
			}
		}
		public void WriteBool(bool value, bool writeToNonAlign = false)
		{
			WriteByte((byte)(value ? 1 : 0), writeToNonAlign);
		}
		public void WriteByte(byte value, bool writeToNonAlign = false)
		{
			CheckWriterIndex(1, writeToNonAlign);
			if (writeToNonAlign) buffer[nonAlignIndex++] = value;
			else buffer[alignIndex++] = value;
		}
		public void WriteSbyte(sbyte value, bool writeToNonAlign = false)
		{
			// 注意：从sbyte强转到byte不会有数据变化或丢失
			WriteByte((byte)value, writeToNonAlign);
		}
		public void WriteShort(short value, bool writeToNonAlign = false)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			WriteBytes(bytes, writeToNonAlign);
		}
		public void WriteUShort(ushort value, bool writeToNonAlign = false)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			WriteBytes(bytes, writeToNonAlign);
		}
		public void WriteInt(int value, bool writeToNonAlign = false)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			WriteBytes(bytes, writeToNonAlign);
		}
		public void WriteUInt(uint value, bool writeToNonAlign = false)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			WriteBytes(bytes, writeToNonAlign);
		}
		public void WriteLong(long value, bool writeToNonAlign = false)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			WriteBytes(bytes, writeToNonAlign);
		}
		public void WriteULong(ulong value, bool writeToNonAlign = false)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			WriteBytes(bytes, writeToNonAlign);
		}
		public void WriteFloat(float value, bool writeToNonAlign = false)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			WriteBytes(bytes, writeToNonAlign);
		}
		public void WriteDouble(double value, bool writeToNonAlign = false)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			WriteBytes(bytes, writeToNonAlign);
		}
		public void WriteString(string value, bool onlywriteToNonAlign = false)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			int num = bytes.Length; // + 1; // 注意：字符串末尾写入结束符
			if (num > ushort.MaxValue)
				throw new FormatException($"String length cannot be greater than {ushort.MaxValue} !");
			if (!onlywriteToNonAlign)
            {
				if (bytes.Length == 0)
                {
					WriteInt(-1);
					return;
				}
				WriteInt(nonAlignIndex);
			}
			WriteUShort(Convert.ToUInt16(num), true);
			WriteBytes(bytes, true);
		}
        /// <summary>
        /// string类型第二个参数为onlywriteToNonAlign
        /// </summary>
        public void Write<T>(T val, bool writeToNonAlign = false)
        {
            WriteHelper<T>.Write(val, writeToNonAlign);
        }

		public void WriteListBool(List<bool> ls, bool onlywriteToNonAlign = false)
        {
			int count = 0;
			if (ls != null) count = ls.Count;
            if (!onlywriteToNonAlign)
            {
                if (count == 0)
                {
                    WriteInt(-1);
                    return;
                }
                WriteInt(nonAlignIndex);
            }
            WriteUShort((ushort)count, true);
            for (int i = 0; i < count; i++)
            {
                WriteBool(ls[i], true);
            }
        }
		public void WriteListByte(List<byte> ls, bool onlywriteToNonAlign = false)
		{
			int count = 0;
			if (ls != null) count = ls.Count;
            if (!onlywriteToNonAlign)
            {
                if (count == 0)
                {
                    WriteInt(-1);
                    return;
                }
                WriteInt(nonAlignIndex);
            }
            WriteUShort((ushort)count, true);
            for (int i = 0; i < count; i++)
            {
                WriteByte(ls[i], true);
            }
        }
		public void WriteListShort(List<short> ls, bool onlywriteToNonAlign = false)
        {
			int count = 0;
			if (ls != null) count = ls.Count;
            if (!onlywriteToNonAlign)
            {
                if (count == 0)
                {
                    WriteInt(-1);
                    return;
                }
                WriteInt(nonAlignIndex);
            }
            WriteUShort((ushort)count, true);
            for (int i = 0; i < count; i++)
            {
                WriteShort(ls[i], true);
            }
        }
		public void WriteListInt(List<int> ls, bool onlywriteToNonAlign = false)
        {
			int count = 0;
			if (ls != null) count = ls.Count;
            if(!onlywriteToNonAlign)
            {
                if (count == 0)
                {
                    WriteInt(-1);
                    return;
                }
                WriteInt(nonAlignIndex);
            }
            WriteUShort((ushort)count, true);
            for (int i = 0; i < count; i++)
            {
                WriteInt(ls[i], true);
            }
        }
		public void WriteListFloat(List<float> ls, bool onlywriteToNonAlign = false)
        {
			int count = 0;
			if (ls != null) count = ls.Count;
            if (!onlywriteToNonAlign)
            {
                if (count == 0)
                {
                    WriteInt(-1);
                    return;
                }
                WriteInt(nonAlignIndex);
            }
            WriteUShort((ushort)count, true);
            for (int i = 0; i < count; i++)
            {
                WriteFloat(ls[i], true);
            }
        }
		public void WriteListLong(List<long> ls, bool onlywriteToNonAlign = false)
        {
			int count = 0;
			if (ls != null) count = ls.Count;
            if (!onlywriteToNonAlign)
            {
                if (count == 0)
                {
                    WriteInt(-1);
                    return;
                }
                WriteInt(nonAlignIndex);
            }
            WriteUShort((ushort)count, true);
            for (int i = 0; i < count; i++)
            {
                WriteLong(ls[i], true);
            }
        }
		public void WriteListDouble(List<double> ls, bool onlywriteToNonAlign = false)
        {
			int count = 0;
			if (ls != null) count = ls.Count;
            if (!onlywriteToNonAlign)
            {
                if (count == 0)
                {
                    WriteInt(-1);
                    return;
                }
                WriteInt(nonAlignIndex);
            }
            WriteUShort((ushort)count, true);
            for (int i = 0; i < count; i++)
            {
                WriteDouble(ls[i], true);
            }
        }
		public void WriteListSbyte(List<sbyte> ls, bool onlywriteToNonAlign = false)
        {
			int count = 0;
			if (ls != null) count = ls.Count;
            if (!onlywriteToNonAlign)
            {
                if (count == 0)
                {
                    WriteInt(-1);
                    return;
                }
                WriteInt(nonAlignIndex);
            }
            WriteUShort((ushort)count, true);
            for (int i = 0; i < count; i++)
            {
                WriteSbyte(ls[i], true);
            }
        }
		public void WriteListUInt(List<uint> ls, bool onlywriteToNonAlign = false)
        {
			int count = 0;
			if (ls != null) count = ls.Count;
            if (!onlywriteToNonAlign)
            {
                if (count == 0)
                {
                    WriteInt(-1);
                    return;
                }
                WriteInt(nonAlignIndex);
            }
            WriteUShort((ushort)count, true);
            for (int i = 0; i < count; i++)
            {
                WriteUInt(ls[i], true);
            }
        }
		public void WriteListULong(List<ulong> ls, bool onlywriteToNonAlign = false)
        {
			int count = 0;
			if (ls != null) count = ls.Count;
            if (!onlywriteToNonAlign)
            {
                if (count == 0)
                {
                    WriteInt(-1);
                    return;
                }
                WriteInt(nonAlignIndex);
            }
            WriteUShort((ushort)count, true);
            for (int i = 0; i < count; i++)
            {
                WriteULong(ls[i], true);
            }
        }
		public void WriteListUShort(List<ushort> ls, bool onlywriteToNonAlign = false)
        {
			int count = 0;
			if (ls != null) count = ls.Count;
            if (!onlywriteToNonAlign)
            {
                if (count == 0)
                {
                    WriteInt(-1);
                    return;
                }
                WriteInt(nonAlignIndex);
            }
            WriteUShort((ushort)count, true);
            for (int i = 0; i < count; i++)
            {
                WriteUShort(ls[i], true);
            }
        }
		public void WriteListString(List<string> ls, bool onlywriteToNonAlign = false)
        {
			int count = 0;
			if (ls != null) count = ls.Count;
            if (!onlywriteToNonAlign)
            {
                if (count == 0)
                {
                    WriteInt(-1);
                    return;
                }
                WriteInt(nonAlignIndex);
            }
            WriteUShort((ushort)count, true);
            for (int i = 0; i < count; i++)
            {
                WriteString(ls[i], true);
            }
        }
		public void WriteList<T>(List<T> ls, bool onlywriteToNonAlign = false)
        {
			WriteHelper<List<T>>.Write(ls, onlywriteToNonAlign);
        }

		public void WriteVec2Int(Vector2Int vec)
        {
			WriteInt(vec.x);
			WriteInt(vec.y);
		}
		public void WriteVec2(Vector2 vec)
		{
			WriteFloat(vec.x);
			WriteFloat(vec.y);
		}
		public void WriteVec3Int(Vector3Int vec)
		{
			WriteInt(vec.x);
			WriteInt(vec.y);
			WriteInt(vec.z);
		}
		public void WriteVec3(Vector3 vec)
		{
			WriteFloat(vec.x);
			WriteFloat(vec.y);
			WriteFloat(vec.z);
		}
		public void WriteVec4Int(Vector4Int vec)
		{
			WriteInt(vec.x);
			WriteInt(vec.y);
			WriteInt(vec.z);
			WriteInt(vec.w);
		}
		public void WriteVec4(Vector4 vec)
		{
			WriteFloat(vec.x);
			WriteFloat(vec.y);
			WriteFloat(vec.z);
			WriteFloat(vec.w);
		}

		public void WriteDict(Dictionary<string, string> dict, string keyType, string valType, bool onlywriteToNonAlign = false)
        {
			int count = 0;
			if (dict != null) count = dict.Count;
            if (!onlywriteToNonAlign)
            {
                if (count == 0)
                {
                    WriteInt(-1);
                    return;
                }
                WriteInt(nonAlignIndex);
            }
            WriteUShort((ushort)count, true);   // 先写入长度
            //int keyLen = DataTypeHelper.GetBaseTypeLen(keyType);
            //int valLen = DataTypeHelper.GetBaseTypeLen(valType);
            //int KeyStart = _heapIndex;
            //int valStart = KeyStart + keyLen * count;
            //int realIndex = valStart + valLen * count;
            //int num = 0;
            foreach (var pair in dict)
            {
                //_heapIndex = KeyStart + num * keyLen;
                WriteBaseTypeToNonAlign(pair.Key, keyType);
                //_heapIndex = valStart + num * valLen;
                WriteBaseTypeToNonAlign(pair.Value, valType);
                //num++;
            }
        }

		private void WriteBaseTypeToNonAlign(string value, string type)
        {
			switch (type)
			{
				case TypeDef.boolType:
					WriteBool(StringConvert.ToBool(value), true);
					break;
				case TypeDef.sbyteType:
					WriteSbyte(Convert.ToSByte(value), true);
					break;
				case TypeDef.byteType:
					WriteByte(Convert.ToByte(value), true);
					break;
				case TypeDef.ushortType:
					WriteUShort(Convert.ToUInt16(value), true);
					break;
				case TypeDef.shortType:
					WriteShort(Convert.ToInt16(value), true);
					break;
				case TypeDef.uintType:
					WriteUInt(Convert.ToUInt32(value), true);
					break;
				case TypeDef.intType:
					WriteInt(Convert.ToInt32(value), true);
					break;
				case TypeDef.floatType:
					WriteFloat(Convert.ToSingle(value), true);
					break;
				case TypeDef.ulongType:
					WriteULong(Convert.ToUInt64(value), true);
					break;
				case TypeDef.longType:
					WriteLong(Convert.ToInt64(value), true);
					break;
				case TypeDef.doubleType:
					WriteDouble(Convert.ToDouble(value), true);
					break;
				case TypeDef.stringType:
					WriteString(value, true);
					break;
			}
		}

		#endregion
	}
}