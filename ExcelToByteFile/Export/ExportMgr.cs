using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;

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
        static ByteWriteBuffer fileBuffer = new ByteWriteBuffer(ConfigDefine.fileStreamMaxLen);
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
                if (!Program.IsCommandLine)
                {

                    Program.mainForm?.Invoke(new Action(() => { Program.mainForm.SetProgress(i + 1, "正在生成：" + excelFile.ExcelName); }));
                }
                   
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
                    Log.LogError($"{excelFile.ExcelName} 加载错误");
                }
                
            }
            // 导出文件信息
            ExportManifest(fileDatas);
            // 导出cs定义文件
            ExportExcelDefineCSCode(GlobalConfig.Ins.codeFileOutputDir, fileDatas);
        }

        public static void ExportOneSheet(string path, SheetData sheet)
        {
            Log.LogConsole($"正在生成 {sheet.GetExportFileName()}.bytes {sheet.rows.Count}行 {sheet.heads.Count}列 ...");
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
            string filePath = StringHelper.MakeFullPath(path + Path.DirectorySeparatorChar, $"{sheet.GetExportFileName()}.bytes");
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                byte[] data = fileBuffer.GetBuffer();
                int length = fileBuffer.ReadableBytes;
                fs.Write(data, 0, length);
            }
        }

        private static void ExportManifest(List<FileInfoData> fileDatas)
        {
            Log.LogConsole("正在生成 manifest.bytes ...");
            string path = GlobalConfig.Ins.byteFileOutputDir;
            fileBuffer.Clear();
            // 写入信息
            fileBuffer.SetHeapIndexStartPos(28 * fileDatas.Count + 4);    // 文件信息内容是固定的
            fileBuffer.WriteInt(fileDatas.Count);   // 先写入总个数
            for (int i = 0, len = fileDatas.Count; i < len; i++)
            {
                FileInfoData data = fileDatas[i];
                fileBuffer.WriteString(data.FileName);
                fileBuffer.WriteInt(data.IdColIndex);
                fileBuffer.WriteInt((int)data.RowCount);
                fileBuffer.WriteInt((int)data.RowLength);
                fileBuffer.WriteListInt(data.Tokens);
                fileBuffer.WriteListInt(data.ColOffset);
                fileBuffer.WriteListString(data.VariableNames);
            }

            // 创建文件
            string filePath = StringHelper.MakeFullPath(path, "manifest.bytes");
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                byte[] data = fileBuffer.GetBuffer();
                int length = fileBuffer.ReadableBytes;
                fs.Write(data, 0, length);
            }
        }

        private static void ExportExcelDefineCSCode(string path, List<FileInfoData> data)
        {
            Log.LogConsole("正在生成 ExcelDefine.cs ...");
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
                    sb2.Append(@"   ///<summary>");
                    sb2.Append($"主列: " + info.VariableNames[info.IdColIndex] +
                        $" [{info.GetTypeByToken(info.Tokens[info.IdColIndex])}]");
                    sb2.AppendLine(@"</summary>");
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
                                varName = raw + "_c_" + a.ToString();
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
                        int val = (j << 16) + info.ColOffset[j];
                        sb1.AppendLine(varName + @" = " + val.ToString() + ";");
                    }
                    sb1.AppendLine(@"   }");
                }
                sb1.AppendLine(@"}");
                sb2.Append(@"}");
                sw.Write(sb1.ToString());
                sw.Write(sb2.ToString());
            }
        }

		private static void WriteCell(ByteWriteBuffer buffer, HeadData head, string value)
		{
			switch (head.MainType)
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
                case TypeDefine.vecType:
                    WriteVect(buffer, head.SubType, value);
                    break;
				default: 
                    Log.LogError($"不支持的类型 {head.MainType}");
                    break;
			}
		}

		private static void WriteList(ByteWriteBuffer buffer, string subType, string value)
        {
			switch(subType)
            {
                case TypeDefine.sbyteType:
                    buffer.WriteListSByte(StringConvert.StringToList<sbyte>(value, ConstDefine.splitChar));
                    break;
                case TypeDefine.uintType:
                    buffer.WriteListUInt(StringConvert.StringToList<uint>(value, ConstDefine.splitChar));
                    break;
                case TypeDefine.ulongType:
                    buffer.WriteListULong(StringConvert.StringToList<ulong>(value, ConstDefine.splitChar));
                    break;
                case TypeDefine.ushortType:
                    buffer.WriteListUShort(StringConvert.StringToList<ushort>(value, ConstDefine.splitChar));
                    break;
                case TypeDefine.boolType:
                    buffer.WriteListBool(StringConvert.StringToList<bool>(value, ConstDefine.splitChar));
                    break;
                case TypeDefine.byteType:
                    buffer.WriteListByte(StringConvert.StringToList<byte>(value, ConstDefine.splitChar));
                    break;
                case TypeDefine.shortType:
                    buffer.WriteListShort(StringConvert.StringToList<short>(value, ConstDefine.splitChar));
                    break;
                case TypeDefine.intType:
                    buffer.WriteListInt(StringConvert.StringToList<int>(value, ConstDefine.splitChar));
                    break;
                case TypeDefine.floatType:
                    buffer.WriteListFloat(StringConvert.StringToList<float>(value, ConstDefine.splitChar)); break;
                case TypeDefine.stringType:
                    buffer.WriteListString(StringConvert.StringToStringList(value)); break;
                case TypeDefine.longType:
                    buffer.WriteListLong(StringConvert.StringToList<long>(value, ConstDefine.splitChar)); break;
                case TypeDefine.doubleType:
                    buffer.WriteListDouble(StringConvert.StringToList<double>(value, ConstDefine.splitChar)); break;
            }
        }

        private static void WriteDict(ByteWriteBuffer buffer, string[] subType, string value)
        {
            var dict = StringConvert.StringToDict<string, string>(value);
            buffer.WriteDict(dict, subType[0], subType[1]);
        }

        private static void WriteVect(ByteWriteBuffer buffer, string[] subType, string value)
        {
            int dimen = int.Parse(subType[0]);
            if (dimen == 2)
            {
                if (subType[1] == TypeDefine.intType)
                {
                    Vector2Int vec = StringConvert.StringToVec2Int(value);
                    buffer.WriteVec2Int(vec);
                }
                else
                {
                    Vector2 vec = StringConvert.StringToVec2(value);
                    buffer.WriteVec2(vec);
                }
            }
            else if (dimen == 3)
            {
                if (subType[1] == TypeDefine.intType)
                {
                    Vector3Int vec = StringConvert.StringToVec3Int(value);
                    buffer.WriteVec3Int(vec);
                }
                else
                {
                    Vector3 vec = StringConvert.StringToVec3(value);
                    buffer.WriteVec3(vec);
                }
            }
            else
            {
                if (subType[1] == TypeDefine.intType)
                {
                    Vector4Int vec = StringConvert.StringToVec4Int(value);
                    buffer.WriteVec4Int(vec);
                }
                else
                {
                    Vector4 vec = StringConvert.StringToVec4(value);
                    buffer.WriteVec4(vec);
                }
            }
        }
	}
}
