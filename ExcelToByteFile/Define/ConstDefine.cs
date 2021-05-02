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

    public enum ValueType
    {
        Bool,
        Byte,
        Short,
        Int,
        Float,
        String,
        Long,
        Double,
        List,
        Dictionary
    }
}
