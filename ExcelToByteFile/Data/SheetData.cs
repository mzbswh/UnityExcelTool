using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;

namespace ExcelToByteFile
{
	/// <summary>
	/// sheet表一行数据
	/// </summary>
	public struct RowData
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
	}

	/// <summary>
	/// sheet表头部数据，每个数据列都对应一个头数据
	/// </summary>
	public struct HeadData
    {
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
		/// 所在第几列
		/// </summary>
		public int CellNum { get; }

		public HeadData(string name, string type, string comment, int cellNum)
		{
			Name = name;
			Type = type;
			Comment = comment;
			CellNum = cellNum;
		}
	}

    /// <summary>
    /// excel里一个sheet的数据类
    /// </summary>
    public class SheetData
    {
        /// <summary>
        /// Sheet名称
        /// </summary>
        public string SheetName { get; }

		/// <summary>
		/// Sheet所属的WorkBook
		/// </summary>
        private IWorkbook _workbook = null;

		/// <summary>
		/// Sheet表引用
		/// </summary>
        private ISheet _sheet = null;

		/// <summary>
		/// 公式计算器
		/// </summary>
		private XSSFFormulaEvaluator _evaluator = null;

		public XSSFFormulaEvaluator Evaluator { get { return _evaluator; } }

		/// <summary>
		/// 表格头部数据, 从左到右顺序存储
		/// </summary>
		public readonly List<HeadData> heads = new List<HeadData>();

		/// <summary>
		/// 表格所有行数据, 自上而下顺序存储
		/// </summary>
		public readonly List<RowData> rows = new List<RowData>();

		public SheetData(string sheetName, IWorkbook workbook, ISheet sheet, XSSFFormulaEvaluator evaluator)
        {
            SheetName = sheetName;
			_workbook = workbook;
			_sheet = sheet;
			_evaluator = evaluator;
		}

        /// <summary>
        /// 加载sheet
        /// </summary>
        public void Load()
        {
			// 检测合并单元格
			int merge = _sheet.NumMergedRegions;
			if (merge > 0) throw new Exception($"不支持合并单元格，请移除后从新生成：{_sheet.GetMergedRegion(0).FormatAsString()}");

			int curRow = _sheet.FirstRowNum;		// 当前行数

			// 数据头一共三行
			if (_sheet.LastRowNum < ConstDefine.headRowNum)
            {
				throw new Exception($"行数错误");
			}
			IRow typeRow = _sheet.GetRow(curRow++);		//类型
			IRow nameRow = _sheet.GetRow(curRow++);		//名称
			IRow commentRow = _sheet.GetRow(curRow++);  //备注

			int endCol = typeRow.LastCellNum;
			// 获取数据类型信息
			for (int index = typeRow.FirstCellNum; index < endCol; index++)
			{
				ICell typeCell = typeRow.GetCell(index);
				ICell nameCell = nameRow.GetCell(index);
				ICell commentCell = commentRow.GetCell(index);

				// 检测重复的列
				string type = ExcelTools.GetCellValue(typeCell, _evaluator);
				string name = ExcelTools.GetCellValue(nameCell, _evaluator);
				string comment = ExcelTools.GetCellValue(commentCell, _evaluator);
				bool isNotesCol = type.Contains(ConstDefine.noteChar);
				if (!isNotesCol)
				{
					if (string.IsNullOrEmpty(type))
						throw new Exception($"检测到空列：第{index}列");
					else if (!DataTypeHelper.IsValidType(type))
						throw new Exception($"错误的数据类型：第{index}列, {type}");
					else if (IsNameExist(name))
						throw new Exception($"检测到重复变量名称 : 第{index}列, {name}");

					HeadData head = new HeadData(name, type, comment, index);
					heads.Add(head);
				}
			}

			// 如果没有ID列
			if (!IsNameExist(ConstDefine.idColName))
			{
				throw new Exception("表格必须设立一个 'id' 列.");
			}

			// 所有数据行
			int tableBeginRowNum = ++curRow; //Table初始行
			for (int rowNum = tableBeginRowNum; rowNum <= _sheet.LastRowNum; rowNum++)
			{
				IRow row = _sheet.GetRow(rowNum);

				// 如果是注释行，就跳过
				if (IsNoteRow(row)) continue;
                // 如果是结尾行
                if (IsEndRow(row)) break;

				List<string> vals = ExcelTools.GetOneRowData(this, row); 
				RowData rowData = new RowData(rowNum, row, vals);
				rows.Add(rowData);
			}
		}

        public void Export(string path)
        {
			ExportMgr.ExportOneSheet(path, this);
        }

		/// <summary>
		/// 是否是注释行
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		public bool IsNoteRow(IRow row)
        {
			ICell firstCell = row.GetCell(row.FirstCellNum);
			string value = ExcelTools.GetCellValue(firstCell, _evaluator);
			return value.ToLower().Contains(ConstDefine.noteChar);
		}

		/// <summary>
		/// 此变量名称是否已经存在
		/// </summary>
		/// <param name="headName"></param>
		/// <returns></returns>
		public bool IsNameExist(string name)
        {
			for (int i = 0; i < heads.Count; i++)
			{
				if (heads[i].Name == name) return true;
			}
			return false;
		}

		/// <summary>
		/// 是否是结束行
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		public bool IsEndRow(IRow row)
        {
			if (row == null)
				return true;

			ICell firstCell = row.GetCell(row.FirstCellNum);
			if (firstCell == null)
				return true;

			string value = ExcelTools.GetCellValue(firstCell, _evaluator);
			if (string.IsNullOrEmpty(value))
				return true;

			return false;
		}

		/// <summary>
		/// 获取sheet页
		/// </summary>
		/// <returns></returns>
		public ISheet GetSheet()
		{
			return _sheet;
		}
    }
}
