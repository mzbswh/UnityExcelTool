using System;
using System.Collections.Generic;
using NPOI.SS.UserModel;
using NPOI.SS.Formula;

namespace ExcelToByteFile
{
    public class SheetData
    {
        public string ExcelName { get; }

        public string Name { get; }

        /// <summary>
        /// 主列索引，在heads的索引，已经去除了注释列
        /// </summary>
        public int PrimaryColIndex { get; private set; }

        public SheetConfigData SheetConfig { get; private set; }

        public SheetOptimizeData SheetOptimizeData { get; private set; }

        /// <summary>
        /// 表格头部数据, 从左到右顺序存储
        /// </summary>
        public readonly List<HeadData> heads = new List<HeadData>();

        /// <summary>
        /// 表格所有行数据, 自上而下顺序存储
        /// </summary>
        public readonly List<RowData> rows = new List<RowData>();

        private readonly ISheet sheet = null;

        private readonly BaseFormulaEvaluator evaluator;

        private int primaryColIndex = -1;   // excel列索引，没有去除注释列

		public SheetData(ISheet sheet, string excelName, BaseFormulaEvaluator evaluator)
        {
            this.sheet = sheet;
            Name = sheet.SheetName;
            ExcelName = excelName;
            this.evaluator = evaluator;
        }

        public void Load()
        {
			// 检测合并单元格
			//int merge = _sheet.NumMergedRegions;
			//if (merge > 0) throw new Exception($"不支持合并单元格，请移除后从新生成：{_sheet.GetMergedRegion(0).FormatAsString()}");
			//Log.LogError($"不支持合并单元格，请移除后从新生成：{_sheet.GetMergedRegion(0).FormatAsString()}");

            // 先解析第一个单元格，获取此sheet的导出信息
            IRow row = sheet.GetRow(0);
            ICell cell = row.GetCell(0);
            SheetConfig = new SheetConfigData(GetCellValue(cell), $"{ExcelName}_{Name}");

            if (!SheetConfig.Export) return;

            // 获取类型行，名字行以及可能存在的注释行
            IRow commentRow = null, typeRow = null, nameRow = null;
            byte ok = 0;
			for (int i = 1; i <= ConstDef.sheetDefRowMax; i++)
            {
                row = sheet.GetRow(i);
                if (row == null) break;
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
                Log.Error($"{ExcelName}_{Name}: 没有检测到类型行或变量名字行");
            }

            // 获取数据类型名字信息
            bool existIdCol = false;
            for (int idx = 1; idx < typeRow.LastCellNum; idx++)
			{
                ColLabel lab = GetColLabel(idx);
                // 最后一个有效列前面要么是有效列要么标记为注释列，不能是None且类型为空
                if (lab == ColLabel.Note) continue;

				ICell typeCell = typeRow.GetCell(idx);
				ICell nameCell = nameRow.GetCell(idx);
				ICell commentCell = commentRow?.GetCell(idx);

                // 检测重复的列
                string type = GetCellValue(typeCell).ToLowerAndRemoveWhiteSpace();
                string name = GetCellValue(nameCell); //.ToLowerAndRemoveWhiteSpace();
                string comment = GetCellValue(commentCell);

                if (string.IsNullOrWhiteSpace(type)) break; // 如果类型是空就认为是到末尾了

                if (string.IsNullOrEmpty(type))
                {
                    Log.Error($"{ExcelName}_{Name}: 检测第{idx}列类型为空，如果是注释列请进行标注");
                }
                else if (!DataTypeHelper.IsValidType(type))
                {
                    Log.Error($"错误的数据类型：{ExcelName}_{Name}: 第{idx}列, 类型={type}");
                }
                else if (ExistName(name))
                {
                    Log.Error($"检测到重复变量名称 : {ExcelName}_{Name} 第{idx}列, {name}");
                }
                // 添加到heads数据里
                string mainType = DataTypeHelper.GetMainType(type);
                string[] subType = DataTypeHelper.GetSubType(type);
                bool primary = lab == ColLabel.Primary;
                existIdCol |= primary;
                HeadData head = new HeadData(name, mainType, subType, comment, idx, primary);
                heads.Add(head);
                if (primary)
                {
                    primaryColIndex = idx;
                    PrimaryColIndex = heads.Count - 1;
                }
            }

			if (!existIdCol)
			{
				Log.Error($"{ExcelName}_{Name}表格必须设立一个主列！");
			}

			// 读取所有数据行
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

            // 优化数据
            if (SheetConfig.Optimize)
            {
                if (DataTypeHelper.CanOptimizeIfPrimaryIs(heads[PrimaryColIndex].MainType))
                {
                    if (rows.Count < 20) return;
                    List<long> nums = new List<long>(rows.Count);
                    for (int i = 0; i < rows.Count; i++)
                    {
                        if (long.TryParse(rows[i][PrimaryColIndex], out long val))
                        {
                            nums.Add(val);
                        }
                        else
                        {
                            return;
                        }
                    }
                    var optimizeType = Optimize.GetOptimizeType(nums);
                    if (optimizeType != OptimizeType.None)
                    {
                        SheetOptimizeData = new SheetOptimizeData(optimizeType, Optimize.segment, Optimize.segmentStart, Optimize.partialContinuityStart, Optimize.step); ;
                    }
                }
            }
        }

        private RowLabel GetRowLabel(IRow row)
        {
            ICell cell = row.GetCell(0);
            if (cell == null) return RowLabel.None;
            string s = GetCellValue(cell).ToLowerAndRemoveWhiteSpace();
            if (s == string.Empty)
            {
                return RowLabel.None;
            }
            else if (s == SymbolDef.noteChar.ToString() || s == RowLabel.Note.ToString().ToLower())
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
            string s = GetCellValue(cell).ToLowerAndRemoveWhiteSpace();
            if (s == string.Empty)
            {
                return ColLabel.None;
            }
            else if (s == SymbolDef.noteChar.ToString() || s == RowLabel.Note.ToString().ToLower())
            {
                return ColLabel.Note;
            }
            else if (s == ColLabel.Primary.ToString().ToLower() || s == SymbolDef.keyWord)
            {
                return ColLabel.Primary;
            }
            return ColLabel.None;
        }

        private bool IsEndRow(IRow row)
        {
            if (row == null) return true;
            ICell cell = row.GetCell(primaryColIndex);
            return cell == null || string.IsNullOrEmpty(GetCellValue(cell));
        }

        private bool ExistName(string name)
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
        private List<string> GetOneRowData(IRow row)
        {
            List<string> ls = new List<string>();
            for (int i = 0; i < heads.Count; i++)
            {
                int cellNum = heads[i].CellNum;
                // 获取单元格字符串
                ICell cell = row.GetCell(cellNum);
                string value = GetCellValue(cell);

                if (string.IsNullOrEmpty(value))
                {
                    string type = heads[i].MainType;
                    if (DataTypeHelper.IsBaseType(type) && type != TypeDef.stringType)
                        value = 0.ToString();
                    else if (type == TypeDef.stringType)
                        value = string.Empty;
                    else
                    {
                        Log.Error($"{ExcelName}_{Name}: 第{row.RowNum}行，第{cellNum}列单元格数值不能为空（0 based）！");
                    }
                }
                ls.Add(value);
            }
            return ls;
        }

        private string GetCellValue(ICell cell)
        {
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
                    var val = evaluator.Evaluate(cell);
                    if (val.CellType == CellType.Numeric)
                        return val.NumberValue.ToString();
                    else if (val.CellType == CellType.String)
                        return val.StringValue;
                    else
                    {
                        Log.Error($"未支持的公式类型 : {val.CellType}");
                        return string.Empty;
                    }
                }
                default:
                    Log.Error($"未支持的单元格类型 : {cell.CellType}");
                    return string.Empty;
            }
        }
    }
}
