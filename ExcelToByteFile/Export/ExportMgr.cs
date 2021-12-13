using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace ExcelToByteFile
{

	public class ExportMgr
    {
        static readonly ByteWriterBuffer fileBuffer = new ByteWriterBuffer(ConstDef.fileStreamMaxLen);
        static readonly List<ManifestData> fileManifests = new List<ManifestData>();

        public static void Export(List<string> fileList)
        {
            // 每次导出都存储一次配置文件
            GlobalConfig.Ins.SaveConfig();
            fileManifests.Clear();
            // 加载选择的Excel文件列表
            for (int i = 0; i < fileList.Count; i++)
            {
                fileBuffer.Clear();
                string filePath = fileList[i];
                ExcelData excelData = new ExcelData(filePath);
                Program.mainForm?.Invoke(new Action(() => { Program.mainForm.SetProgress(i + 1, "正在生成：" + excelData.Name); }));
                try
                {
                    if (excelData.Load())
                    {
                        try
                        {
                            foreach (var sheetData in excelData.sheetDataList)
                            {
                                string exportName;
                                if (sheetData.SheetConfig.ExportName != string.Empty)
                                {
                                    exportName = sheetData.SheetConfig.ExportName;
                                }
                                else
                                {
                                    exportName = excelData.sheetDataList.Count > 1 ? $"{excelData.Name}_{sheetData.Name}" : excelData.Name;
                                }
                                ExportByteFile(GlobalConfig.Ins.byteFileOutputDir, exportName, sheetData);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"表格[{excelData.Name}]加载错误：{ex}");
                        }
                    }
                    excelData.Dispose();
                }
                catch
                {
                    Log.Error($"{excelData.Name} 加载错误");
                }
                
            }
            // 导出Manifest信息
            ExportManifest(fileManifests);
            // 导出cs定义文件
            ExportExcelDefineCSCode(GlobalConfig.Ins.codeFileOutputDir, fileManifests);
            // 导出数据结构信息文件
            ExportStructInfoCsCode(GlobalConfig.Ins.codeFileOutputDir, fileManifests);
        }

        public static void ExportByteFile(string path, string exportName, SheetData sheet)
        {
            Log.LogConsole($"正在生成 {exportName}.bytes {sheet.rows.Count}行 {sheet.heads.Count}列 ...");
            ManifestData fileData = new ManifestData(sheet, exportName);
            fileManifests.Add(fileData);
            int heapStart = fileData.AlignLength;
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
            string filePath = StringHelper.MakeFullPath(path + Path.DirectorySeparatorChar, $"{exportName}.bytes");
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                byte[] data = fileBuffer.GetBuffer();
                int length = fileBuffer.ReadableBytes;
                fs.Write(data, 0, length);
            }
        }

        private static void ExportManifest(List<ManifestData> fileDatas)
        {
            Log.LogConsole("正在生成 manifest.bytes ...");
            string path = GlobalConfig.Ins.byteFileOutputDir;
            fileBuffer.Clear();
            // 写入信息
            fileBuffer.SetHeapIndexStartPos(28 * fileDatas.Count + 4);    // 文件信息内容是固定的
            fileBuffer.WriteInt(fileDatas.Count);   // 先写入总个数
            for (int i = 0, len = fileDatas.Count; i < len; i++)
            {
                ManifestData data = fileDatas[i];
                fileBuffer.WriteString(data.ByteFileName);
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

        private static void ExportExcelDefineCSCode(string path, List<ManifestData> data)
        {
            Log.LogConsole("正在生成 ExcelVariableDefine.cs ...");
            using (StreamWriter sw = new StreamWriter(
                path + Path.DirectorySeparatorChar + "ExcelVariableDefine" + ".cs", 
                false, 
                new UTF8Encoding(false)))
            {
                StringBuilder sb1 = new StringBuilder();    // 类变量
                StringBuilder sb2 = new StringBuilder();    // 文件枚举
                //sb1.AppendLine(@"public sealed class ExcelVariableDef");
                //sb1.AppendLine(@"{");
                sb2.AppendLine(@"public enum ExcelName");
                sb2.AppendLine(@"{");
                for (int i = 0; i < data.Count; i++)
                {
                    ManifestData info = data[i];
                    string fileName = "EVD_" + info.ByteFileName;
                    sb1.Append(@"public static class EVD_");
                    sb1.AppendLine(info.ByteFileName);
                    sb1.AppendLine(@"{");
                    sb2.Append(@"    ///<summary>");
                    sb2.Append($"主列: " + info.VariableNames[info.IdColIndex] +
                        $" [{DataTypeHelper.GetCommentTypeByToken(info.GetToken(info.IdColIndex))}]");
                    sb2.AppendLine(@"</summary>");
                    sb2.AppendLine(@"    " + info.ByteFileName + @" = " + i.ToString() + @",");
                    for (int j = 0; j < info.VariableNames.Count; j++)
                    {
                        string varName = info.VariableNames[j];
                        string raw = varName;
                        if (varName == fileName)
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
                        sb1.Append(@"    /// <summary>");
                        string type = DataTypeHelper.GetCommentTypeByToken(info.Tokens[j]);
                        sb1.Append($"[{type}] " + info.Comments[j]);
                        sb1.AppendLine(@"</summary>");
                        sb1.Append(@"    public const int ");
                        int val = (j << 16) + info.ColOffset[j];
                        sb1.AppendLine(varName + @" = " + val.ToString() + ";");
                    }
                    sb1.AppendLine(@"}");
                }
                //sb1.AppendLine(@"}");
                sb2.Append(@"}");
                sw.Write(sb1.ToString());
                sw.Write(sb2.ToString());
            }
        }

        private static void ExportStructInfoCsCode(string path, List<ManifestData> data)
        {
            Log.LogConsole("正在生成 ExcelDataStruct.cs ...");
            using (StreamWriter sw = new StreamWriter(
                path + Path.DirectorySeparatorChar + "ExcelDataStruct" + ".cs",
                false,
                new UTF8Encoding(false)))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(@"using System.Collections.Generic;");
                sb.AppendLine(@"using UnityEngine;");
                sb.AppendLine();

                for (int i = 0; i < data.Count; i++)
                {
                    ManifestData info = data[i];
                    string fileName = "EDS_" + info.ByteFileName;
                    string idType = DataTypeHelper.GetType(info.Tokens[info.IdColIndex]);
                    sb.AppendLine(@"public struct " + fileName);
                    sb.AppendLine(@"{");
                    sb.AppendLine(@"    " + idType + @" primaryColVal;");
                    
                    sb.AppendLine(@"    public " + fileName + @"(" + idType
                        + @" val) { this.primaryColVal = val; }");
                    for (int j = 0; j < info.VariableNames.Count; j++)
                    {
                        string varName = info.VariableNames[j];
                        string raw = varName;
                        string csType = DataTypeHelper.GetType(info.Tokens[j]);
                        if (varName == fileName)
                        {
                            bool b = true;
                            int a = 0;
                            while (b)
                            {
                                varName = raw + "_c_" + a.ToString();
                                if (!info.VariableNames.Contains(varName))
                                {
                                    b = false;
                                }
                                else a++;
                            }
                        }
                        sb.Append(@"    /// <summary>");
                        sb.Append(info.Comments[j]);
                        sb.AppendLine(@"</summary>");
                        sb.AppendLine("    " + info.GetPropertyStr(j, varName));
                    }
                    sb.AppendLine(@"}");
                }
                sw.Write(sb.ToString());
            }
        }

		private static void WriteCell(ByteWriterBuffer buffer, HeadData head, string value)
		{
			switch (head.MainType)
            {
                case TypeDef.sbyteType:
                    buffer.WriteSbyte(StringConvert.StringToValue<sbyte>(value));
                    break;
                case TypeDef.uintType:
                    buffer.WriteUInt(StringConvert.StringToValue<uint>(value));
                    break;
                case TypeDef.ulongType:
                    buffer.WriteULong(StringConvert.StringToValue<ulong>(value));
                    break;
                case TypeDef.ushortType:
                    buffer.WriteUShort(StringConvert.StringToValue<ushort>(value));
                    break;
                case TypeDef.boolType:
					buffer.WriteBool(StringConvert.StringToBool(value));
					break;
				case TypeDef.byteType:
					buffer.WriteByte(StringConvert.StringToValue<byte>(value));
					break;
				case TypeDef.shortType:
					buffer.WriteShort(StringConvert.StringToValue<short>(value));
					break;
				case TypeDef.intType:
					buffer.WriteInt(StringConvert.StringToValue<int>(value));
					break;
				case TypeDef.floatType:
					buffer.WriteFloat(StringConvert.StringToValue<float>(value));
					break;
				case TypeDef.stringType:
					buffer.WriteString(value);
					break;
				case TypeDef.longType:
					buffer.WriteLong(StringConvert.StringToValue<long>(value));
					break;
				case TypeDef.doubleType:
					buffer.WriteDouble(StringConvert.StringToValue<double>(value));
					break;
				case TypeDef.listType:
					WriteList(buffer, head.SubType[0], value);
					break;
                case TypeDef.dictType:
                    WriteDict(buffer, head.SubType, value);
                    break;
                case TypeDef.vecType:
                    WriteVect(buffer, head.SubType, value);
                    break;
				default: 
                    Log.Error($"不支持的类型 {head.MainType}");
                    break;
			}
		}

		private static void WriteList(ByteWriterBuffer buffer, string subType, string value)
        {
			switch(subType)
            {
                case TypeDef.sbyteType:
                    buffer.WriteListSbyte(StringConvert.StringToList<sbyte>(value, SymbolDef.splitChar));
                    break;
                case TypeDef.uintType:
                    buffer.WriteListUInt(StringConvert.StringToList<uint>(value, SymbolDef.splitChar));
                    break;
                case TypeDef.ulongType:
                    buffer.WriteListULong(StringConvert.StringToList<ulong>(value, SymbolDef.splitChar));
                    break;
                case TypeDef.ushortType:
                    buffer.WriteListUShort(StringConvert.StringToList<ushort>(value, SymbolDef.splitChar));
                    break;
                case TypeDef.boolType:
                    buffer.WriteListBool(StringConvert.StringToList<bool>(value, SymbolDef.splitChar));
                    break;
                case TypeDef.byteType:
                    buffer.WriteListByte(StringConvert.StringToList<byte>(value, SymbolDef.splitChar));
                    break;
                case TypeDef.shortType:
                    buffer.WriteListShort(StringConvert.StringToList<short>(value, SymbolDef.splitChar));
                    break;
                case TypeDef.intType:
                    buffer.WriteListInt(StringConvert.StringToList<int>(value, SymbolDef.splitChar));
                    break;
                case TypeDef.floatType:
                    buffer.WriteListFloat(StringConvert.StringToList<float>(value, SymbolDef.splitChar)); break;
                case TypeDef.stringType:
                    buffer.WriteListString(StringConvert.StringToStringList(value)); break;
                case TypeDef.longType:
                    buffer.WriteListLong(StringConvert.StringToList<long>(value, SymbolDef.splitChar)); break;
                case TypeDef.doubleType:
                    buffer.WriteListDouble(StringConvert.StringToList<double>(value, SymbolDef.splitChar)); break;
            }
        }

        private static void WriteDict(ByteWriterBuffer buffer, string[] subType, string value)
        {
            var dict = StringConvert.StringToDict<string, string>(value);
            buffer.WriteDict(dict, subType[0], subType[1]);
        }

        private static void WriteVect(ByteWriterBuffer buffer, string[] subType, string value)
        {
            int dimen = int.Parse(subType[0]);
            if (dimen == 2)
            {
                if (subType[1] == TypeDef.intType)
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
                if (subType[1] == TypeDef.intType)
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
                if (subType[1] == TypeDef.intType)
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
