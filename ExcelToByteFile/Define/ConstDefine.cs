using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelToByteFile
{
    public class ConstDefine
    {
        /// <summary>
        /// 分隔符
        /// </summary>
        public const char splitChar = ',';

        /// <summary>
        /// 注释符号
        /// </summary>
        public const char noteChar = '#';

        /// <summary>
        /// id列名称
        /// </summary>
        public const string idColName = "id";

        /// <summary>
        /// excel头部固定行数
        /// </summary>
        public const int headRowNum = 3; 
    }

    public class TypeDefine
    {
        public const string sbyteType = "sbyte";
        public const string ushortType = "ushort";
        public const string uintType = "uint";
        public const string ulongType = "ulong";
        public const string boolType = "bool";
        public const string byteType = "byte";
        public const string shortType = "short";
        public const string intType = "int";
        public const string floatType = "float";
        public const string stringType = "string";
        public const string longType = "long";
        public const string doubleType = "double";
        public const string listType = "list";
        public const string dictType = "dict";
    }

    public enum TypeToken
    {
        SByte = 1,
        Byte = 2,
        Bool = 3,
        UShort = 4,
        Short = 5,
        UInt = 6,
        Int = 7,
        Float = 8,
        Double = 9,
        ULong = 10,
        Long = 11,
        String = 12,
        List = 100,
        Dictionary = 10000
    }
}
