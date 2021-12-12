using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.Windows.Forms;
using ExcelToByteFile.Utils;

namespace ExcelToByteFile
{
    /// <summary>
    /// excel里一个sheet的数据类
    /// </summary>
    public class SheetData
    {
        public string ExcelName => excelData.ExcelName;

        public string SheetName { get; }

        public string ExportName => excelData.sheetDataList.Count <= 1 ? ExcelName : ExcelName + "_" + SheetName;

        public bool CanExport { get; private set; }

        public int IdColIndex { get; private set; }     // 从0开始，不包含注释列

        /// <summary>
        /// 表格头部数据, 从左到右顺序存储
        /// </summary>
        public readonly List<HeadData> heads = new List<HeadData>();

        /// <summary>
        /// 表格所有行数据, 自上而下顺序存储
        /// </summary>
        public readonly List<RowData> rows = new List<RowData>();

        private readonly ISheet sheet = null;

        private readonly ExcelData excelData;

        private int primaryColIndex = -1;

		public SheetData(ISheet sheet, ExcelData excelData)
        {
            this.excelData = excelData;
            this.sheet = sheet;
            SheetName = sheet.SheetName;
		}

        public void Load()
        {
			// 检测合并单元格
			//int merge = _sheet.NumMergedRegions;
			//if (merge > 0) throw new Exception($"不支持合并单元格，请移除后从新生成：{_sheet.GetMergedRegion(0).FormatAsString()}");
			//Log.LogError($"不支持合并单元格，请移除后从新生成：{_sheet.GetMergedRegion(0).FormatAsString()}");
            // 数据头一共三行
            //if (_sheet.LastRowNum < ConstDefine.headFixedRowNum - 1)
            //{
            //	//Log.LogError($"Excel: {ExcelName} Sheet: {SheetName} 行数错误");
            //	Log.LogError($"Excel: {ExcelName} Sheet: {SheetName} 行数错误");
            //}
            
            // 先解析第一个单元格，获取此sheet的导出信息
            IRow row = sheet.GetRow(0);
            ICell cell = row.GetCell(0);
            string[] sheetExportInfo = GetCellValue(cell).Split(Environment.NewLine.ToCharArray());
            ParseSheetExportInfo(sheetExportInfo);
            if (!CanExport) return;

            IRow commentRow = null, typeRow = null, nameRow = null;
            byte ok = 0;
			for (int i = 1; i < sheet.LastRowNum; i++)
            {
                row = sheet.GetRow(i);
                RowLabel lab = GetRowLabel(row);
                switch (lab)
                {
                    case RowLabel.Type: typeRow = row; ok |= 1; break;
                    case RowLabel.Name: nameRow = row; ok |= 2; break;
                    case RowLabel.Comment: commentRow = row; ok |= 4; break;
                }
                if (ok == 7) break;
            }
            if (typeRow == null || nameRow == null)
            {
                Log.LogError($"Excel: {ExcelName} Sheet: {SheetName} 没有检测到类型行或名字行");
            }
			int endCol = typeRow.LastCellNum;
            bool existIdCol = false;
			// 获取数据类型信息
			for (int index = 1; index < endCol; index++)
			{
                ColLabel lab = GetColLabel(index);
                if (lab == ColLabel.None || lab == ColLabel.Note) continue;
				ICell typeCell = typeRow.GetCell(index);
				ICell nameCell = nameRow.GetCell(index);
				ICell commentCell = commentRow?.GetCell(index);
				
				// 检测重复的列
				string type = GetCellValue(typeCell).Trim().Replace(" ","").ToLower();
				string name = GetCellValue(nameCell).Trim().Replace(" ", "");
				string comment = GetCellValue(commentCell);
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
                else if (ExistName(name))
                {
                    //Log.LogError($"检测到重复变量名称 : 第{index}列, {name}");
                    Log.LogError($"检测到重复变量名称 : Excel: {ExcelName} Sheet: {SheetName} 第{index}列, {name}");
                }
                string processedType = DataTypeHelper.GetMainType(type);
                string[] subType = DataTypeHelper.GetSubType(type);
                bool primary = lab == ColLabel.Primary;
                existIdCol |= primary;
                HeadData head = new HeadData(name, processedType, subType, comment, index, primary);
                heads.Add(head);
                if (primary)
                {
                    primaryColIndex = index;
                    IdColIndex = heads.Count - 1;
                }
            }

			// 如果没有ID列
			if (!existIdCol)
			{
				Log.LogError($"{ExcelName}_{SheetName}表格必须设立一个 'id' 列.");
			}

			// 所有数据行
			for (int i = 1; i <= sheet.LastRowNum; i++)
			{
				row = sheet.GetRow(i);
                var lab = GetRowLabel(row);
				if (lab != RowLabel.None) continue;
                if (IsEndRow(row)) break; 

                List<string> vals = GetOneRowData(row); 
				RowData rowData = new RowData(row, vals);
				rows.Add(rowData);
			}
		}

        private void ParseSheetExportInfo(string[] sheetExportInfo)
        {
            bool reverse = false;
            string value;
            for (int i = 0; i < sheetExportInfo.Length; i++)
            {
                value = string.Empty;
                string keyword = sheetExportInfo[i].ToLower();
                if (keyword[0] == ConstDefine.reverseChar)
                {
                    keyword = keyword[1..];
                    reverse = true;
                }
                else
                {
                    var temp = keyword.Split('=');
                    if (temp.Length > 1)
                    {
                        keyword = temp[0];
                        value = temp[1];
                    }
                    else
                    {
                        value = string.Empty;
                    }
                }
                
                switch (keyword)
                {
                    case SheetKeyWord.Export:
                        if (reverse)
                        {
                            CanExport = false;
                        }
                        else
                        {
                            if (value == string.Empty)
                            {
                                CanExport = true;
                            }
                            else if (value == ConstDefine.trueWord)
                            {
                                CanExport = true;
                            }
                            else if (value == ConstDefine.falseWord)
                            {
                                CanExport = false;
                            }
                            else
                            {
                                CanExport = false;
                            }
                        }
                        break;
                }
            }
        }

        private RowLabel GetRowLabel(IRow row)
        {
            ICell cell = row.GetCell(0);
            if (cell == null) return RowLabel.None;
            string s = GetCellValue(cell).ToLower();
            if (s == string.Empty)
            {
                return RowLabel.None;
            }
            else if (s == ConstDefine.noteChar.ToString() || s == RowLabel.Note.ToString().ToLower())
            {
                return RowLabel.Note;
            }
            else if (s == RowLabel.Type.ToString().ToLower())
            {
                return RowLabel.Type;
            }
            else if (s == RowLabel.Name.ToString().ToLower())
            {
                return RowLabel.Name;
            }
            else if (s == RowLabel.Comment.ToString().ToLower())
            {
                return RowLabel.Comment;
            }
            return RowLabel.None;
        }

        private ColLabel GetColLabel(int col)
        {
            IRow row = sheet.GetRow(0);
            ICell cell = row.GetCell(col);
            if (cell == null) return ColLabel.None;
            string s = GetCellValue(cell).ToLower();
            if (s == string.Empty)
            {
                return ColLabel.None;
            }
            else if (s == ConstDefine.noteChar.ToString() || s == RowLabel.Note.ToString().ToLower())
            {
                return ColLabel.Note;
            }
            else if (s == ColLabel.Primary.ToString().ToLower() || s == "key")
            {
                return ColLabel.Primary;
            }
            return ColLabel.None;
        }

		public bool IsNoteRow(IRow row) => GetRowLabel(row) == RowLabel.Note;

        public bool IsEndRow(IRow row)
        {
            if (row == null) return true;

            ICell cell = row.GetCell(primaryColIndex);
            if (cell == null) return true;
            string value = GetCellValue(cell);
            return string.IsNullOrEmpty(value);
        }

        public bool ExistName(string name)
        {
			for (int i = 0; i < heads.Count; i++)
			{
				if (heads[i].Name == name) return true;
			}
			return false;
		}

        /// <summary>
        /// 获取一个sheet里的一行数据（仅有效数据，不包含注释列）
        /// </summary>
        public List<string> GetOneRowData(IRow row)
        {
            List<string> ls = new List<string>();
            for (int i = 0; i < heads.Count; i++)
            {
                int cellNum = heads[i].CellNum;

                // 获取单元格字符串
                ICell cell = row.GetCell(cellNum);
                string value = GetCellValue(cell);

                // 检测数值单元格是否为空值
                // 如果开启了自动补全功能且是可以自动补全的类型，就设置其值		
                if (string.IsNullOrEmpty(value))
                {
                    if (GlobalConfig.Ins.autoCompletion)
                    {
                        string type = heads[i].MainType;
                        if (DataTypeHelper.IsBaseType(type) && type != TypeDefine.stringType)
                            value = GlobalConfig.Ins.autoCompletionVal;
                        else if (type == TypeDefine.stringType)
                            value = string.Empty;
                        else
                        {
                            Log.LogError($"此列单元格数值不能为空，第{cellNum}列");
                        }

                    }
                }
                ls.Add(value);
            }
            return ls;
        }

        public string GetCellValue(ICell cell)
        {
            // cell 可能为空值，因此需要判断这种情况
            if (cell == null) return string.Empty;

            switch (cell.CellType)
            {
                case CellType.Blank: return string.Empty;
                case CellType.Numeric: return cell.NumericCellValue.ToString();
                case CellType.String: return cell.StringCellValue;
                case CellType.Boolean: return cell.BooleanCellValue.ToString().ToLower();
                case CellType.Formula:
                {
                    // 公式只支持数值和字符串类型
                    var val = excelData.Evaluator.Evaluate(cell);
                    if (val.CellType == CellType.Numeric)
                        return val.NumberValue.ToString();
                    else if (val.CellType == CellType.String)
                        return val.StringValue;
                    else
                    {
                        Log.LogError($"未支持的公式类型 : {val.CellType}");
                        return string.Empty;
                    }
                }
                default:
                    Log.LogError($"未支持的单元格类型 : {cell.CellType}");
                    return string.Empty;
            }
        }
    }
}
