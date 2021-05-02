using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelToByteFile
{
    public class ExcelTools
    {
        
        /// <summary>
        /// 获取一个sheet里的一行数据（仅有效数据，不包含注释列）
        /// </summary>
        public static List<string> GetOneRowData(SheetData sheet, IRow row)
        {
            List<string> ls = new List<string>();
            for (int i = 0; i < sheet.heads.Count; i++)
            {
                int cellNum = sheet.heads[i].CellNum;

                // 获取单元格字符串
                ICell cell = row.GetCell(cellNum);
                string value = GetCellValue(cell, sheet.Evaluator);

                // 检测数值单元格是否为空值
                // 如果开启了自动补全功能且是可以自动补全的类型，就设置其值		
                if (string.IsNullOrEmpty(value))
                {
                    if (MainConfig.Ins.autoCompletion)
                    {
                        string type = sheet.heads[i].Type;
                        switch (type)
                        {
                            case TypeDefine.boolType:
                            case TypeDefine.byteType:
                            case TypeDefine.shortType:
                            case TypeDefine.intType:
                            case TypeDefine.floatType:
                            case TypeDefine.stringType:
                            case TypeDefine.longType:
                            case TypeDefine.doubleType:
                                value = MainConfig.Ins.autoCompletionVal;
                                break;
                        }
                        throw new Exception($"此单元格数值不能为空，第{cellNum}列");
                    }
                }

                ls.Add(value);
            }
            return ls;
        }

        /// <summary>
        /// 获取单元格的值
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static string GetCellValue(ICell cell, XSSFFormulaEvaluator evaluator)
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
                        var val = evaluator.Evaluate(cell);
                        if (val.CellType == CellType.Numeric) 
                            return val.NumberValue.ToString();
                        else if (val.CellType == CellType.String)
                            return val.StringValue;
                        else throw new Exception($"未支持的公式类型 : {val.CellType}");
                    }
                default: throw new Exception($"未支持的单元格类型 : {cell.CellType}");
            }
        }
    }
}
