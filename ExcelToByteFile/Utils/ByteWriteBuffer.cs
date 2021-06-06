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
	public class ByteWriteBuffer
	{
		private readonly byte[] _buffer;
		private int _stackIndex = 0;
		private int _heapIndex = 0;     // 模拟c#引用堆，存储引用类型实际数据
		private int _heapStart = 0;		// 堆的起始索引

		/// <summary>
		/// 字节缓冲区
		/// </summary>
		public ByteWriteBuffer(int capacity)
		{
			_buffer = new byte[capacity];
			InitWriteListHelper();
		}

		/// <summary>
		/// 缓冲区容量
		/// </summary>
		public int Capacity
		{
			get { return _buffer.Length; }
		}

		/// <summary>
		/// 当前可读数据量
		/// </summary>
		public int ReadableBytes
		{
			get { return _heapIndex; }
		}

		/// <summary>
		/// 写入的下标位置
		/// </summary>
		public int WriterIndex
		{
			get { return _stackIndex; }
		}

		private void InitWriteListHelper()
		{
			WriteListHelper<bool>.WriteList = (ls) => { WriteListBool(ls); };
			WriteListHelper<sbyte>.WriteList = (ls) => { WriteListSByte(ls); };
			WriteListHelper<byte>.WriteList = (ls) => { WriteListByte(ls); };
			WriteListHelper<ushort>.WriteList = (ls) => { WriteListUShort(ls); };
			WriteListHelper<short>.WriteList = (ls) => { WriteListShort(ls); };
			WriteListHelper<uint>.WriteList = (ls) => { WriteListUInt(ls); };
			WriteListHelper<int>.WriteList = (ls) => { WriteListInt(ls); };
			WriteListHelper<float>.WriteList = (ls) => { WriteListFloat(ls); };
			WriteListHelper<ulong>.WriteList = (ls) => { WriteListULong(ls); };
			WriteListHelper<long>.WriteList = (ls) => { WriteListLong(ls); };
			WriteListHelper<double>.WriteList = (ls) => { WriteListDouble(ls); };
			WriteListHelper<string>.WriteList = (ls) => { WriteListString(ls); };
		}

		/// <summary>
		/// 获取数据
		/// </summary>
		public byte[] GetBuffer()
		{
			return _buffer;
		}

		/// <summary>
		/// 清空缓冲区
		/// </summary>
		public void Clear()
		{
			_stackIndex = 0;
		}

		/// <summary>
		/// 设置引用类型索引起始位置
		/// </summary>
		/// <param name="pos"></param>
		public void SetHeapIndexStartPos(int pos)
        {
			_heapIndex = pos;
			_heapStart = pos;
		}

		#region 写入操作
		[Conditional("DEBUG")]
		private void CheckWriterIndex(int length, bool writeToHeap = false)
		{
			if (writeToHeap)
            {
				if (_heapIndex + length > Capacity) 
					throw new IndexOutOfRangeException();
			}
			else if (_stackIndex + length > _heapStart)
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
			CheckWriterIndex(count);
			if (writeToHeap)
            {
				Buffer.BlockCopy(data, offset, _buffer, _heapIndex, count);
				_heapIndex += count;
			}
			else
            {
				Buffer.BlockCopy(data, offset, _buffer, _stackIndex, count);
				_stackIndex += count;
			}
		}
		public void WriteBool(bool value, bool writeToHeap = false)
		{
			WriteByte((byte)(value ? 1 : 0), writeToHeap);
		}
		public void WriteByte(byte value, bool writeToHeap = false)
		{
			CheckWriterIndex(1);
			if (writeToHeap) _buffer[_heapIndex++] = value;
			else _buffer[_stackIndex++] = value;
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

			if (!onlyWriteToHeap) WriteInt(_heapIndex);	// 写入堆索引地址
			WriteUShort(Convert.ToUInt16(num), true);
			WriteBytes(bytes, true);
			//WriteByte((byte)'\0', true);
		}

		public void WriteListBool(List<bool> ls)
        {
			int count = 0;
			if (ls != null) count = ls.Count;
			WriteInt(_heapIndex);
			WriteUShort((ushort)count, true);
			for (int i = 0; i < count; i++)
			{
				WriteBool(ls[i], true);
			}
		}
		public void WriteListByte(List<byte> ls)
		{
			int count = 0;
			if (ls != null) count = ls.Count;
			WriteInt(_heapIndex);
			WriteUShort((ushort)count, true);
			for (int i = 0; i < count; i++)
			{
				WriteByte(ls[i], true);
			}
		}
		public void WriteListShort(List<short> ls)
		{
			int count = 0;
			if (ls != null) count = ls.Count;
			WriteInt(_heapIndex);
			WriteUShort((ushort)count, true);
			for (int i = 0; i < count; i++)
			{
				WriteShort(ls[i], true);
			}
		}
		public void WriteListInt(List<int> ls)
		{
			int count = 0;
			if (ls != null) count = ls.Count;
			WriteInt(_heapIndex);
			WriteUShort((ushort)count, true);
			for (int i = 0; i < count; i++)
			{
				WriteInt(ls[i], true);
			}
		}
		public void WriteListFloat(List<float> ls)
		{
			int count = 0;
			if (ls != null) count = ls.Count;
			WriteInt(_heapIndex);
			WriteUShort((ushort)count, true);
			for (int i = 0; i < count; i++)
			{
				WriteFloat(ls[i], true);
			}
		}
		public void WriteListLong(List<long> ls)
		{
			int count = 0;
			if (ls != null) count = ls.Count;
			WriteInt(_heapIndex);
			WriteUShort((ushort)count, true);
			for (int i = 0; i < count; i++)
			{
				WriteLong(ls[i], true);
			}
		}
		public void WriteListDouble(List<double> ls)
		{
			int count = 0;
			if (ls != null) count = ls.Count;
			WriteInt(_heapIndex);
			WriteUShort((ushort)count, true);
			for (int i = 0; i < count; i++)
			{
				WriteDouble(ls[i], true);
			}
		}
		public void WriteListSByte(List<sbyte> ls)
		{
			int count = 0;
			if (ls != null) count = ls.Count;
			WriteInt(_heapIndex);
			WriteUShort((ushort)count, true);
			for (int i = 0; i < count; i++)
			{
				WriteSbyte(ls[i], true);
			}
		}
		public void WriteListUInt(List<uint> ls)
		{
			int count = 0;
			if (ls != null) count = ls.Count;
			WriteInt(_heapIndex);
			WriteUShort((ushort)count, true);
			for (int i = 0; i < count; i++)
			{
				WriteUInt(ls[i], true);
			}
		}
		public void WriteListULong(List<ulong> ls)
		{
			int count = 0;
			if (ls != null) count = ls.Count;
			WriteInt(_heapIndex);
			WriteUShort((ushort)count, true);
			for (int i = 0; i < count; i++)
			{
				WriteULong(ls[i], true);
			}
		}
		public void WriteListUShort(List<ushort> ls)
		{
			int count = 0;
			if (ls != null) count = ls.Count;
			WriteInt(_heapIndex);
			WriteUShort((ushort)count, true);
			for (int i = 0; i < count; i++)
			{
				WriteUShort(ls[i], true);
			}
		}
		public void WriteListString(List<string> ls)
		{
			int count = 0;
			if (ls != null) count = ls.Count;
			WriteInt(_heapIndex);   // list地址
			WriteUShort((ushort)count, true);

			/*int lsStart = _heapIndex;      // 存储列表开始索引（第一个元素所在位置）
            _heapIndex += count * 4;
            int realHeapIndex = _heapIndex;// 此时指向第一个要写入的字符串的位置
            for (int i = 0; i < count; i++)
            {
                _heapIndex = lsStart + i * 4;  // 先写入地址
                WriteUInt((uint)_heapIndex, true);
                _heapIndex = realHeapIndex;
                WriteString(ls[i], true);
                realHeapIndex = _heapIndex;    // 暂存此时的堆位置
            }*/

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

			WriteInt(_heapIndex);
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

		private void WriteBaseTypeToHeap(string value, string type)
        {
			switch (type)
			{
				case TypeDefine.boolType:
					WriteBool(Convert.ToBoolean(value), true);
					break;
				case TypeDefine.sbyteType:
					WriteSbyte(Convert.ToSByte(value), true);
					break;
				case TypeDefine.byteType:
					WriteByte(Convert.ToByte(value), true);
					break;
				case TypeDefine.ushortType:
					WriteUShort(Convert.ToUInt16(value), true);
					break;
				case TypeDefine.shortType:
					WriteShort(Convert.ToInt16(value), true);
					break;
				case TypeDefine.uintType:
					WriteUInt(Convert.ToUInt32(value), true);
					break;
				case TypeDefine.intType:
					WriteInt(Convert.ToInt32(value), true);
					break;
				case TypeDefine.floatType:
					WriteFloat(Convert.ToSingle(value), true);
					break;
				case TypeDefine.ulongType:
					WriteULong(Convert.ToUInt64(value), true);
					break;
				case TypeDefine.longType:
					WriteLong(Convert.ToInt64(value), true);
					break;
				case TypeDefine.doubleType:
					WriteDouble(Convert.ToDouble(value), true);
					break;
				case TypeDefine.stringType:
					WriteString(value, true);
					break;
			}
		}

		#endregion

	}
}