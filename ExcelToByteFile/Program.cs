using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelToByteFile
{
    static class Program
    {
        public static MainForm mainForm = new MainForm();
        public static bool IsCommandLine = false;

        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole(); 

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            GlobalConfig.Ins.ReadConfig();
            if (args.Length > 0)
            {
                IsCommandLine = true;
                AllocConsole();

                string excelPath = string.Empty;
                for (int i = 0; i < args.Length; i++)
                {
                    string cmd = args[i];
                    switch (cmd)
                    {
                        case "-e":
                            excelPath = args[i + 1];
                            break;
                        case "-o":
                            GlobalConfig.Ins.byteFileOutputDir = args[i + 1];
                            break;
                        case "-c":
                            GlobalConfig.Ins.codeFileOutputDir = args[i + 1];
                            break;
                    }
                }
                if (string.IsNullOrEmpty(excelPath))
                {
                    Console.WriteLine("错误：excel目录不能为空！");
                    Console.ReadLine();
                    FreeConsole();
                    return;
                }
                else
                {
                    List<string> files = new List<string>();
                    // 将文件路径添加到列表
                    DirectoryInfo dInfo = new DirectoryInfo(excelPath);
                    foreach (var file in dInfo.GetFiles())
                    {
                        if (file.Extension == ".xlsx" || file.Extension == ".xls")
                        {
                            files.Add(file.FullName);
                        }
                    }
                    if (files.Count > 0)
                    {
                        // 更新最近一次打开的目录
                        GlobalConfig.Ins.lastSelectExcelPath = excelPath;
                        ExportMgr.Export(files);
                    }
                    Console.ReadLine();
                    FreeConsole();
                    return;
                }
            }
            else
            {
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(mainForm);
            }
        }
    }
}
