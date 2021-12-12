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
		/// 一行的数据类
		/// </summary>
		public IRow Row { get; }

		/// <summary>
		/// 单元格数据列表
		/// </summary>
		public List<string> ValueList { get; }

		public RowData(IRow row, List<string> valList)
		{
			Row = row;
			ValueList = valList;
		}

        public string this[int index] => ValueList[index];

    }
}
