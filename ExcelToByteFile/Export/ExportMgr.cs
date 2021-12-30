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

            // 加载选择的Excel文件列表并导出字节文件
            CreateOrClearDir(GlobalConfig.Ins.byteFileOutputDir);
            CreateOrClearDir(GlobalConfig.Ins.codeFileOutputDir);

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
            ExportByteFiles.ExportManifest(fileManifests);

            // 导出cs定义文件 *必须
            string defDir = GlobalConfig.Ins.codeFileOutputDir + Path.DirectorySeparatorChar + "Def";
            if (!Directory.Exists(defDir)) Directory.CreateDirectory(defDir);
            ExportCSharpCode.ExportVariableDefCSCode(defDir, fileManifests);
            // 导出数据结构定义文件 *可选
            if (GlobalConfig.Ins.generateStructInfoCode)
            {
                ExportCSharpCode.ExportStructInfoCsCode(defDir, fileManifests);
            }
            string cacheDir = GlobalConfig.Ins.codeFileOutputDir + Path.DirectorySeparatorChar + "ExcelDataCache";
            if (!Directory.Exists(cacheDir)) Directory.CreateDirectory(cacheDir);
            ExportCSharpCode.ExportCacheCsCode(cacheDir, fileManifests);
        }

        public static void CreateOrClearDir(string srcPath)
        {
            if (!Directory.Exists(srcPath))
            {
                Directory.CreateDirectory(srcPath);
            }
            else
            {
                try
                {
                    DirectoryInfo dir = new DirectoryInfo(srcPath);
                    FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                    foreach (FileSystemInfo i in fileinfo)
                    {
                        if (i is DirectoryInfo)            //判断是否文件夹
                        {
                            DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                            subdir.Delete(true);          //删除子目录和文件
                        }
                        else
                        {
                            File.Delete(i.FullName);      //删除指定文件
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
