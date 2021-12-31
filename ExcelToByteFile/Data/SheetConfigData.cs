using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using NPOI.SS.UserModel;

using Org.BouncyCastle.Crypto.Operators;

namespace ExcelToByteFile
{
    /// <summary>
    /// 对应表格第一个单元格数据
    /// </summary>
    public struct SheetConfigData
    {
        public string ExportName { get; }

        public bool CacheData { get; }

        public bool Export { get; }

        public bool Optimize { get; }

        public Dictionary<string, string> ExtraInfo { get; }

        private readonly string logFileName; // log时用于判断是哪个excel和哪个sheet

        public SheetConfigData(string config, string logFileName)
        {
            this.logFileName = logFileName;
            ExtraInfo = new Dictionary<string, string>();
            /******* default *******/
            ExportName = string.Empty;
            CacheData = false;
            Export = true;
            Optimize = true;
            /***********************/
            //config = config.ToLowerAndRemoveWhiteSpace();
            if (string.IsNullOrWhiteSpace(config)) return;
            var configs = config.Split("\n"); // Environment.NewLine
            bool reverse = false;
            string key;
            string value;
            bool isExtra = false;
            foreach (var c in configs)
            {
                if (string.IsNullOrWhiteSpace(c)) continue;
                value = string.Empty;
                reverse = false;
                string keyword = c.ToLower().Trim(); //Regex.Replace(c.ToLower(), @"\s", ""); // 转小写并去空格
                if (isExtra)
                {
                    var temp = keyword.Split('=');
                    if (temp.Length == 2)
                    {
                        ExtraInfo.Add(temp[0].Trim(), temp[1].Trim());
                    }
                    else
                    {
                        Log.Error($"{logFileName} 配置写法错误：{keyword}, 必须为key=value形式");
                    }
                }
                if (keyword == SheetKeyword.ExtraInfo)
                {
                    isExtra = true;
                    continue;
                }
                if (keyword[0] == SymbolDef.reverseChar)
                {
                    key = keyword[1..].Trim();
                    reverse = true;
                }
                else
                {
                    var temp = keyword.Split('=');
                    key = temp[0].Trim(); ;
                    if (temp.Length > 1)
                    {
                        if (temp.Length > 2)
                        {
                            Log.Error($"{logFileName} 配置写法错误：{keyword}");
                        }
                        value = temp[1].Trim();
                    }
                }

                switch (keyword)
                {
                    case SheetKeyword.Export:
                        Export = !reverse && ((value == string.Empty) || (value == SymbolDef.trueWord));
                        break;
                    case SheetKeyword.ExportName:
                        if (value == null)
                        {
                            Log.Error($"{logFileName} 配置写法错误：{keyword}, 导出名称不可为空");
                        }
                        ExportName = value;
                        break;
                    case SheetKeyword.CacheData:
                        CacheData = !reverse && ((value == string.Empty) || (value == SymbolDef.trueWord));
                        break;
                    case SheetKeyword.Optimize:
                        Optimize = !reverse && ((value == string.Empty) || (value == SymbolDef.trueWord));
                        break;
                    default:
                        Log.Error($"{logFileName} 未支持的关键字：{keyword}, value={value}");
                        break;
                }
            }
        }
    }
}
