using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelToByteFile
{
	/// <summary>
	/// sheet表一行数据
	/// </summary>
	public class RowData
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
		/// 单元格数据列表
		/// </summary>
		public List<string> ValueList { get; }

		public RowData(int rowNum, IRow row, List<string> valList)
		{
			RowNum = rowNum;
			Row = row;
			ValueList = valList;
		}

		public string this[int index]
        {
			get
            {
				return ValueList[index];
            }
        }

	}
}
