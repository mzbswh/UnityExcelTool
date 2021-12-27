using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                sw.WriteLine(@"using ExcelToByteFile;");
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
                sb.AppendLine(@"using ExcelToByteFile;");
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
                    string name = string.Empty;
                    var temp = info.ByteFileName.Split("_");
                    for (int i = 0; i < temp.Length; i++)
                    {
                        name += temp[i].Substring(0, 1).ToUpper() + temp[i].Substring(1).ToLower();
                    }
                    string fileName = "Dict" + name;
                    fileNames.Add(fileName);
                    using (StreamWriter sw = new StreamWriter(path + Path.DirectorySeparatorChar + $"{fileName}.cs",
                    false,
                    new UTF8Encoding(false)))
                    {
                        StringBuilder sb = new StringBuilder();
                        StringBuilder sb2 = new StringBuilder();
                        sb.AppendLine(@"using System.Collections.Generic;");
                        sb.AppendLine(@"using UnityEngine;");
                        sb.AppendLine(@"using ExcelToByteFile;");
                        sb.AppendLine();
                        sb.AppendLine(@"#pragma warning disable");
                        sb.AppendLine();
                        string idType = DataTypeHelper.GetType(info.Tokens[info.IdColIndex]);
                        sb.AppendLine(@"public partial class " + fileName);
                        sb.AppendLine(@"{");
                        // 静态字段
                        sb.AppendLine(@"    static bool cached = false;");
                        sb.AppendLine($"    static ByteFileInfo<{idType}> byteFileInfo;");
                        //sb.AppendLine(@"    static ByteFileReader byteFileReader;");
                        sb.AppendLine($"    Dictionary<{idType}, Model> m_dict = new Dictionary<{idType}, Model>();");
                        sb.AppendLine($"    public Dictionary<{idType}, Model> Dict => m_dict;");
                        sb.AppendLine($"    public {idType}[] Ids => byteFileInfo.Ids;");
                        sb.AppendLine();
                        // Model类
                        sb.AppendLine(@"    public class Model");
                        sb.AppendLine(@"    {");
                        // 字段
                        //sb2.AppendLine(@"           var bf = byteFileReader;  // ILRuntime环境下，使用局部变量会有相当大的性能提升");
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
                            sb.Append(@"        // <summary>");
                            sb.Append(info.Comments[j]);
                            sb.AppendLine(@"</summary>");
                            sb.AppendLine($"        public {csType} {varName};");
                            if (j == info.IdColIndex)
                            {
                                sb2.AppendLine($"           this.{varName} = id;");
                                sb2.AppendLine(@"           ByteFileReader.SkipOne();");
                            }
                            else
                            {
                                if (csType.StartsWith("Dict"))
                                {
                                    int keyToken = (info.Tokens[j] - (int)TypeToken.Dictionary) / 100;
                                    int valToken = (info.Tokens[j] - (int)TypeToken.Dictionary) % 100;
                                    var keyType = ((TypeToken)keyToken).ToString().ToLower();
                                    var valType = ((TypeToken)valToken).ToString().ToLower();
                                    sb2.AppendLine($"           this.{varName} = ByteFileReader.GetDict<{keyType}, {valType}>();");
                                }
                                else
                                {
                                    sb2.AppendLine($"           this.{varName} = ByteFileReader.Get<{csType}>();");
                                }
                            }
                        }
                        sb.AppendLine();
                        // 构造方法
                        sb.AppendLine($"        public Model({idType} id)");
                        sb.AppendLine(@"        {");
                        sb.AppendLine(sb2.ToString());
                        sb2.Clear();
                        sb.AppendLine(@"        }");
                        sb.AppendLine(@"    }");
                        
                        // 静态方法 CacheData 用于缓存所有数据
                        sb.AppendLine(@"    public void CacheData()");
                        sb.AppendLine(@"    {");
                        sb.AppendLine(@"        if (cached) return;");
                        sb.AppendLine(@"        if (byteFileInfo == null)");
                        sb.AppendLine(@"        {");
                        sb.AppendLine($"            byteFileInfo = ExcelDataMgr.GetByteFileInfo<{idType}>((short)ExcelName.{info.ByteFileName});");
                        sb.AppendLine(@"        }");
                        sb.AppendLine(@"        if (!byteFileInfo.ByteDataLoaded) byteFileInfo.LoadByteData();");
                        //sb.AppendLine(@"        byteFileReader = byteFileInfo.GetByteFileReader();");
                        sb.AppendLine(@"        ByteFileReader.Reset(byteFileInfo.GetData(), byteFileInfo.RowLength, byteFileInfo.GetColOff());");
                        sb.AppendLine(@"        for (int i = 0; i < byteFileInfo.RowCount; i++)");
                        sb.AppendLine(@"        {");
                        sb.AppendLine($"            {idType} id = byteFileInfo.GetKey(i);");
                        sb.AppendLine($"            Model cache = new Model(id);");
                        sb.AppendLine(@"            m_dict.Add(id, cache);");
                        sb.AppendLine(@"        }");
                        sb.AppendLine(@"    }");
                        sb.AppendLine();
                        // Get 获取某一行缓存数据
                        sb.AppendLine($"    public Model GetModel({idType} id)");
                        sb.AppendLine(@"    {");
                        sb.AppendLine(@"        if (m_dict.TryGetValue(id, out var cache)) return cache;");
                        sb.AppendLine($"        Debug.LogError($\"{{typeof({fileName}).Name}}不存在主列值{{id.ToString()}}\");");
                        sb.AppendLine(@"        return null;");
                        sb.AppendLine(@"    }");
                        // 索引器
                        sb.AppendLine($"    public Model this[{idType} id]");
                        sb.AppendLine(@"    {");
                        sb.AppendLine(@"        get");
                        sb.AppendLine(@"        {");
                        sb.AppendLine(@"            if (m_dict.TryGetValue(id, out var cache)) return cache;");
                        sb.AppendLine($"            Debug.LogError($\"{{typeof({fileName}).Name}}不存在主列值{{id.ToString()}}\");");
                        sb.AppendLine(@"            return null;");
                        sb.AppendLine(@"        }");
                        sb.AppendLine(@"    }");
                        // getFileName
                        sb.AppendLine($"    public string GetFileName() => byteFileInfo.Name + \".bytes\";");
                        // hasId
                        sb.AppendLine($"    public bool HasId({idType} id) => m_dict.ContainsKey(id);");
                        sb.AppendLine(@"}");
                        sw.Write(sb.ToString());
                        sw.Flush();
                    }
                }

                // 导出缓存管理器脚本
                using (StreamWriter sw = new StreamWriter(GlobalConfig.Ins.codeFileOutputDir + Path.DirectorySeparatorChar + $"DictDataManager.cs",
                    false,
                    new UTF8Encoding(false)))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(@"#pragma warning disable");
                    sb.AppendLine();
                    sb.AppendLine(@"public class DictDataManager");
                    sb.AppendLine(@"{");
                    sb.AppendLine(@"    private static DictDataManager m_instance;");
                    sb.AppendLine(@"    public static DictDataManager Instance");
                    sb.AppendLine(@"    {");
                    sb.AppendLine(@"        get");
                    sb.AppendLine(@"        {");
                    sb.AppendLine(@"            if(m_instance == null) m_instance = new DictDataManager();");
                    sb.AppendLine(@"            return m_instance;");
                    sb.AppendLine(@"        }");
                    sb.AppendLine(@"    }");
                    sb.AppendLine();
                    sb.AppendLine(@"    public void Init()");
                    sb.AppendLine(@"    {");
                    for (int i = 0; i < fileNames.Count; i++)
                    {
                        sb.AppendLine($"        {fileNames[i].Substring(0, 1).ToLower() + fileNames[i][1..]}.CacheData();");
                    }
                    sb.AppendLine(@"    }");
                    sb.AppendLine();
                    for (int i = 0; i < fileNames.Count; i++)
                    {
                        var name = fileNames[i];
                        sb.AppendLine($"    public {name} {name.Substring(0, 1).ToLower() + name[1..]} = new {name}();");
                    }
                    sb.AppendLine(@"}");
                    sw.Write(sb.ToString());
                    sw.Flush();
                }
            }
        }
    }
}