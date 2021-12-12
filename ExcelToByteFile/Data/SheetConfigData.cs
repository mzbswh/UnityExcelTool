using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ExcelToByteFile
{
    internal class SheetConfigData
    {
        public string ExportName { get; }

        public bool CacheData { get; }

        public bool Export { get; }

        private readonly SheetData sheetData;

        public SheetConfigData(string config, SheetData sheetData)
        {
            this.sheetData = sheetData;
            /******* default *******/
            ExportName = string.Empty;
            CacheData = false;
            Export = true;
            /***********************/

            var configs = config.Split(Environment.NewLine);
            bool reverse = false;
            string key;
            string value;
            foreach (var c in configs)
            {
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
                            Log.Error($"{sheetData.ExcelName}_{sheetData.Name} 配置写法错误：{keyword}");
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
                            Log.Error($"{sheetData.ExcelName}_{sheetData.Name} 配置写法错误：{keyword}, 导出名称不可为空");
                        }
                        ExportName = value;
                        break;
                    case SheetKeyword.CacheData:
                        CacheData = !reverse && ((value == string.Empty) || (value == SymbolDef.trueWord));
                        break;
                }
            }
        }
    }
}
