using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelToByteFile
{
	/// <summary>
	/// sheet表头部数据，每个数据列都对应一个头数据
	/// </summary>
	public class HeadData
	{
		/// <summary>
		/// 名称
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
		/// 所在第几列, 注释列会影响此值
		/// </summary>
		public int CellNum { get; }

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
