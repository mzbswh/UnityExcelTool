using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.Windows.Forms;

namespace ExcelToByteFile
{
    /// <summary>
    /// excel里一个sheet的数据类
    /// </summary>
    public class SheetData
    {
		/// <summary>
		/// 所属Excel的名称, 用于设置导出名称
		/// </summary>
		public string ExcelName { get; }

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

		/// <summary>
		/// 解析类型
		/// </summary>
		public byte ParseType { get; private set; }

		public SheetData(IWorkbook workbook, ISheet sheet, XSSFFormulaEvaluator evaluator, string excelName)
        {
			ExcelName = excelName;
			SheetName = sheet.SheetName;
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
			//int merge = _sheet.NumMergedRegions;
			//if (merge > 0) throw new Exception($"不支持合并单元格，请移除后从新生成：{_sheet.GetMergedRegion(0).FormatAsString()}");
			//Log.LogError($"不支持合并单元格，请移除后从新生成：{_sheet.GetMergedRegion(0).FormatAsString()}");
			int curRow = _sheet.FirstRowNum;        // 当前行数
			// 数据头一共三行
			if (_sheet.LastRowNum < ConstDefine.headFixedRowNum - 1)
            {
				//Log.LogError($"Excel: {ExcelName} Sheet: {SheetName} 行数错误");
				Log.LogError($"Excel: {ExcelName} Sheet: {SheetName} 行数错误");
			}

			IRow commentRow, typeRow, nameRow;
			
			if (GlobalConfig.Ins.commentInFirstRow)
			{
				//MessageBox.Show($"{ExcelName} {_sheet.GetRow(1) == null}");
				commentRow = _sheet.GetRow(curRow++);  //备注: 可能为空
				typeRow = _sheet.GetRow(curRow++);     //类型
				nameRow = _sheet.GetRow(curRow++);     //名称
			}
			else
            {
				typeRow = _sheet.GetRow(curRow++);     //类型
				nameRow = _sheet.GetRow(curRow++);     //名称
				commentRow = _sheet.GetRow(curRow++);  //备注: 可能为空
			}
			int endCol = typeRow.LastCellNum;
			// 获取数据类型信息
			for (int index = typeRow.FirstCellNum; index < endCol; index++)
			{
				ICell typeCell = typeRow.GetCell(index);
				ICell nameCell = nameRow.GetCell(index);
				ICell commentCell = commentRow?.GetCell(index);
				
				// 检测重复的列
				string type = ExcelTool.GetCellValue(typeCell, _evaluator).Trim().ToLower();
				string name = ExcelTool.GetCellValue(nameCell, _evaluator).Trim();
				string comment = ExcelTool.GetCellValue(commentCell, _evaluator);
				bool isNotesCol = (type.Contains(ConstDefine.noteChar)) 
					|| (GlobalConfig.Ins.typeNullIsNoteCol && (type == string.Empty));
				if (!isNotesCol)
				{
					if (string.IsNullOrEmpty(type))
                    {
						//Log.LogError($"检测到空列：第{index}列");
						Log.LogError($"Excel: {ExcelName} Sheet: {SheetName} 检测到空列：第{index}列");
					}
					else if (!DataTypeHelper.IsValidType(type))
                    {
						//Log.LogError($"错误的数据类型：第{index}列, {type}");
						Log.LogError($"错误的数据类型：Excel: {ExcelName} Sheet: {SheetName} 第{index}列, {type}");
					}
					else if (IsNameExist(name))
                    {
						//Log.LogError($"检测到重复变量名称 : 第{index}列, {name}");
						Log.LogError($"检测到重复变量名称 : Excel: {ExcelName} Sheet: {SheetName} 第{index}列, {name}");
					}

					string processedType = DataTypeHelper.GetMainType(type);
					string[] subType = DataTypeHelper.GetSubType(type);
					HeadData head = new HeadData(name, processedType, subType, comment, index);
					heads.Add(head);
				}
			}

			// 如果没有ID列
			if (!IsNameExist(GlobalConfig.Ins.idColName))
			{
				Log.LogError($"{ExcelName}_{SheetName}表格必须设立一个 'id' 列.");
			}

			curRow += GlobalConfig.Ins.skipRowBeginRead;
			// 所有数据行
			for (; curRow <= _sheet.LastRowNum; curRow++)
			{
				IRow row = _sheet.GetRow(curRow);

				if (row == null) continue;
				// 如果是注释行，就跳过
				if (IsNoteRow(row)) continue;
                // 如果是结尾行
                if (IsEndRow(row)) break;

				List<string> vals = ExcelTool.GetOneRowData(this, row); 
				RowData rowData = new RowData(curRow, row, vals);
				rows.Add(rowData);
			}
			ParseType = 0;
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
			string value = ExcelTool.GetCellValue(firstCell, _evaluator);
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

			string value = ExcelTool.GetCellValue(firstCell, _evaluator);
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

		public string GetExportFileName()
		{
			if (_workbook.NumberOfSheets == 1 || GlobalConfig.Ins.onlyOneSheet)
				return ExcelName;
			else return ExcelName + "_" + SheetName;
		}
	}
}
