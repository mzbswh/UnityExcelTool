using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelToByteFile
{
    public class SheetKeyword
    {
        public const string Export = "export";          // 是否导出
        public const string ExportName = "exportname";  // 导出名字
        public const string CacheData = "cache";        // 是否缓存数据
        public const string Optimize = "optimize";      // 是否优化数据
        public const string ExtraInfo = "[extra]";      // 此关键字下的都是额外信息
    }
}
