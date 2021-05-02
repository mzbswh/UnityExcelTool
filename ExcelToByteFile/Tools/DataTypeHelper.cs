using System;
using System.Collections.Generic;
using System.Text;

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
            type = type.Trim();
            if (type.StartsWith(TypeDefine.listType))
            {
                string sub = type.Substring(4, type.Length - 4);
                if (sub[0] == '<' && sub[sub.Length - 1] == '>')
                {
                    sub = sub.Substring(1, sub.Length - 2);
                    if (IsBaseType(sub)) return true;
                }
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
            type = type.Trim();
            if (type.StartsWith(TypeDefine.dictType))
            {
                string sub = type.Substring(4, type.Length - 4);
                if (sub[0] == '<' && sub[sub.Length - 1] == '>')
                {
                    sub = sub.Substring(1, sub.Length - 2);
                    string[] baseTypes = sub.Split(',');
                    if (baseTypes.Length == 2 && IsBaseType(baseTypes[0]) && IsBaseType(baseTypes[1])) 
                        return true;
                }
            }
            return false;
        }
    }
}
