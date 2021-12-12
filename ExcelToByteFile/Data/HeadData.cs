using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelToByteFile
{
	/// <summary>
	/// 保存列信息：列名、类型等
	/// </summary>
	public class HeadData
	{
        /// <summary>
        /// 变量名称
        /// </summary>
		public string Name { get; }

		/// <summary>
		/// 类型: 基本类型 列表为list 字典为dict
		/// </summary>
		public string MainType { get; }

		/// <summary>
		/// 如果是list，dict, vector，会有子类型
		/// </summary>
		public string[] SubType { get; }

		/// <summary>
		/// 类型备注
		/// </summary>
		public string Comment { get; }

		/// <summary>
		/// 所在第几列，从0开始, 对应excel表格的列
		/// </summary>
		public int CellNum { get; }

        /// <summary>
        /// 是否是主列，主列将作为索引
        /// </summary>
        public bool Primary { get; }

		public HeadData(string name, string type,string[] subType, string comment, int cellNum, bool primary)
		{
			Name = name;
			MainType = type;
			SubType = subType;
			Comment = comment;
			CellNum = cellNum;
            Primary = primary;
		}
	}
}
