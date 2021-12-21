using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExcelToByteFile
{
    internal class ExportCSharpCode
    {
        public static void ExportVariableDefCSCode(string path, List<ManifestData> data)
        {
            Log.LogConsole("正在生成 ExcelVariableDef.cs ...");
            using (StreamWriter sw = new StreamWriter(
                path + Path.DirectorySeparatorChar + "ExcelVariableDef" + ".cs",
                false,
                new UTF8Encoding(false)))
            {
                StringBuilder sb1 = new StringBuilder();    // 类变量
                StringBuilder sb2 = new StringBuilder();    // 文件枚举
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
                sb2.Append(@"}");
                sw.Write(sb1.ToString());
                sw.Write(sb2.ToString());
            }
        }

        public static void ExportStructInfoCsCode(string path, List<ManifestData> data)
        {
            Log.LogConsole("正在生成 ExcelDataStruct.cs ...");
            using (StreamWriter sw = new StreamWriter(path + Path.DirectorySeparatorChar + "ExcelStructDef" + ".cs",
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
                    string fileName = "ESD_" + info.ByteFileName;
                    string idType = DataTypeHelper.GetType(info.Tokens[info.IdColIndex]);
                    sb.AppendLine(@"public struct " + fileName);
                    sb.AppendLine(@"{");
                    sb.AppendLine(@"    " + idType + @" primaryColVal;");

                    sb.AppendLine(@"    public " + fileName + @"(" + idType + @" val) { this.primaryColVal = val; }");
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
    }
}
