using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Microsoft.VisualBasic;

namespace ExcelToByteFile
{
    internal class ExportCSharpCode
    {
        public static void ExportVariableDefCSCode(string path, List<ManifestData> data)
        {
            //Log.LogConsole("正在生成 ExcelVariableDef.cs ...");
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
                sw.WriteLine(@"#pragma warning disable");
                sw.WriteLine(@"using ExcelTool;");
                sw.Write(sb1.ToString());
                sw.Write(sb2.ToString());
                sw.Flush();
            }
        }

        public static void ExportStructInfoCsCode(string path, List<ManifestData> data)
        {
            //Log.LogConsole("正在生成 ExcelDataStruct.cs ...");
            using (StreamWriter sw = new StreamWriter(path + Path.DirectorySeparatorChar + "ExcelStructDef" + ".cs",
                false,
                new UTF8Encoding(false)))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(@"using System.Collections.Generic;");
                sb.AppendLine(@"using UnityEngine;");
                sb.AppendLine(@"using ExcelTool;");
                sb.AppendLine();
                sb.AppendLine(@"#pragma warning disable");
                sb.AppendLine();

                for (int i = 0; i < data.Count; i++)
                {
                    ManifestData info = data[i];
                    string fileName = "ESD_" + info.ByteFileName;
                    string idType = DataTypeHelper.GetType(info.Tokens[info.IdColIndex]);
                    sb.AppendLine(@"public struct " + fileName);
                    sb.AppendLine(@"{");
                    // 字段
                    sb.AppendLine(@"    " + idType + @" primaryColVal;");
                    sb.AppendLine(@"    readonly ByteFileInfo<" + idType + @"> byteFileInfo;");
                    // 构造方法
                    sb.AppendLine(@"    public " + fileName + @"(" + idType + @" val)");
                    sb.AppendLine(@"    {");
                    sb.AppendLine(@"        this.primaryColVal = val;");
                    sb.AppendLine(@"        this.byteFileInfo = ExcelDataMgr.GetByteFileInfo<" + idType + @">((short)ExcelName." + info.ByteFileName + @");");
                    sb.AppendLine(@"    }");
                    // 重新设置主列值
                    sb.AppendLine(@"    public void SetPrimary(" + idType + @" id) { this.primaryColVal = id; } ");
                    // 属性
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
                        sb.AppendLine("    " + GetPropertyStr(info, j, varName));
                    }
                    sb.AppendLine(@"}");
                }
                sw.Write(sb.ToString());
                sw.Flush();
            }

            static string GetPropertyStr(ManifestData info, int index, string varName)
            {
                string csType = DataTypeHelper.GetType(info.Tokens[index]);
                int type = info.Tokens[index];
                int off = (index << 16) + info.ColOffset[index];
                if (type >= (int)TypeToken.Dictionary && type < (int)TypeToken.Vector)
                {
                    int keyToken = (type - (int)TypeToken.Dictionary) / 100;
                    int valToken = (type - (int)TypeToken.Dictionary) % 100;
                    string keyType = ((TypeToken)keyToken).ToString().ToLower();
                    string valType = ((TypeToken)valToken).ToString().ToLower();
                    return $"public Dictionary<{keyType}, {valType}> {varName} => byteFileInfo.GetDict<{keyType}, {valType}>(primaryColVal, {off});";
                }
                else
                {
                    return $"public {csType} {varName} => byteFileInfo.Get<{csType}>(primaryColVal, {off});";
                }
            }
        }

        public static void ExportCacheCsCode(string path, List<ManifestData> data)
        {
            List<ManifestData> cacheInfo = new List<ManifestData>();
            List<string> fileNames = new List<string>();
            foreach (var info in data)
            {
                if (info.SheetConfig.CacheData)
                {
                    cacheInfo.Add(info);
                }
            }
            if (cacheInfo.Count > 0)
            {
                foreach (var info in cacheInfo)
                {
                    string fileName = "EDC_" + info.ByteFileName;
                    fileNames.Add(fileName);
                    using (StreamWriter sw = new StreamWriter(path + Path.DirectorySeparatorChar + $"{fileName}.cs",
                    false,
                    new UTF8Encoding(false)))
                    {
                        StringBuilder sb = new StringBuilder();
                        StringBuilder sb2 = new StringBuilder();
                        sb.AppendLine(@"using System.Collections.Generic;");
                        sb.AppendLine(@"using UnityEngine;");
                        sb.AppendLine(@"using ExcelTool;");
                        sb.AppendLine();
                        sb.AppendLine(@"#pragma warning disable");
                        sb.AppendLine();
                        string idType = DataTypeHelper.GetType(info.Tokens[info.IdColIndex]);
                        sb.AppendLine(@"public class " + fileName);
                        sb.AppendLine(@"{");
                        // 静态字段
                        sb.AppendLine($"    public static {idType}[] Ids => byteFileInfo.Ids;");
                        sb.AppendLine(@"    static bool cached = false;");
                        sb.AppendLine($"    static ByteFileInfo<{idType}> byteFileInfo;");
                        sb.AppendLine($"    static Dictionary<{idType}, {fileName}> cacheDict = new Dictionary<{idType}, {fileName}>();");
                        sb.AppendLine();
                        // 属性
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
                            sb.AppendLine($"    public {csType} {varName} {{ get; }}");
                            if (j == info.IdColIndex)
                            {
                                sb2.AppendLine($"        this.{varName} = id;");
                                sb2.AppendLine($"        ByteFileReader.SkipOne();");
                            }
                            else
                            {
                                if (csType.StartsWith("Dict"))
                                {
                                    int keyToken = (info.Tokens[j] - (int)TypeToken.Dictionary) / 100;
                                    int valToken = (info.Tokens[j] - (int)TypeToken.Dictionary) % 100;
                                    var keyType = ((TypeToken)keyToken).ToString().ToLower();
                                    var valType = ((TypeToken)valToken).ToString().ToLower();
                                    //sb2.AppendLine($"        this.{varName} = byteFileInfo.GetDictByRowAndIndex<{keyType}, {valType}>(row, {j});");
                                    sb2.AppendLine($"        this.{varName} = ByteFileReader.GetDict<{keyType}, {valType}>();");
                                }
                                else
                                {
                                    //sb2.AppendLine($"        this.{varName} = byteFileInfo.GetByRowAndIndex<{csType}>(row, {j});");
                                    sb2.AppendLine($"        this.{varName} = ByteFileReader.Get<{csType}>();");
                                }
                            }
                        }
                        sb.AppendLine();
                        // 构造方法
                        sb.AppendLine($"    public {fileName}({idType} id)");
                        sb.AppendLine(@"    {");
                        sb.AppendLine(sb2.ToString());
                        sb2.Clear();
                        sb.AppendLine(@"    }");
                        sb.AppendLine();
                        // 静态方法 CacheData 用于缓存所有数据
                        sb.AppendLine(@"    public static void CacheData()");
                        sb.AppendLine(@"    {");
                        sb.AppendLine(@"        if (cached) return;");
                        sb.AppendLine(@"        if (byteFileInfo == null)");
                        sb.AppendLine(@"        {");
                        sb.AppendLine($"            byteFileInfo = ExcelDataMgr.GetByteFileInfo<{idType}>((short)ExcelName.{info.ByteFileName});");
                        sb.AppendLine(@"        }");
                        sb.AppendLine(@"        if (!byteFileInfo.ByteDataLoaded) byteFileInfo.LoadByteData();");
                        sb.AppendLine(@"        byteFileInfo.ResetByteFileReader();");
                        sb.AppendLine(@"        for (int i = 0; i < byteFileInfo.RowCount; i++)");
                        sb.AppendLine(@"        {");
                        sb.AppendLine($"            {idType} id = byteFileInfo.GetKey(i);");
                        sb.AppendLine($"            {fileName} cache = new {fileName}(id);");
                        sb.AppendLine(@"            cacheDict.Add(id, cache);");
                        sb.AppendLine(@"        }");
                        sb.AppendLine(@"    }");
                        sb.AppendLine();
                        // 静态方法 Get 获取某一行缓存数据
                        sb.AppendLine($"    public static {fileName} Get({idType} id)");
                        sb.AppendLine(@"    {");
                        sb.AppendLine(@"        if (cacheDict.TryGetValue(id, out var cache)) return cache;");
                        sb.AppendLine($"        Debug.LogError($\"{{typeof({fileName}).Name}}不存在主列值{{id.ToString()}}\");");
                        sb.AppendLine(@"        return null;");
                        sb.AppendLine(@"    }");
                        sb.AppendLine(@"}");
                        sw.Write(sb.ToString());
                        sw.Flush();
                    }
                }

                // 导出缓存管理器脚本
                using (StreamWriter sw = new StreamWriter(GlobalConfig.Ins.codeFileOutputDir + Path.DirectorySeparatorChar + $"ExcelDataCacheMgr.cs",
                    false,
                    new UTF8Encoding(false)))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(@"#pragma warning disable");
                    sb.AppendLine();
                    sb.AppendLine(@"public class ExcelDataCacheMgr");
                    sb.AppendLine(@"{");
                    sb.AppendLine(@"    public static void CacheData()");
                    sb.AppendLine(@"    {");
                    for (int i = 0; i < fileNames.Count; i++)
                    {
                        sb.AppendLine($"        {fileNames[i]}.CacheData();");
                    }
                    sb.AppendLine(@"    }");
                    sb.AppendLine(@"}");
                    sw.Write(sb.ToString());
                    sw.Flush();
                }
            }
        }
    }
}