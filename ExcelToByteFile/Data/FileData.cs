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
        /// sheet表数量
        /// </summary>
        public int SheetCount { get; }

        /// <summary>
        /// sheet名称
        /// </summary>
        public List<string> SheetNames { get; }

        /// <summary>
        /// 每个sheet在byte文件里的起始偏移
        /// </summary>
        public List<int> SheetStartIndex { get; }

        /// <summary>
        /// 每个sheet解析类型
        /// </summary>
        public List<int> SheetParseType { get; }

        /// <summary>
        /// 每个sheet行数
        /// </summary>
        public List<int> SheetRowCount { get; }

        /// <summary>
        /// 每个sheet列数（不包括注释列）
        /// </summary>
        public List<int> SheetColCount { get; }

        /// <summary>
        /// sheet里每列变量的偏移(相对行首)
        /// </summary>
        public List<List<int>> SheetColOffset { get; }

        /// <summary>
        /// 行的长度(一行多少字节)
        /// </summary>
        public List<int> SheetRowLength { get; }

        public FileData(ExcelData data)
        {
            FileName = data.ExcelName + ".bytes";
            SheetCount = data.sheetDataList.Count;

            SheetNames = new List<string>();
            SheetStartIndex = new List<int>();
            SheetParseType = new List<int>();
            SheetRowCount = new List<int>();
            SheetColCount = new List<int>();
            SheetColOffset = new List<List<int>>();
            SheetRowLength = new List<int>();

            for (int i = 0; i < SheetCount; i++)
            {
                SheetData sheet = data.sheetDataList[i];
                int rowLength = GetRowLength(sheet.heads);
                List<int> colOffs = GetColOffset(sheet.heads);
                int oneSheetLen = rowLength * sheet.rows.Count;
                SheetNames.Add(sheet.SheetName);
                SheetRowCount.Add(sheet.rows.Count);
                SheetColCount.Add(sheet.heads.Count);
                SheetParseType.Add(sheet.ParseType);
                SheetStartIndex.Add(i * oneSheetLen);
                SheetColOffset.Add(colOffs);
                SheetRowLength.Add(rowLength);
            }
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
                else len += GetDictTypeLength(data[i].SubType);
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
                else offs.Add(GetDictTypeLength(data[i].SubType));
            }
            offs.RemoveAt(offs.Count - 1);  // 移除最后一个
            return offs;
        }

        private int GetDictTypeLength(string[] subType)
        {
            return GetBaseTypeLength(subType[0]) + GetBaseTypeLength(subType[1]);
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

        /// <summary>
        /// 获取对齐数据的长度
        /// </summary>
        /// <returns></returns>
        public int GetAlignedDataTotalSize()
        {
            int len = 0;
            for (int i = 0; i < SheetCount; i++)
            {
                len += SheetRowCount[i] * SheetRowLength[i];
            }
            return len;
        }
    }
}