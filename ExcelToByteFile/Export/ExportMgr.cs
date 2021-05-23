using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace ExcelToByteFile
{
	public class ConfigDefine
	{
		public const int fileStreamMaxLen = 1024 * 1024 * 128;  // 最大128MB
        public const int sheetStreamMaxLen = 1024 * 1024 * 10;  // 最大10MB
		public const int rowStreamMaxLen = 1024 * 128;          // 最大128k
	}

	public class ExportMgr
    {
        static ByteBuffer fileBuffer = new ByteBuffer(ConfigDefine.fileStreamMaxLen);

        public static void Export(List<string> fileList)
        {
            List<FileData> fileDatas = new List<FileData>();
            // 加载选择的Excel文件列表
            for (int i = 0; i < fileList.Count; i++)
            {
                fileBuffer.Clear();
                string filePath = fileList[i];
                ExcelData excelFile = new ExcelData(filePath);
                if (excelFile.Load())
                {
                    FileData fileData = new FileData(excelFile);
                    fileDatas.Add(fileData);
                    int heapStart = fileData.GetAlignedDataTotalSize();
                    fileBuffer.SetHeapIndexStartPos(heapStart);

                    excelFile.Export();
                }
                excelFile.Dispose();
            }
        }

        public static void ExportOneSheet(string path, SheetData sheet)
        {
            for (int i = 0; i < sheet.rows.Count; i++)
            {
                RowData row = sheet.rows[i];
                // 写入数据
                for (int j = 0; j < sheet.heads.Count; j++)
                {
                    HeadData head = sheet.heads[j];
                    string value = row[j];
                    WriteCell(fileBuffer, head, value);
                }
            }

            // 创建文件
            string filePath = StringHelper.MakeSaveFullPath(path, $"{sheet.SheetName}.bytes");
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                byte[] data = fileBuffer.GetBuffer();
                int length = fileBuffer.ReadableBytes;
                fs.Write(data, 0, length);
            }
        }

		private static void WriteCell(ByteBuffer buffer, HeadData head, string value)
		{
			switch (head.Type)
            {
                case TypeDefine.sbyteType:
                    buffer.WriteSbyte(StringConvert.StringToValue<sbyte>(value));
                    break;
                case TypeDefine.uintType:
                    buffer.WriteUInt(StringConvert.StringToValue<uint>(value));
                    break;
                case TypeDefine.ulongType:
                    buffer.WriteULong(StringConvert.StringToValue<ulong>(value));
                    break;
                case TypeDefine.ushortType:
                    buffer.WriteUShort(StringConvert.StringToValue<ushort>(value));
                    break;
                case TypeDefine.boolType:
					buffer.WriteBool(StringConvert.StringToBool(value));
					break;
				case TypeDefine.byteType:
					buffer.WriteByte(StringConvert.StringToValue<byte>(value));
					break;
				case TypeDefine.shortType:
					buffer.WriteShort(StringConvert.StringToValue<short>(value));
					break;
				case TypeDefine.intType:
					buffer.WriteInt(StringConvert.StringToValue<int>(value));
					break;
				case TypeDefine.floatType:
					buffer.WriteFloat(StringConvert.StringToValue<float>(value));
					break;
				case TypeDefine.stringType:
					buffer.WriteString(value);
					break;
				case TypeDefine.longType:
					buffer.WriteLong(StringConvert.StringToValue<long>(value));
					break;
				case TypeDefine.doubleType:
					buffer.WriteDouble(StringConvert.StringToValue<double>(value));
					break;
				case TypeDefine.listType:
					WriteList(buffer, head.SubType[0], value);
					break;
                case TypeDefine.dictType:
                    WriteDict(buffer, head.SubType, value);
                    break;
				default: throw new Exception($"Not support head type {head.Type}");
			}
		}

		private static void WriteList(ByteBuffer buffer, string subType, string value)
        {
			switch(subType)
            {
                case TypeDefine.sbyteType:
                    buffer.WriteListSByte(StringConvert.StringToValueList<sbyte>(value, ConstDefine.splitChar));
                    break;
                case TypeDefine.uintType:
                    buffer.WriteListUInt(StringConvert.StringToValueList<uint>(value, ConstDefine.splitChar));
                    break;
                case TypeDefine.ulongType:
                    buffer.WriteListULong(StringConvert.StringToValueList<ulong>(value, ConstDefine.splitChar));
                    break;
                case TypeDefine.ushortType:
                    buffer.WriteListUShort(StringConvert.StringToValueList<ushort>(value, ConstDefine.splitChar));
                    break;
                case TypeDefine.boolType:
                    buffer.WriteListBool(StringConvert.StringToValueList<bool>(value, ConstDefine.splitChar));
                    break;
                case TypeDefine.byteType:
                    buffer.WriteListByte(StringConvert.StringToValueList<byte>(value, ConstDefine.splitChar));
                    break;
                case TypeDefine.shortType:
                    buffer.WriteListShort(StringConvert.StringToValueList<short>(value, ConstDefine.splitChar));
                    break;
                case TypeDefine.intType:
                    buffer.WriteListInt(StringConvert.StringToValueList<int>(value, ConstDefine.splitChar));
                    break;
                case TypeDefine.floatType:
                    buffer.WriteListFloat(StringConvert.StringToValueList<float>(value, ConstDefine.splitChar)); break;
                case TypeDefine.stringType:
                    buffer.WriteListString(StringConvert.StringToStringList(value, ConstDefine.splitChar)); break;
                case TypeDefine.longType:
                    buffer.WriteListLong(StringConvert.StringToValueList<long>(value, ConstDefine.splitChar)); break;
                case TypeDefine.doubleType:
                    buffer.WriteListDouble(StringConvert.StringToValueList<double>(value, ConstDefine.splitChar)); break;
            }
        }

        private static void WriteDict(ByteBuffer buffer, string[] subType, string value)
        {

        }

	}
}
