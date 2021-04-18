using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ExcelParseTool
{
    static class StringExtension
    {
        public static string StripMargin(this string s)
        {
            return Regex.Replace(s, @"[ \t]+\|", " ");
        }
    }
}
