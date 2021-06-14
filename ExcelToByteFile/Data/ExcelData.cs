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
        /// 此excel所有的sheet数据
        /// </summary>
        public readonly List<SheetData> sheetDataList = new List<SheetData>();

        /// <summary>
        /// 表格类
        /// </summary>
        private IWorkbook _workbook = null;

        /// <summary>
		/// 公式计算器, 一个表格对应一个公式计算器
		/// </summary>
		private XSSFFormulaEvaluator _evaluator = null;

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
                    _workbook = new XSSFWorkbook(_stream);
                else if (ExcelPath.IndexOf(".xls") > 0)
                    _workbook = new HSSFWorkbook(_stream);
                else
                {
                    string extension = Path.GetExtension(ExcelPath);
                    Log.LogMessageBox($"未支持的Excel文件类型 : {extension}");
                    return false;
                    //throw new Exception($"未支持的Excel文件类型 : {extension}");
                }

                _evaluator = new XSSFFormulaEvaluator(_workbook);

                int len = GlobalConfig.Ins.onlyOneSheet ? 1 : _workbook.NumberOfSheets;
                for (int i = 0; i < len; i++)
                {
                    ISheet sheet = _workbook.GetSheetAt(i);
                    if (GlobalConfig.Ins.customExportSheetPrefix)
                    {
                        if (!sheet.SheetName.StartsWith(GlobalConfig.Ins.customSheetPrefix))
                            continue;
                    }
                    SheetData sheetData = new SheetData(_workbook, sheet, _evaluator, ExcelName);
                    sheetData.Load();
                    sheetDataList.Add(sheetData);
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
                //throw new Exception($"表格[{ExcelName}]加载错误：{ex}");
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

            if (_workbook != null)
            {
                _workbook.Close();
                _workbook = null;
            }

            sheetDataList.Clear();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public bool Export()
        {
            try
            {
                for (int i = 0; i < sheetDataList.Count; i++)
                {
                    sheetDataList[i].Export(GlobalConfig.Ins.byteFileOutputDir);
                }
            }
            catch (Exception ex)
            {
                //Log.LogError($"表格[{ExcelName}]导出错误：{ex}");
                MessageBox.Show($"表格[{ExcelName}]导出错误：{ex}");
                return false;
            }
            return true;
        }
    }
}
