using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ExcelToByteFile
{
    public class DataTypeHelper
    {
        /// <summary>
        /// 是否是正确的类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsValidType(string type)
        {
            if (IsListType(type) || IsDictType(type) || IsBaseType(type))
                return true;
            else return false;
        }
        
        /// <summary>
        /// 是否是基础类型（除列表和字典外的类型）
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsBaseType(string type)
        {
            switch (type)
            {
                case TypeDefine.boolType:
                case TypeDefine.byteType:
                case TypeDefine.shortType:
                case TypeDefine.intType:
                case TypeDefine.floatType:
                case TypeDefine.stringType:
                case TypeDefine.longType:
                case TypeDefine.doubleType:
                case TypeDefine.sbyteType:
                case TypeDefine.uintType:
                case TypeDefine.ulongType:
                case TypeDefine.ushortType:
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 是否是列表类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsListType(string type)
        {
            type = type.Replace(" ", "");
            string pattern = @"list<([a-zA-Z]+)>";
            Match m = Regex.Match(type, pattern);
            if (m.Success)
            {
                if (IsBaseType(m.Groups[1].Value)) return true;
            }
            return false;
        }

        /// <summary>
        /// 是否是字典类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsDictType(string type)
        {
            type = type.Replace(" ", "");
            string pattern = @"dict<([a-zA-Z]+),([a-zA-Z]+)>";
            Match m = Regex.Match(type, pattern);
            if (m.Success)
            {
                if (IsBaseType(m.Groups[1].Value) && IsBaseType(m.Groups[2].Value))
                    return true;
            }
            return false;
        }

        public static string GetProcessedType(string rawType)
        {
            if (IsBaseType(rawType)) return rawType;
            else if (IsListType(rawType)) return TypeDefine.listType;
            else return TypeDefine.dictType;
        }

        /// <summary>
        /// 获取复合类型的子类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string[] GetSubType(string type)
        {
            type = type.Replace(" ", "");
            string[] subType = null;
            if (IsListType(type))
            {
                subType = new string[1];
                string pattern = @"list<([a-zA-Z]+)>";
                Match m = Regex.Match(type, pattern);
                subType[0] = m.Groups[1].Value; 
            }
            else if (IsDictType(type))
            {
                subType = new string[2];
                string pattern = @"dict<([a-zA-Z]+),([a-zA-Z]+)>";
                Match m = Regex.Match(type, pattern);
                subType[0] = m.Groups[1].Value;
                subType[1] = m.Groups[2].Value;
            }
            return subType;
        }
    }
}
