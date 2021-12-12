//**************************************************
// The MIT License
// Copyright©2021
//**************************************************

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
    /// excel数据类
    /// </summary>
    public class ExcelData
    {
        /// <summary>
        /// excel名称
        /// </summary>
        public string ExcelName { get; }

        /// <summary>
        /// excel路径
        /// </summary>
        public string ExcelPath { get; }

        /// <summary>
        /// 表格类
        /// </summary>
        public IWorkbook Workbook { get; private set; }

        /// <summary>
		/// 公式计算器, 一个表格对应一个公式计算器
		/// </summary>
		public XSSFFormulaEvaluator Evaluator { get; private set; }

        /// <summary>
        /// 此excel所有的sheet数据
        /// </summary>
        public readonly List<SheetData> sheetDataList = new List<SheetData>();

        /// <summary>
        /// 文件流
        /// </summary>
        private FileStream _stream = null;

        public ExcelData(string excelPath)
        {
            ExcelPath = excelPath;
            ExcelName = Path.GetFileNameWithoutExtension(ExcelPath);
        }

        /// <summary>
        /// 加载Excel
        /// </summary>
        /// <returns></returns>
        public bool Load()
        {
            try
            {
                _stream = new FileStream(ExcelPath, FileMode.Open, FileAccess.Read);

                if (ExcelPath.IndexOf(".xlsx") > 0)
                    Workbook = new XSSFWorkbook(_stream);
                else if (ExcelPath.IndexOf(".xls") > 0)
                    Workbook = new HSSFWorkbook(_stream);
                else
                {
                    string extension = Path.GetExtension(ExcelPath);
                    Log.LogMessageBox($"未支持的Excel文件类型 : {extension}");
                    return false;
                    //throw new Exception($"未支持的Excel文件类型 : {extension}");
                }

                Evaluator = new XSSFFormulaEvaluator(Workbook);

                int len = Workbook.NumberOfSheets;
                for (int i = 0; i < len; i++)
                {
                    ISheet sheet = Workbook.GetSheetAt(i);
                    SheetData sheetData = new SheetData(sheet, this);
                    sheetData.Load();
                    if (sheetData.CanExport)
                    {
                        sheetDataList.Add(sheetData);
                    }
                }

                // 如果没有找到有效的工作页
                if (sheetDataList.Count == 0)
                {
                    Log.LogError($"没有发现 {ExcelName} 的页签");
                }
            }
            catch (Exception ex)
            {
                Log.LogMessageBox($"表格[{ExcelName}]加载错误：{ex}");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (_stream != null)
            {
                _stream.Close();
                _stream = null;
            }

            if (Workbook != null)
            {
                Workbook.Close();
                Workbook = null;
            }

            sheetDataList.Clear();
            GC.SuppressFinalize(this);
        }
    }
}
