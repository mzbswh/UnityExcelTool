using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelToByteFile
{
    static class Program
    {
        public static MainForm mainForm = new MainForm();

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
            #region 异常
            //设置应用程序处理异常方式：ThreadException处理
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //处理非UI线程异常
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            #endregion

            GlobalConfig.Ins.ReadConfig();
            AllocConsole();
            GlobalConfig.Ins.codeFileOutputDir = @"..\pasture\Assets\hotfixScripts\AutoDict";
            GlobalConfig.Ins.byteFileOutputDir = @"..\pasture\Assets\Bundles\data";
            string excelPath = @".\police\data\data\";
            for (int i = 0; i < args.Length; i++)
            {
                string cmd = args[i];
                switch (cmd)
                {
                    case "-e":
                        excelPath = args[i + 1];
                        break;
                    case "-b":
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
            }
            //else
            //{
            //    Application.SetHighDpiMode(HighDpiMode.SystemAware);
            //    Application.EnableVisualStyles();
            //    Application.SetCompatibleTextRenderingDefault(false);
            //    #region 异常
            //    //设置应用程序处理异常方式：ThreadException处理
            //    Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //    //处理UI线程异常
            //    Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //    //处理非UI线程异常
            //    AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            //    #endregion
            //    Application.Run(mainForm);
            //}
            
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());
            // 后续处理，保存或输出
            File.AppendAllText(Application.StartupPath + "\\excelParseError.log", str + "\r\n\r\n");
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.Exception, e.ToString());
            // 后续处理，保存或输出
            File.AppendAllText(Application.StartupPath + "\\excelParseError.log", str + "\r\n\r\n");
        }

        static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************异常文本****************************");
            sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
            if (ex != null)
            {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
                sb.AppendLine("【异常方法】：" + ex.TargetSite);
            }
            else
            {
                sb.AppendLine("【未处理异常】：" + backStr);
            }
            sb.AppendLine("***************************************************************");
            return sb.ToString();
        }
    }
}
