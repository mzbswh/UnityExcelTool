﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
                    sb.AppendLine(@"    ByteFileInfo<" + idType + @"> byteFileInfo;");
                    // 构造方法
                    sb.AppendLine(@"    public " + fileName + @"(" + idType + @" val)");
                    sb.AppendLine(@"    {");
                    sb.AppendLine(@"        this.primaryColVal = val;");
                    sb.AppendLine(@"        this.byteFileInfo = ExcelDataMgr.GetByteFileInfo<" + idType + @">(ExcelName." + info.ByteFileName + @");");
                    sb.AppendLine(@"    }");
                    // 重新设置主列值
                    sb.AppendLine(@"    public void SetPrimary(" + idType + @" id) { this.primaryColVal = val; } ");
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

            string GetPropertyStr(ManifestData info, int index, string varName)
            {
                string csType = DataTypeHelper.GetType(info.Tokens[index]);
                int type = info.Tokens[index];
                int off = (index << 16) + info.ColOffset[index];
                if (type >= (int)TypeToken.Dictionary)
                {
                    int keyToken = (type - (int)TypeToken.Dictionary) / 100;
                    int valToken = (type - (int)TypeToken.Dictionary) % 100;
                    string keyType = ((TypeToken)keyToken).ToString().ToLower();
                    string valType = ((TypeToken)valToken).ToString().ToLower();
                    return $"public Dictionary<{keyType}, {valType}> {varName} => byteFileInfo.GetDict<{keyType}, {valType}>(ExcelName.{info.ByteFileName}, primaryColVal, {off});";
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
                    using (StreamWriter sw = new StreamWriter(path + Path.DirectorySeparatorChar + $"{info.ByteFileName}Cache.cs",
                    false,
                    new UTF8Encoding(false)))
                    {
                        StringBuilder sb = new StringBuilder();
                        StringBuilder sb2 = new StringBuilder();
                        sb.AppendLine(@"using System.Collections.Generic;");
                        sb.AppendLine(@"using UnityEngine;");
                        sb.AppendLine();
                        sb.AppendLine(@"#pragma warning disable");
                        sb.AppendLine();
                        string fileName = info.ByteFileName + "Cache";
                        fileNames.Add(fileName);
                        string idType = DataTypeHelper.GetType(info.Tokens[info.IdColIndex]);
                        sb.AppendLine(@"public class " + fileName);
                        sb.AppendLine(@"{");
                        // 静态字段
                        sb.AppendLine(@"    static bool cached = false;");
                        sb.AppendLine(@"    static ByteFileInfo<int> byteFileInfo;");
                        sb.AppendLine($"    static Dictionary<{idType}, fileName> cacheDict = new Dictionary<{idType}, {fileName}>();");
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
                                sb2.AppendLine($"       this.{varName} = id");
                            }
                            else
                            {
                                if (csType.StartsWith("Dict"))
                                {
                                    sb2.AppendLine($"       this.{varName} = byteFileInfo.GetDictByRowAndIndex<int>(row, {j});");
                                }
                                else
                                {
                                    sb2.AppendLine($"       this.{varName} = byteFileInfo.GetByRowAndIndex<int>(row, {j});");
                                }
                            }
                        }
                        sb.AppendLine();
                        // 构造方法
                        sb.AppendLine($"    public {fileName}({idType} id, int row)");
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
                        sb.AppendLine($"            byteFileInfo = ExcelDataMgr.GetByteFileInfo<{idType}>(ExcelName.{info.ByteFileName});");
                        sb.AppendLine(@"        }");
                        sb.AppendLine(@"        if (!byteFileInfo.ByteDataLoaded) byteFileInfo.LoadByteData();");
                        sb.AppendLine(@"        for (int i = 0; i < byteFileInfo.RowCount; i++)");
                        sb.AppendLine(@"        {");
                        sb.AppendLine($"            {idType} id = byteFileInfo.GetKey(i);");
                        sb.AppendLine($"            {fileName} cache = new {fileName}(id, i);");
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
                using (StreamWriter sw = new StreamWriter(path + Path.DirectorySeparatorChar + $"ExcelDataCacheMgr.cs",
                    false,
                    new UTF8Encoding(false)))
                {
                    StringBuilder sb = new StringBuilder();
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