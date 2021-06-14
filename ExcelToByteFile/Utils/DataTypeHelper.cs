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
            if (IsBaseType(type) || IsListType(type) || IsDictType(type) || IsVectorType(type))
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

        public static bool IsVectorType(string type)
        {
            type = type.Replace(" ", "").ToLower();
            string pattern = @"vector(\d)([a-zA-Z]*)";
            Match m = Regex.Match(type, pattern);
            if (m.Success)
            {
                int val = int.Parse(m.Groups[1].Value);
                if (m.Groups[2].Value == TypeDefine.intType || m.Groups[2].Value == string.Empty)
                {
                    if (val == 2 || val ==3 || val == 4)
                        return true;
                }
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
            type = type.Replace(" ", "").ToLower();
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
            type = type.Replace(" ", "").ToLower();
            string pattern = @"dict<([a-zA-Z]+),([a-zA-Z]+)>";
            Match m = Regex.Match(type, pattern);
            if (m.Success)
            {
                if (IsBaseType(m.Groups[1].Value) && IsBaseType(m.Groups[2].Value))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 获取主类型
        /// </summary>
        /// <param name="rawType"></param>
        /// <returns></returns>
        public static string GetMainType(string rawType)
        {
            rawType = rawType.Replace(" ", "").ToLower();
            if (IsBaseType(rawType)) return rawType;
            else if (IsListType(rawType)) return TypeDefine.listType;
            else if (IsVectorType(rawType)) return TypeDefine.vecType;
            else return TypeDefine.dictType;
        }

        /// <summary>
        /// 获取复合类型的子类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string[] GetSubType(string type)
        {
            type = type.Replace(" ", "").ToLower();
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
                case TypeDefine.boolType: return typeof(bool);
                case TypeDefine.sbyteType: return typeof(sbyte);
                case TypeDefine.byteType: return typeof(byte);
                case TypeDefine.shortType: return typeof(short);
                case TypeDefine.ushortType: return typeof(ushort);
                case TypeDefine.uintType: return typeof(uint);
                case TypeDefine.intType: return typeof(int);
                case TypeDefine.floatType: return typeof(float);
                case TypeDefine.ulongType: return typeof(ulong);
                case TypeDefine.longType: return typeof(long);
                case TypeDefine.doubleType: return typeof(double);
                case TypeDefine.stringType: return typeof(string);
                default: return null;
            }
        }

        public static int GetBaseTypeLen(string baseType)
        {
            switch (baseType)
            {
                case TypeDefine.sbyteType:
                case TypeDefine.boolType:
                case TypeDefine.byteType:
                    return 1;
                case TypeDefine.shortType:
                case TypeDefine.ushortType:
                    return 2;
                case TypeDefine.floatType:
                case TypeDefine.intType:
                case TypeDefine.stringType:
                case TypeDefine.uintType:
                    return 4;
                case TypeDefine.doubleType:
                case TypeDefine.longType:
                case TypeDefine.ulongType:
                    return 8;
                default: return 0;
            }
        }
    }
}
