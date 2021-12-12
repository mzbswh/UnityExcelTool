//**************************************************
// The MIT License
// Copyright©2021
//**************************************************

using System;
using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;

namespace ExcelToByteFile
{
    /// <summary>
    /// excel数据类
    /// </summary>
    public class ExcelData
    {
        public string Name { get; }

        public string LoadPath { get; }

        public IWorkbook Workbook { get; private set; }

        /// <summary>
		/// 公式计算器, 一个表格对应一个公式计算器
		/// </summary>
		public XSSFFormulaEvaluator Evaluator { get; private set; }

        /// <summary>
        /// 此excel所有的sheet表（仅可导出）数据
        /// </summary>
        public readonly List<SheetData> sheetDataList = new List<SheetData>();

        private FileStream fileStream = null;

        public ExcelData(string excelPath)
        {
            LoadPath = excelPath;
            Name = System.IO.Path.GetFileNameWithoutExtension(LoadPath);
        }

        public bool Load()
        {
            try
            {
                fileStream = new FileStream(LoadPath, FileMode.Open, FileAccess.Read);

                if (LoadPath.IndexOf(".xlsx") > 0)
                    Workbook = new XSSFWorkbook(fileStream);
                else if (LoadPath.IndexOf(".xls") > 0)
                    Workbook = new HSSFWorkbook(fileStream);
                else
                {
                    string extension = System.IO.Path.GetExtension(LoadPath);
                    Log.Info($"未支持的Excel文件类型 : {extension}");
                    return false;
                }

                Evaluator = new XSSFFormulaEvaluator(Workbook);

                for (int i = 0; i < Workbook.NumberOfSheets; i++)
                {
                    ISheet sheet = Workbook.GetSheetAt(i);
                    SheetData sheetData = new SheetData(sheet, this);
                    sheetData.Load();
                    if (sheetData.ShouldExport)
                    {
                        sheetDataList.Add(sheetData);
                    }
                }

                if (sheetDataList.Count == 0)
                {
                    Log.Info($"没有发现[{Name}]的页签");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Info($"表格[{Name}]加载错误：{ex}");
                return false;
            }
            finally
            {
                fileStream?.Close();
                Workbook?.Close();
            }
            return true;
        }

        public void Dispose()
        {
            sheetDataList.Clear();
        }
    }
}
