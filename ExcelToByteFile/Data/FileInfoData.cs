using System.Collections.Generic;
using System.Windows.Forms;

namespace ExcelToByteFile
{
    /// <summary>
    /// 存放每个生成的字节file的数据，是对sheetData数据信息的进一步提炼，用于生成总信息文件
    /// </summary>
    public class FileInfoData
    {
        /// <summary>
        /// 生成字节文件名称
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// 行数
        /// </summary>
        public int RowCount { get; }

        /// <summary>
        /// 行的长度(一行多少字节)
        /// </summary>
        public int RowLength { get; }

        /// <summary>
        /// 列数（不包括注释列）
        /// </summary>
        public int ColCount { get { return Tokens.Count; } }

        public int FileLength { get { return RowCount * RowLength; } }

        public int IdColIndex { get; }

        public string PrimaryColCsType { get; }

        /// <summary>
        /// sheet里每列变量的偏移(相对行首)
        /// </summary>
        public readonly List<int> ColOffset = new List<int>();

        /// <summary>
        /// 类型列表: 如果是list, 值 = 100 + 元素类型值， 
        /// 如果是dict 值 = 10000 + key类型 * 100 + val类型
        /// </summary>
        public readonly List<int> Tokens = new List<int>();

        /// <summary>
        /// 变量名称
        /// </summary>
        public readonly List<string> VariableNames = new List<string>();

        /// <summary>
        /// 变量注释
        /// </summary>
        public readonly List<string> Comments = new List<string>();

        public FileInfoData(SheetData data)
        {
            FileName = data.GetExportFileName();
            RowCount = data.rows.Count;
            RowLength = GetRowLength(data.heads);
            ColOffset = GetColOffset(data.heads);
            Tokens = GetTypeToken(data.heads);
            VariableNames = GetVariable(data.heads);
            Comments = GetComment(data.heads);
            IdColIndex = GlobalConfig.Ins.firstColIsPrimary ? 0 : VariableNames.FindIndex((x) => x == GlobalConfig.Ins.idColName);
            PrimaryColCsType = GetCsType(IdColIndex);
        }

        /// <summary>
        /// 获取一行需要占据的字节数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private int GetRowLength(List<HeadData> data)
        {
            int len = 0;
            for (int i = 0; i < data.Count; i++)
            {
                string type = data[i].MainType;
                if (DataTypeHelper.IsBaseType(type))
                    len += DataTypeHelper.GetBaseTypeLen(type);
                else if (type == TypeDefine.vecType)
                {
                    int dimension = int.Parse(data[i].SubType[0]);
                    len += dimension * 4;
                    // MessageBox.Show("di = " + dimension + "  len = " + len);
                }
                else len += 4;  // list 和 dict
            }
            return len;
        }

        /// <summary>
        /// 获取每列相对行首的偏移
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private List<int> GetColOffset(List<HeadData> data)
        {
            List<int> offs = new List<int>();
            int off = 0;
            offs.Add(off);    // 第一列的偏移一定是0
            for (int i = 0; i < data.Count; i++)
            {
                string type = data[i].MainType;
                if (DataTypeHelper.IsBaseType(type))
                {
                    off += DataTypeHelper.GetBaseTypeLen(type);
                }
                else if (type == TypeDefine.vecType)
                {
                    int dimension = int.Parse(data[i].SubType[0]);
                    int len = dimension * 4;
                    off += len;
                }
                else off += 4;
                offs.Add(off);
            }
            offs.RemoveAt(offs.Count - 1);  // 移除最后一个
            return offs;
        }

        private List<int> GetTypeToken(List<HeadData> data)
        {
            List<int> ls = new List<int>();
            for (int i = 0; i < data.Count; i++)
            {
                HeadData head = data[i];
                if (head.MainType == TypeDefine.listType)
                {
                    int baseToken = GetMainTypeToken(head.MainType);
                    int elemToken = GetMainTypeToken(head.SubType[0]);
                    ls.Add((baseToken + elemToken));
                }
                else if (head.MainType == TypeDefine.vecType)
                {
                    int baseToken = GetMainTypeToken(head.MainType);
                    int dimen = int.Parse(head.SubType[0]);
                    int valToken = string.Empty == head.SubType[1] ? 0 : GetMainTypeToken(head.SubType[1]);
                    ls.Add(baseToken + dimen * 100 + valToken);
                }
                else if (head.MainType == TypeDefine.dictType)
                {
                    int baseToken = GetMainTypeToken(head.MainType);
                    int keyToken = GetMainTypeToken(head.SubType[0]);
                    int valToken = GetMainTypeToken(head.SubType[1]);
                    ls.Add(baseToken + keyToken * 100 + valToken);
                }
                else ls.Add(GetMainTypeToken(head.MainType));
            }
            return ls;
        }

        private List<string> GetVariable(List<HeadData> data)
        {
            List<string> ls = new List<string>();
            for (int i = 0; i < data.Count; i++)
            {
                ls.Add(data[i].Name);
            }
            return ls;
        }

        private List<string> GetComment(List<HeadData> data)
        {
            List<string> ls = new List<string>();
            for (int i = 0; i < data.Count; i++)
            {
                ls.Add(data[i].Comment);
            }
            return ls;
        }

        private int GetMainTypeToken(string type)
        {
            switch (type)
            {
                case TypeDefine.boolType: return (int)TypeToken.Bool;
                case TypeDefine.sbyteType: return (int)TypeToken.SByte;
                case TypeDefine.byteType: return (int)TypeToken.Byte;
                case TypeDefine.shortType: return (int)TypeToken.Short;
                case TypeDefine.ushortType: return (int)TypeToken.UShort;
                case TypeDefine.uintType: return (int)TypeToken.UInt;
                case TypeDefine.intType: return (int)TypeToken.Int;
                case TypeDefine.floatType: return (int)TypeToken.Float;
                case TypeDefine.ulongType: return (int)TypeToken.ULong;
                case TypeDefine.longType: return (int)TypeToken.Long;
                case TypeDefine.doubleType: return (int)TypeToken.Double;
                case TypeDefine.stringType: return (int)TypeToken.String;
                case TypeDefine.listType: return (int)TypeToken.List;
                case TypeDefine.dictType: return (int)TypeToken.Dictionary;
                case TypeDefine.vecType: return (int)TypeToken.Vector;
                default: return (int)TypeToken.Null;
            }
        }

        public string GetCommentTypeByToken(int token)
        {
            if (token >= (int)TypeToken.Vector)
            {
                int dimen = (token - 20000) / 100;
                int valToken = (token - 20000) % 100;
                string type = string.Empty;
                if (valToken != 0) type = ((TypeToken)valToken).ToString();
                return "Vector" + (dimen).ToString() + type;
            }
            else if (token >= (int)TypeToken.Dictionary)
            {
                int keyToken = (token - 10000) / 100;
                int valToken = (token - 10000) % 100;
                return "Dict&lt;" + ((TypeToken)keyToken).ToString() + " ,"
                    + ((TypeToken)valToken).ToString() + "&gt;";
            }
            else if (token >= (int)TypeToken.List)
            {
                int subToken = token - 100;
                return "List&lt;" + ((TypeToken)subToken).ToString() + "&gt;";
            }
            else
            {
                return ((TypeToken)token).ToString();
            }
        }

        public string GetCsType(int index)
        {
            int type = Tokens[index];
            if (type >= (int)TypeToken.Vector)
            {
                int dimen = (type - 20000) / 100;
                int valToken = (type - 20000) % 100;
                string s = "Vector" + (dimen).ToString();
                if (valToken != 0) s += "Int";
                return s;
            }
            else if (type >= (int)TypeToken.Dictionary)
            {
                int keyToken = (type - 10000) / 100;
                int valToken = (type - 10000) % 100;
                return "Dictionary<" + ((TypeToken)keyToken).ToString().ToLower() + " ,"
                    + ((TypeToken)valToken).ToString().ToLower() + ">";
            }
            else if (type >= (int)TypeToken.List)
            {
                int subToken = type - 100;
                return "List<" + ((TypeToken)subToken).ToString().ToLower() + ">";
            }
            else
            {
                return ((TypeToken)type).ToString().ToLower();
            }
        }

        public string GetPropertyStr(int index, string varName)
        {
            string csType = GetCsType(index);
            int type = Tokens[index];
            int off = (index << 16) + ColOffset[index]; 
            if (type >= (int)TypeToken.Vector)
            {
                int dimen = (type - 20000) / 100;
                int valToken = (type - 20000) % 100;
                return "public " + csType + " " + varName +
                    " { get { return ExcelDataAccess.Get" + csType + "<" +
                      PrimaryColCsType +
                    ">(ExcelName." + FileName + ", primaryColVal, " + off.ToString() + "); } }";
            }
            else if (type >= (int)TypeToken.Dictionary)
            {
                int keyToken = (type - 10000) / 100;
                int valToken = (type - 10000) % 100;
                string keyType = ((TypeToken)keyToken).ToString().ToLower();
                string valType = ((TypeToken)valToken).ToString().ToLower();
                return "public Dictionary<" + keyType + ", " + valType + "> " + varName +
                    " { get { return ExcelDataAccess.GetDict<" +
                    keyType + ", " + valType + ", " + PrimaryColCsType +
                    ">(ExcelName." + FileName + ", primaryColVal, " + off.ToString() + "); } }";
            }
            else if (type >= (int)TypeToken.List)
            {
                int subToken = type - 100;
                return "public " + csType + " " + varName +
                    " { get { return ExcelDataAccess.GetList<" +
                    ((TypeToken)subToken).ToString().ToLower() + ", " + PrimaryColCsType +
                    ">(ExcelName." + FileName + ", primaryColVal, " + off.ToString() + "); } }";
            }
            else
            {
                return "public " + csType + " " + varName +
                    " { get { return ExcelDataAccess.Get<" + 
                    csType + ", " + PrimaryColCsType + 
                    ">(ExcelName." + FileName + ", primaryColVal, " + off.ToString() + "); } }";
            }
        }
    }
}