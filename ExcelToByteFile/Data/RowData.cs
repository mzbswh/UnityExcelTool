using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelToByteFile
{
	public class RowData
	{
		public IRow Row { get; }

		/// <summary>
		/// 一行的所有有效数据
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
