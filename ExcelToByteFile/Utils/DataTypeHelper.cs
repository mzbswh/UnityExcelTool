using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ExcelToByteFile
{
    public static class DataTypeHelper
    {
        /// <summary>
        /// 是否是正确的类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsValidType(string type)
        {
            return IsBaseType(type) || IsListType(type) || IsDictType(type) || IsVectorType(type);
        }
        
        /// <summary>
        /// 是否是基础类型（除列表和字典向量外的类型）
        /// </summary>
        public static bool IsBaseType(string type)
        {
            switch (type)
            {
                case TypeDef.boolType:
                case TypeDef.byteType:
                case TypeDef.shortType:
                case TypeDef.intType:
                case TypeDef.floatType:
                case TypeDef.stringType:
                case TypeDef.longType:
                case TypeDef.doubleType:
                case TypeDef.sbyteType:
                case TypeDef.uintType:
                case TypeDef.ulongType:
                case TypeDef.ushortType:
                    return true;
            }
            return false;
        }

        public static bool IsVectorType(string type)
        {
            type = type.ToLowerAndRemoveWhiteSpace();
            string pattern = @"vector(\d)([a-zA-Z]*)";
            Match m = Regex.Match(type, pattern);
            if (m.Success)
            {
                int val = int.Parse(m.Groups[1].Value);
                if (m.Groups[2].Value == TypeDef.intType || m.Groups[2].Value == string.Empty)
                {
                    if (val == 2 || val ==3 || val == 4) return true;
                }
            }
            return false;
        }

        public static bool IsListType(string type)
        {
            type = type.ToLowerAndRemoveWhiteSpace();
            string pattern = @"list<([a-zA-Z]+)>";
            Match m = Regex.Match(type, pattern);
            if (m.Success)
            {
                if (IsBaseType(m.Groups[1].Value)) return true;
            }
            return false;
        }

        public static bool IsDictType(string type)
        {
            type = type.ToLowerAndRemoveWhiteSpace();
            string pattern = @"dict<([a-zA-Z]+),([a-zA-Z]+)>";
            Match m = Regex.Match(type, pattern);
            if (m.Success)
            {
                if (IsBaseType(m.Groups[1].Value) && IsBaseType(m.Groups[2].Value))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CanOptimizeIfPrimaryIs(string type)
        {
            type = type.ToLowerAndRemoveWhiteSpace();
            switch (type)
            {
                case TypeDef.byteType:
                case TypeDef.shortType:
                case TypeDef.intType:
                case TypeDef.longType:
                case TypeDef.sbyteType:
                case TypeDef.uintType:
                case TypeDef.ulongType:
                case TypeDef.ushortType:
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 获取主类型, 如list<int>返回list
        /// </summary>
        public static string GetMainType(string rawType)
        {
            rawType = rawType.ToLowerAndRemoveWhiteSpace();
            if (IsBaseType(rawType))
            {
                return rawType;
            }
            else if (IsListType(rawType))
            {
                return TypeDef.listType;
            }
            else if (IsVectorType(rawType))
            {
                return TypeDef.vecType;
            }
            else if (IsDictType(rawType))
            {
                return TypeDef.dictType;
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取复合类型的子类型
        /// </summary>
        public static string[] GetSubType(string type)
        {
            type = type.ToLowerAndRemoveWhiteSpace();
            string[] subType = null;
            if (IsVectorType(type))
            {
                subType = new string[2];
                string pattern = @"vector(\d)([a-zA-Z]*)";
                Match m = Regex.Match(type, pattern);
                subType[0] = m.Groups[1].Value;
                subType[1] = m.Groups[2].Value;
            }
            else if (IsListType(type))
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

        public static Type GetType(string baseType)
        {
            switch (baseType)
            {
                case TypeDef.boolType: return typeof(bool);
                case TypeDef.sbyteType: return typeof(sbyte);
                case TypeDef.byteType: return typeof(byte);
                case TypeDef.shortType: return typeof(short);
                case TypeDef.ushortType: return typeof(ushort);
                case TypeDef.uintType: return typeof(uint);
                case TypeDef.intType: return typeof(int);
                case TypeDef.floatType: return typeof(float);
                case TypeDef.ulongType: return typeof(ulong);
                case TypeDef.longType: return typeof(long);
                case TypeDef.doubleType: return typeof(double);
                case TypeDef.stringType: return typeof(string);
                default: return null;
            }
        }

        /// <summary>
        /// 获取基本数据类型在对齐字节区所占的空间
        /// </summary>
        public static int GetBaseTypeLen(string baseType)
        {
            switch (baseType)
            {
                case TypeDef.sbyteType:
                case TypeDef.boolType:
                case TypeDef.byteType:
                    return 1;
                case TypeDef.shortType:
                case TypeDef.ushortType:
                    return 2;
                case TypeDef.floatType:
                case TypeDef.intType:
                case TypeDef.stringType:
                case TypeDef.uintType:
                    return 4;
                case TypeDef.doubleType:
                case TypeDef.longType:
                case TypeDef.ulongType:
                    return 8;
                default: return 0;
            }
        }

        public static string GetType(int token)
        {
            if (token >= (int)TypeToken.Vector)
            {
                int dimen = (token - (int)TypeToken.Vector) / 100;
                int valToken = (token - (int)TypeToken.Vector) % 100;
                string s = "Vector" + (dimen).ToString();
                if (valToken != 0) s += "Int";
                return s;
            }
            else if (token >= (int)TypeToken.Dictionary)
            {
                int keyToken = (token - (int)TypeToken.Dictionary) / 100;
                int valToken = (token - (int)TypeToken.Dictionary) % 100;
                return "Dictionary<" + ((TypeToken)keyToken).ToString().ToLower() + " ,"
                    + ((TypeToken)valToken).ToString().ToLower() + ">";
            }
            else if (token >= (int)TypeToken.List)
            {
                int subToken = token - (int)TypeToken.List;
                return "List<" + ((TypeToken)subToken).ToString().ToLower() + ">";
            }
            else
            {
                return ((TypeToken)token).ToString().ToLower();
            }
        }

        /// <summary>
        /// 获取注释里显示的变量类型（注释里无法使用大于小于号，所以使用其他方式显示大于小于号）
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GetCommentTypeByToken(int token)
        {
            if (token >= (int)TypeToken.Vector)
            {
                int dimen = (token - (int)TypeToken.Vector) / 100;
                int valToken = (token - (int)TypeToken.Vector) % 100;
                string type = string.Empty;
                if (valToken != 0) type = ((TypeToken)valToken).ToString();
                return "Vector" + (dimen).ToString() + type;
            }
            else if (token >= (int)TypeToken.Dictionary)
            {
                int keyToken = (token - (int)TypeToken.Dictionary) / 100;
                int valToken = (token - (int)TypeToken.Dictionary) % 100;
                return "Dict&lt;" + ((TypeToken)keyToken).ToString() + " ,"
                    + ((TypeToken)valToken).ToString() + "&gt;";
            }
            else if (token >= (int)TypeToken.List)
            {
                int subToken = token - (int)TypeToken.List;
                return "List&lt;" + ((TypeToken)subToken).ToString() + "&gt;";
            }
            else
            {
                return ((TypeToken)token).ToString();
            }
        }
    }
}
