using System;
using System.Text.RegularExpressions;

namespace ExcelToByteFile
{
    static class Extensions
    {
        /// <summary>
        /// 转小写并去空格
        /// </summary>
        public static string ToLowerAndRemoveWhiteSpace(this string s)
        {
            return Regex.Replace(s.ToLower(), @"\s", "");
        }
    }
}
