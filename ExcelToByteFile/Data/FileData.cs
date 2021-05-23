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
                switch (data[i].Type)
                {
                    case TypeDefine.sbyteType: len += 1; break;
                    case TypeDefine.boolType: len += 1; break;
                    case TypeDefine.byteType: len += 1; break;
                    case TypeDefine.dictType: len += 4; break;
                    case TypeDefine.doubleType: len += 8; break;
                    case TypeDefine.floatType: len += 4; break;
                    case TypeDefine.intType: len += 4; break;
                    case TypeDefine.listType: len += 4; break;
                    case TypeDefine.longType: len += 8; break;
                    case TypeDefine.shortType: len += 2; break;
                    case TypeDefine.stringType: len += 4; break;
                    case TypeDefine.uintType: len += 4; break;
                    case TypeDefine.ulongType: len += 8; break;
                    case TypeDefine.ushortType: len += 2; break;
                }
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
                switch (data[i].Type)
                {
                    case TypeDefine.sbyteType: offs.Add(1); break;
                    case TypeDefine.boolType: offs.Add(1); break;
                    case TypeDefine.byteType: offs.Add(1); break;
                    case TypeDefine.dictType: offs.Add(4); break;
                    case TypeDefine.doubleType: offs.Add(8); break;
                    case TypeDefine.floatType: offs.Add(4); break;
                    case TypeDefine.intType: offs.Add(4); break;
                    case TypeDefine.listType: offs.Add(4); break;
                    case TypeDefine.longType: offs.Add(8); break;
                    case TypeDefine.shortType: offs.Add(2); break;
                    case TypeDefine.stringType: offs.Add(4); break;
                    case TypeDefine.uintType: offs.Add(4); break;
                    case TypeDefine.ulongType: offs.Add(8); break;
                    case TypeDefine.ushortType: offs.Add(2); break;
                    
                }
            }
            offs.RemoveAt(offs.Count - 1);  // 移除最后一个
            return offs;
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