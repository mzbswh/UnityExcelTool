using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ExcelToByteFile
{
    class Log
    {
        public static void LogConsole(string msg)
        {
            Console.WriteLine(msg);
        }

        public static void Info(string msg, string caption = null)
        {
            LogConsole(msg);
        }

        public static void Error(string msg)
        {
            LogConsole("错误：" + msg);
            Console.WriteLine("按任意键退出");
            Console.ReadKey();
        }
    }
}
