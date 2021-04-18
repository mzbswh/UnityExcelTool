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
    /// excel里一个sheet的数据类
    /// </summary>
    public class SheetData
    {
        /// <summary>
        /// 页表名称
        /// </summary>
        public string SheetName { get; }

        private IWorkbook _workbook = null;

        private ISheet _sheet = null;

		/// <summary>
		/// 表格头部数据
		/// </summary>
		public readonly List<HeadData> heads = new List<HeadData>();

		public readonly List<TableData> tables = new List<TableData>();

		// 公式计算器
		private XSSFFormulaEvaluator _evaluator = null;

		public SheetData(string sheetName)
        {
            SheetName = sheetName;
        }

        /// <summary>
        /// 加载sheet
        /// </summary>
        public void Load(IWorkbook workbook, ISheet sheet)
        {
			_workbook = workbook;
			_sheet = sheet;

			// 检测合并单元格
			int MergedCount = _sheet.NumMergedRegions;
			for (int i = 0; i < MergedCount; i++)
			{
				var region = sheet.GetMergedRegion(i);
				throw new Exception($"导表工具不支持合并单元格，请移除合并单元格：{region.FormatAsString()}");
			}

			// 公式计算器
			_evaluator = new XSSFFormulaEvaluator(_workbook);

			int firstRowNum = sheet.FirstRowNum;

			// 数据头一共三行
			IRow row1 = sheet.GetRow(firstRowNum); //类型
			IRow row2 = sheet.GetRow(++firstRowNum); //名称
			IRow row3 = sheet.GetRow(++firstRowNum); //备注

			// 检测策划备注行
			while (true)
			{
				int checkRow = firstRowNum + 1;
				if (checkRow > sheet.LastRowNum)
					break;
				IRow row = sheet.GetRow(checkRow);
				if (IsNoteRow(row))
					++firstRowNum;
				else
					break;
			}

			// 组织头部数据
			for (int cellNum = row1.FirstCellNum; cellNum < row1.LastCellNum; cellNum++)
			{
				ICell row1cell = row1.GetCell(cellNum);
				ICell row2cell = row2.GetCell(cellNum);
				ICell row3cell = row3.GetCell(cellNum);

				// 检测重复的列
				string headName = GetCellValue(row1cell);
				bool isNotesRow = headName.Contains(ConstDefine.noteChar);
				if (isNotesRow == false)
				{
					if (string.IsNullOrEmpty(headName))
						throw new Exception($"检测到空列：第{++cellNum}列");
					if (IsContainsHead(headName))
						throw new Exception($"检测到重复列 : {headName}");
				}

				// 创建Wrapper
				string type = GetCellValue(row1cell);
				string name = GetCellValue(row2cell);
				string comment = GetCellValue(row3cell);
				HeadData wrapper = new HeadData(cellNum, name, type, comment);
				heads.Add(wrapper);
			}

			// 如果没有ID列
			if (IsContainsHead(ConstDefine.idColName) == false)
			{
				throw new Exception("表格必须设立一个 'id' 列.");
			}

			// 所有数据行
			int tableBeginRowNum = ++firstRowNum; //Table初始行
			for (int rowNum = tableBeginRowNum; rowNum <= sheet.LastRowNum; rowNum++)
			{
				IRow row = sheet.GetRow(rowNum);

				// 如果是结尾行
				if (IsEndRow(row))
					break;

				TableData wrapper = new TableData(rowNum, row);
				wrapper.CacheAllCellValue(this);
				tables.Add(wrapper);
			}

			//// 创建所有注册的导出器
			//for (int i = 0; i < ExportHandler.ExportTypes.Count; i++)
			//{
			//	Type type = ExportHandler.ExportTypes[i];
			//	BaseExporter exporter = (BaseExporter)Activator.CreateInstance(type, this);
			//	_exporters.Add(exporter);
			//}
		}

        public void Export(string path)
        {
			ExportMgr.ExportFile(path, this);
        }

		/// <summary>
		/// 是否是注释行
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		public bool IsNoteRow(IRow row)
        {
			ICell firstCell = row.GetCell(row.FirstCellNum);
			string value = GetCellValue(firstCell);
			return value.ToLower().Contains(ConstDefine.noteChar);
		}

		public bool IsContainsHead(string headName)
        {
			for (int i = 0; i < heads.Count; i++)
			{
				if (heads[i].Name == headName)
					return true;
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

			string value = GetCellValue(firstCell);
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

		/// <summary>
		/// 获取单元格的值
		/// </summary>
		/// <param name="cell"></param>
		/// <returns></returns>
		public string GetCellValue(ICell cell)
        {
			// 注意：内容为空的单元格有时候会为空对象
			if (cell == null)
				return string.Empty;

			if (cell.CellType == CellType.Blank)
			{
				return string.Empty;
			}
			else if (cell.CellType == CellType.Numeric)
			{
				return cell.NumericCellValue.ToString();
			}
			else if (cell.CellType == CellType.String)
			{
				return cell.StringCellValue;
			}
			else if (cell.CellType == CellType.Boolean)
			{
				return cell.BooleanCellValue.ToString().ToLower();
			}
			else if (cell.CellType == CellType.Formula)
			{
				// 注意：公式只支持数值和字符串类型
				var formulaValue = _evaluator.Evaluate(cell);
				if (formulaValue.CellType == CellType.Numeric)
					return formulaValue.NumberValue.ToString();
				else if (formulaValue.CellType == CellType.String)
					return formulaValue.StringValue;
				else
					throw new Exception($"未支持的公式类型 : {formulaValue.CellType}");
			}
			else
			{
				throw new Exception($"未支持的单元格类型 : {cell.CellType}");
			}
		}
    }
}
