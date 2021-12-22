using System.Collections.Generic;
using System.Windows.Forms;

namespace ExcelToByteFile
{
    /// <summary>
    /// 每个生成的bytes文件对应生成一个ManifestData，最后所有的ManifestData会整体生成一个manifest.bytes文件
    /// </summary>
    public class ManifestData
    {
        /// <summary>
        /// 生成字节文件名称, 无后缀拓展名称
        /// </summary>
        public string ByteFileName { get; }

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
        public int ColCount => Tokens.Count;

        /// <summary>
        /// 对齐数据的长度 (= RowCount * RowLength)
        /// </summary>
        public int AlignLength => RowCount * RowLength;

        public int IdColIndex { get; }

        public string PrimaryColCsType { get; }

        public SheetConfigData SheetConfig { get; }

        public SheetOptimizeData SheetOptimizeData { get; }

        /// <summary>
        /// sheet里每列变量的偏移(相对行首)
        /// </summary>
        public readonly List<int> ColOffset = new List<int>();

        /// <summary>
        /// 类型列表: 如果是list, 值 = 100 + 元素类型值， 
        /// 如果是dict 值 = 10000 + key类型 * 100 + val类型
        /// vec 值 = 20000 + dimension * 100 + val类型
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

        public ManifestData(SheetData data, string exportName)
        {
            ByteFileName = exportName;
            RowCount = data.rows.Count;
            RowLength = GetRowLength(data.heads);
            ColOffset = GetColOffset(data.heads);
            Tokens = GetTypeToken(data.heads);
            VariableNames = GetVariable(data.heads);
            Comments = GetComment(data.heads);
            IdColIndex = data.PrimaryColIndex;
            PrimaryColCsType = DataTypeHelper.GetType(Tokens[IdColIndex]);
            SheetConfig = data.SheetConfig;
            SheetOptimizeData = data.SheetOptimizeData;
        }

        public int GetToken(int index)
        {
            return Tokens[index];
        }

        /// <summary>
        /// 获取一行需要占据的字节数
        /// </summary>
        private int GetRowLength(List<HeadData> data)
        {
            int len = 0;
            for (int i = 0; i < data.Count; i++)
            {
                string type = data[i].MainType;
                if (DataTypeHelper.IsBaseType(type))
                    len += DataTypeHelper.GetBaseTypeLen(type);
                else if (type == TypeDef.vecType)
                {
                    int dimension = int.Parse(data[i].SubType[0]);
                    len += dimension * 4;
                }
                else len += 4;  // list 和 dict
            }
            return len;
        }

        /// <summary>
        /// 获取每列相对行首的偏移
        /// </summary>
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
                else if (type == TypeDef.vecType)
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
                if (head.MainType == TypeDef.listType)
                {
                    int baseToken = GetMainTypeToken(head.MainType);
                    int elemToken = GetMainTypeToken(head.SubType[0]);
                    ls.Add((baseToken + elemToken));
                }
                else if (head.MainType == TypeDef.vecType)
                {
                    int baseToken = GetMainTypeToken(head.MainType);
                    int dimen = int.Parse(head.SubType[0]);
                    int valToken = string.Empty == head.SubType[1] ? 0 : GetMainTypeToken(head.SubType[1]);
                    ls.Add(baseToken + dimen * 100 + valToken);
                }
                else if (head.MainType == TypeDef.dictType)
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
                case TypeDef.boolType: return (int)TypeToken.Bool;
                case TypeDef.sbyteType: return (int)TypeToken.Sbyte;
                case TypeDef.byteType: return (int)TypeToken.Byte;
                case TypeDef.shortType: return (int)TypeToken.Short;
                case TypeDef.ushortType: return (int)TypeToken.UShort;
                case TypeDef.uintType: return (int)TypeToken.UInt;
                case TypeDef.intType: return (int)TypeToken.Int;
                case TypeDef.floatType: return (int)TypeToken.Float;
                case TypeDef.ulongType: return (int)TypeToken.ULong;
                case TypeDef.longType: return (int)TypeToken.Long;
                case TypeDef.doubleType: return (int)TypeToken.Double;
                case TypeDef.stringType: return (int)TypeToken.String;
                case TypeDef.listType: return (int)TypeToken.List;
                case TypeDef.dictType: return (int)TypeToken.Dictionary;
                case TypeDef.vecType: return (int)TypeToken.Vector;
                default: return (int)TypeToken.Null;
            }
        }
    }
}