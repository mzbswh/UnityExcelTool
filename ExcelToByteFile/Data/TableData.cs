using System;
using System.Collections.Generic;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;

namespace ExcelToByteFile
{
    /// <summary>
    /// sheet表一行数据
    /// </summary>
    public class TableData
    {
        /// <summary>
        /// 行号
        /// </summary>
        public int RowNum { get; }

        /// <summary>
        /// 一行的数据类
        /// </summary>
        public IRow Row { get; }

        /// <summary>
        /// 单元格数据字典：cellnum -> cellval
        /// </summary>
        private Dictionary<int, string> cellValDict = new Dictionary<int, string>();

        public TableData(int rowNum, IRow row)
        {
            RowNum = rowNum;
            Row = row;
        }

		/// <summary>
		/// 缓存单元格数据
		/// </summary>
		public void CacheAllCellValue(SheetData sheet)
		{
			for (int i = 0; i < sheet.heads.Count; i++)
			{
				HeadData head = sheet.heads[i];

				// 如果是备注列
				if (head.IsNotes)
				{
					cellValDict.Add(head.CellNum, string.Empty);
					continue;
				}

				// 获取单元格字符串
				ICell cell = Row.GetCell(head.CellNum);
				string value = sheet.GetCellValue(cell);

				// 检测数值单元格是否为空值		
				if (string.IsNullOrEmpty(value))
				{
					if (head.Type == "int" || head.Type == "long" || head.Type == "float" || head.Type == "double"
						|| head.Type == "enum" || head.Type == "bool")
					{
						// 如果开启了自动补全功能
						value = 0.ToString();
						//throw new Exception($"数值单元格不能为空，请检查{head.Name}列");
					}
				}

				cellValDict.Add(head.CellNum, value);
			}
		}

		/// <summary>
		/// 获取单元格数据
		/// </summary>
		public string GetCellValue(int cellNum)
		{
			return cellValDict[cellNum];
		}
	}
}
