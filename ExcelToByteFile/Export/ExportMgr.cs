using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using NPOI.SS.UserModel;

namespace ExcelToByteFile
{

	public class ExportMgr
    {
        public static readonly ByteWriterBuffer fileBuffer = new ByteWriterBuffer(ConstDef.fileStreamMaxLen);
        public static readonly List<ManifestData> fileManifests = new List<ManifestData>();

        public static void Export(List<string> fileList)
        {
            // 每次导出都存储一次配置文件
            GlobalConfig.Ins.SaveConfig();
            fileManifests.Clear();
            // 加载选择的Excel文件列表
            for (int i = 0; i < fileList.Count; i++)
            {
                string filePath = fileList[i];
                ExcelData excelData = new ExcelData(filePath);
                Program.mainForm?.Invoke(new Action(() => Program.mainForm.SetProgress(i + 1, "正在生成：" + excelData.Name)));
                try
                {
                    if (excelData.Load())
                    {
                        try
                        {
                            foreach (var sheetData in excelData.sheetDataList)
                            {
                                string exportName;
                                if (sheetData.SheetConfig.ExportName != string.Empty)
                                {
                                    exportName = sheetData.SheetConfig.ExportName;
                                }
                                else
                                {
                                    exportName = excelData.sheetDataList.Count > 1 ? $"{excelData.Name}_{sheetData.Name}" : excelData.Name;
}
                                ManifestData fileData = new ManifestData(sheetData, exportName);
                                fileManifests.Add(fileData);
                                ExportByteFiles.Export(GlobalConfig.Ins.byteFileOutputDir, fileData, sheetData);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"表格[{excelData.Name}]加载错误：{ex}");
                        }
                    }
                    excelData.Dispose();
                }
                catch
                {
                    Log.Error($"{excelData.Name} 加载错误");
                }
                
            }
            // 导出Manifest信息 *必须
            ExportByteFiles.ExportManifest(fileManifests);
            string defDir = GlobalConfig.Ins.codeFileOutputDir + Path.DirectorySeparatorChar + "Def";
            if (!Directory.Exists(defDir)) Directory.CreateDirectory(defDir);
            // 导出cs定义文件 *必须
            ExportCSharpCode.ExportVariableDefCSCode(defDir, fileManifests);
            // 导出数据结构信息文件 *可选
            if (GlobalConfig.Ins.generateStructInfoCode)
            {
                ExportCSharpCode.ExportStructInfoCsCode(defDir, fileManifests);
            }
            string cacheDir = GlobalConfig.Ins.codeFileOutputDir + Path.DirectorySeparatorChar + "DataCache";
            if (!Directory.Exists(cacheDir)) Directory.CreateDirectory(cacheDir);
            ExportCSharpCode.ExportCacheCsCode(cacheDir, fileManifests);
        }  
	}
}
