using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelToByteFile
{
    /// <summary>
    /// sheet表头部，每列都对应一个头数据
    /// </summary>
    public class HeadData
    {
		/// <summary>
		/// 列号
		/// </summary>
		public int CellNum { get; }

		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// 类型
		/// </summary>
		public string Type { get; }

		/// <summary>
		/// 类型备注
		/// </summary>
		public string Comment { get; }

		/// <summary>
		/// 是否是注释列
		/// </summary>
		public bool IsNotes { get; }

		public HeadData(int cellNum, string name, string type, string comment)
		{
			CellNum = cellNum;
			Name = name;
			Type = type;
			Comment = comment;

			// 检测是否为策划注释列
			IsNotes = name.Contains(ConstDefine.noteChar) || type.Contains(ConstDefine.noteChar);
		}
	}
}
