using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace ExcelToByteFile
{
	public static class WriteListHelper<T>
    {
		public static Action<List<T>> WriteList;
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
            WriteListHelper<bool>.WriteList = (ls) => WriteListBool(ls);
            WriteListHelper<sbyte>.WriteList = (ls) => WriteListSbyte(ls);
            WriteListHelper<byte>.WriteList = (ls) => WriteListByte(ls);
            WriteListHelper<ushort>.WriteList = (ls) => WriteListUShort(ls);
            WriteListHelper<short>.WriteList = (ls) => WriteListShort(ls);
            WriteListHelper<uint>.WriteList = (ls) => WriteListUInt(ls);
            WriteListHelper<int>.WriteList = (ls) => WriteListInt(ls);
            WriteListHelper<float>.WriteList = (ls) => WriteListFloat(ls);
            WriteListHelper<ulong>.WriteList = (ls) => WriteListULong(ls);
            WriteListHelper<long>.WriteList = (ls) => WriteListLong(ls);
            WriteListHelper<double>.WriteList = (ls) => WriteListDouble(ls);
            WriteListHelper<string>.WriteList = (ls) => WriteListString(ls);
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
		public void SetHeapIndexStartPos(int pos)
        {
			nonAlignIndex = pos;
			nonAlignStart = pos;
		}

		#region 写入操作

		[Conditional("DEBUG")]
		private void CheckWriterIndex(int length, bool writeToHeap = false)
		{
			if (writeToHeap)
            {
				if (nonAlignIndex + length > Capacity) 
					throw new IndexOutOfRangeException();
			}
			else if (alignIndex + length > nonAlignStart)
			{
				throw new IndexOutOfRangeException();
			}
		}

		public void WriteBytes(byte[] data, bool writeToHeap = false)
		{
			WriteBytes(data, 0, data.Length, writeToHeap);
		}
		public void WriteBytes(byte[] data, int offset, int count, bool writeToHeap = false)
		{
			if (count <= 0) return;
			CheckWriterIndex(count);
			if (writeToHeap)
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
		public void WriteBool(bool value, bool writeToHeap = false)
		{
			WriteByte((byte)(value ? 1 : 0), writeToHeap);
		}
		public void WriteByte(byte value, bool writeToHeap = false)
		{
			CheckWriterIndex(1);
			if (writeToHeap) buffer[nonAlignIndex++] = value;
			else buffer[alignIndex++] = value;
		}
		public void WriteSbyte(sbyte value, bool writeToHeap = false)
		{
			// 注意：从sbyte强转到byte不会有数据变化或丢失
			WriteByte((byte)value, writeToHeap);
		}
		public void WriteShort(short value, bool writeToHeap = false)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			WriteBytes(bytes, writeToHeap);
		}
		public void WriteUShort(ushort value, bool writeToHeap = false)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			WriteBytes(bytes, writeToHeap);
		}
		public void WriteInt(int value, bool writeToHeap = false)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			WriteBytes(bytes, writeToHeap);
		}
		public void WriteUInt(uint value, bool writeToHeap = false)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			WriteBytes(bytes, writeToHeap);
		}
		public void WriteLong(long value, bool writeToHeap = false)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			WriteBytes(bytes, writeToHeap);
		}
		public void WriteULong(ulong value, bool writeToHeap = false)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			WriteBytes(bytes, writeToHeap);
		}
		public void WriteFloat(float value, bool writeToHeap = false)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			WriteBytes(bytes, writeToHeap);
		}
		public void WriteDouble(double value, bool writeToHeap = false)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			WriteBytes(bytes, writeToHeap);
		}
		public void WriteString(string value, bool onlyWriteToHeap = false)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			int num = bytes.Length; // + 1; // 注意：字符串末尾写入结束符
			if (num > ushort.MaxValue)
				throw new FormatException($"String length cannot be greater than {ushort.MaxValue} !");
			if (!onlyWriteToHeap)
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

		public void WriteListBool(List<bool> ls, bool onlyWriteToHeap = false)
        {
			int count = 0;
			if (ls != null) count = ls.Count;
            if (!onlyWriteToHeap)
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
		public void WriteListByte(List<byte> ls, bool onlyWriteToHeap = false)
		{
			int count = 0;
			if (ls != null) count = ls.Count;
            if (!onlyWriteToHeap)
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
		public void WriteListShort(List<short> ls, bool onlyWriteToHeap = false)
        {
			int count = 0;
			if (ls != null) count = ls.Count;
            if (!onlyWriteToHeap)
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
		public void WriteListInt(List<int> ls, bool onlyWriteToHeap = false)
        {
			int count = 0;
			if (ls != null) count = ls.Count;
            if(!onlyWriteToHeap)
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
		public void WriteListFloat(List<float> ls, bool onlyWriteToHeap = false)
        {
			int count = 0;
			if (ls != null) count = ls.Count;
            if (!onlyWriteToHeap)
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
		public void WriteListLong(List<long> ls, bool onlyWriteToHeap = false)
        {
			int count = 0;
			if (ls != null) count = ls.Count;
            if (!onlyWriteToHeap)
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
		public void WriteListDouble(List<double> ls, bool onlyWriteToHeap = false)
        {
			int count = 0;
			if (ls != null) count = ls.Count;
            if (!onlyWriteToHeap)
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
		public void WriteListSbyte(List<sbyte> ls, bool onlyWriteToHeap = false)
        {
			int count = 0;
			if (ls != null) count = ls.Count;
            if (!onlyWriteToHeap)
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
		public void WriteListUInt(List<uint> ls, bool onlyWriteToHeap = false)
        {
			int count = 0;
			if (ls != null) count = ls.Count;
            if (!onlyWriteToHeap)
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
		public void WriteListULong(List<ulong> ls, bool onlyWriteToHeap = false)
        {
			int count = 0;
			if (ls != null) count = ls.Count;
            if (!onlyWriteToHeap)
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
		public void WriteListUShort(List<ushort> ls, bool onlyWriteToHeap = false)
        {
			int count = 0;
			if (ls != null) count = ls.Count;
            if (!onlyWriteToHeap)
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
		public void WriteListString(List<string> ls, bool onlyWriteToHeap = false)
        {
			int count = 0;
			if (ls != null) count = ls.Count;
            if (!onlyWriteToHeap)
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
		public void WriteList<T>(List<T> ls)
        {
			WriteListHelper<T>.WriteList(ls);
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

		public void WriteDict(Dictionary<string, string> dict, string keyType, string valType)
        {
			int count = 0;
			if (dict != null) count = dict.Count;
			if (count == 0) WriteInt(-1);
			else
            {
				WriteInt(nonAlignIndex);
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
					WriteBaseTypeToHeap(pair.Key, keyType);
					//_heapIndex = valStart + num * valLen;
					WriteBaseTypeToHeap(pair.Value, valType);
					//num++;
				}
			}
		}

		private void WriteBaseTypeToHeap(string value, string type)
        {
			switch (type)
			{
				case TypeDef.boolType:
					WriteBool(Convert.ToBoolean(value), true);
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