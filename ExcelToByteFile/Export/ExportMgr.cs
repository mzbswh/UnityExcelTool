using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Windows.Forms;

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
        static List<FileInfoData> fileDatas = new List<FileInfoData>();

        public static void Export(List<string> fileList)
        {
            // 每次导出都会存储配置文件
            GlobalConfig.Ins.SaveConfig();

            fileDatas.Clear();
            // 加载选择的Excel文件列表
            for (int i = 0; i < fileList.Count; i++)
            {
                fileBuffer.Clear();
                string filePath = fileList[i];
                ExcelData excelFile = new ExcelData(filePath);
                try
                {
                    if (excelFile.Load())
                    {
                        excelFile.Export();
                    }
                    excelFile.Dispose();
                }
                catch
                {
                    Environment.Exit(1);
                }
                
            }
            // 导出文件信息
            ExportManifest(fileDatas);
            // 导出cs定义文件
            ExportExcelDefineCSCode(GlobalConfig.Ins.codeFileOutputDir, fileDatas);
            Log.LogNormal("生成完成！");
        }

        public static void ExportOneSheet(string path, SheetData sheet)
        {
            Log.LogNormal($"正在生成 {sheet.GetExportFileName()}.bytes {sheet.rows.Count}行 {sheet.heads.Count}列 ...");
            FileInfoData fileData = new FileInfoData(sheet);
            fileDatas.Add(fileData);
            int heapStart = fileData.FileLength;
            fileBuffer.SetHeapIndexStartPos(heapStart); // 设置引用类型起始地址

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
            string filePath = StringHelper.MakeSaveFullPath(path + Path.DirectorySeparatorChar, $"{sheet.GetExportFileName()}.bytes");
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                byte[] data = fileBuffer.GetBuffer();
                int length = fileBuffer.ReadableBytes;
                fs.Write(data, 0, length);
            }
        }

        private static void ExportManifest(List<FileInfoData> fileDatas)
        {
            Log.LogNormal("正在生成 manifest.bytes ...");
            string path = GlobalConfig.Ins.byteFileOutputDir;
            fileBuffer.Clear();
            // 写入信息
            fileBuffer.SetHeapIndexStartPos(28 * fileDatas.Count + 4);    // 文件信息内容是固定的
            fileBuffer.WriteInt(fileDatas.Count);   // 先写入总个数
            for (int i = 0, len = fileDatas.Count; i < len; i++)
            {
                FileInfoData data = fileDatas[i];
                fileBuffer.WriteString(data.FileName);
                fileBuffer.WriteInt((int)data.RowCount);
                fileBuffer.WriteInt((int)data.RowLength);
                fileBuffer.WriteListInt(data.ColOffset);
                fileBuffer.WriteListInt(data.Tokens);
                //fileBuffer.WriteListString(data.VariableNames);
                //fileBuffer.WriteListString(data.Comments);
            }

            // 创建文件
            string filePath = StringHelper.MakeSaveFullPath(path, "manifest.bytes");
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                byte[] data = fileBuffer.GetBuffer();
                int length = fileBuffer.ReadableBytes;
                fs.Write(data, 0, length);
            }
        }

        private static void ExportExcelDefineCSCode(string path, List<FileInfoData> data)
        {
            Log.LogNormal("正在生成 ExcelDefine.cs ...");
            using (StreamWriter sw = new StreamWriter(
                path + Path.DirectorySeparatorChar + "ExcelDefine" + ".cs", 
                false, 
                new UTF8Encoding(false)))
            {
                StringBuilder sb1 = new StringBuilder();    // 类变量
                StringBuilder sb2 = new StringBuilder();    // 文件枚举
                sb1.AppendLine(@"public sealed class ExcelVariableDef");
                sb1.AppendLine(@"{");
                sb2.AppendLine(@"public enum ExcelName");
                sb2.AppendLine(@"{");
                for (int i = 0; i < data.Count; i++)
                {
                    FileInfoData info = data[i];
                    sb1.Append(@"   public sealed class ");
                    sb1.AppendLine(info.FileName);
                    sb1.AppendLine(@"   {");
                    sb2.AppendLine(@"   " + info.FileName + @" = " + i.ToString() + @",");
                    for (int j = 0; j < info.VariableNames.Count; j++)
                    {
                        string varName = info.VariableNames[j];
                        string raw = varName;
                        if (varName == info.FileName)
                        {
                            bool b = true;
                            int a = 0;
                            while(b)
                            {
                                varName = raw + a.ToString();
                                if (!info.VariableNames.Contains(varName))
                                {
                                    b = false;
                                }
                                else a++;
                            }
                        }
                        sb1.Append(@"       /// <summary>");
                        string type = info.GetTypeByToken(info.Tokens[j]);
                        sb1.Append($"[{type}] " + info.Comments[j]);
                        sb1.AppendLine(@"</summary>");
                        sb1.Append(@"       public const int ");
                        sb1.AppendLine(varName + @" = " + j.ToString() + ";");
                    }
                    sb1.AppendLine(@"   }");
                }
                sb1.AppendLine(@"}");
                sb2.Append(@"}");
                sw.Write(sb1.ToString());
                sw.Write(sb2.ToString());
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
				default: 
                    MessageBox.Show($"Not support head type {head.Type}");
                    Environment.Exit(1);
                    break;
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
                    buffer.WriteListString(StringConvert.StringToStringList(value)); break;
                case TypeDefine.longType:
                    buffer.WriteListLong(StringConvert.StringToValueList<long>(value, ConstDefine.splitChar)); break;
                case TypeDefine.doubleType:
                    buffer.WriteListDouble(StringConvert.StringToValueList<double>(value, ConstDefine.splitChar)); break;
            }
        }

        private static void WriteDict(ByteBuffer buffer, string[] subType, string value)
        {
            var dict = StringConvert.StringToDict<string, string>(value);
            buffer.WriteDict(dict, subType[0], subType[1]);
        }
	}
}
