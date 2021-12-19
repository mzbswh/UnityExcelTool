using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using NPOI.SS.UserModel;

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

        private readonly string logFileName; // log时用于判断是哪个excel和哪个sheet

        public SheetConfigData(string config, string logFileName)
        {
            this.logFileName = logFileName;

            /******* default *******/
            ExportName = string.Empty;
            CacheData = false;
            Export = true;
            Optimize = true;
            /***********************/
            config = config.ToLowerAndRemoveWhiteSpace();
            if (string.IsNullOrWhiteSpace(config)) return;
            var configs = config.Split(Environment.NewLine);
            bool reverse = false;
            string key;
            string value;
            foreach (var c in configs)
            {
                if (string.IsNullOrWhiteSpace(c)) continue;
                value = string.Empty;
                reverse = false;
                string keyword = Regex.Replace(c.ToLower(), @"\s", ""); // 转小写并去空格
                if (keyword[0] == SymbolDef.reverseChar)
                {
                    key = keyword[1..];
                    reverse = true;
                }
                else
                {
                    var temp = keyword.Split('=');
                    key = temp[0];
                    if (temp.Length > 1)
                    {
                        if (temp.Length > 2)
                        {
                            Log.Error($"{logFileName} 配置写法错误：{keyword}");
                        }
                        value = temp[1];
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
