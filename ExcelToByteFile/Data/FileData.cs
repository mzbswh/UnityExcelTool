using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelToByteFile
{
    /// <summary>
    /// 存放每个生成的字节file的数据，是对sheetData数据信息的进一步提炼，用于生成总信息文件
    /// </summary>
    public class FileData
    {
        /// <summary>
        /// 生成字节文件名称
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// 解析类型
        /// </summary>
        public int ParseType { get; }

        /// <summary>
        /// 行数
        /// </summary>
        public int RowCount { get; }

        /// <summary>
        /// 列数（不包括注释列）
        /// </summary>
        public int ColCount { get; }

        /// <summary>
        /// sheet里每列变量的偏移(相对行首)
        /// </summary>
        public List<int> ColOffset { get; }

        /// <summary>
        /// 行的长度(一行多少字节)
        /// </summary>
        public int RowLength { get; }

        public int FileLength { get { return RowCount * RowLength; } }

        public FileData(SheetData data)
        {
            FileName = data.GetExportFileName();
            ParseType = 0;
            RowCount = data.rows.Count;
            ColCount = data.heads.Count;
            ColOffset = GetColOffset(data.heads);
            RowLength = GetRowLength(data.heads);
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
                string type = data[i].Type;
                if (DataTypeHelper.IsBaseType(type))
                    len += GetBaseTypeLength(type);
                else if (DataTypeHelper.IsListType(type))
                    len += 4;
                else len += 8;
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
            offs.Add(0);    // 第一列的偏移一定是0
            for (int i = 0; i < data.Count; i++)
            {
                string type = data[i].Type;
                if (DataTypeHelper.IsBaseType(type))
                    offs.Add(GetBaseTypeLength(type));
                else if (DataTypeHelper.IsListType(type))
                    offs.Add(4);
                else offs.Add(8);       // dict
            }
            offs.RemoveAt(offs.Count - 1);  // 移除最后一个
            return offs;
        }

        private int GetBaseTypeLength(string type)
        {
            switch (type)
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