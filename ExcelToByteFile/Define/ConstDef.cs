using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelToByteFile
{
    internal class ConstDef
    {
        public const int fileStreamMaxLen = 1024 * 1024 * 128;  // 最大128M
        public const int sheetDefRowMax = 20;                   // sheet表类型名称行所在位置范围（从前xx行读取，即类型名称必须在此值内）
    }
}
