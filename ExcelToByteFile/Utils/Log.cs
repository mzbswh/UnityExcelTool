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
            System.Windows.Forms.MessageBox.Show(msg, caption);
        }

        public static void Error(string msg)
        {
            System.Windows.Forms.MessageBox.Show(msg + Environment.NewLine + Environment.NewLine
                + "按确定键退出...", "错误", MessageBoxButtons.OK);
            Environment.Exit(1);
        }
    }
}
