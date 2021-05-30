using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ExcelToByteFile
{
    class Log
    {
        static string prefix = "[" + DateTime.Now.ToString("HH:mm:ss") + "] ";

        /// <summary>
        /// 向窗口下方的文本框发送消息
        /// </summary>
        /// <param name="msg"></param>
        public static void LogMessage(string msg, Color col, bool newLine = true)
        {
            if (Program.IsCommandLine)
                Console.WriteLine(msg);
            else
                Program.mainForm.LogMessage(prefix + msg, col);
        }

        public static void LogNormal(string msg)
        {
            if (Program.IsCommandLine)
                Console.WriteLine(msg);
            else
                Program.mainForm.LogMessage(prefix + msg, Color.Black);
        }

        public static void LogNormal(object msg)
        {
            if (Program.IsCommandLine)
                Console.WriteLine(msg);
            else
                Program.mainForm.LogMessage(prefix + msg.ToString(), Color.Black);
        }

        public static void LogWarning(string msg)
        {
            if (Program.IsCommandLine)
                Console.WriteLine(msg);
            else
                Program.mainForm.LogMessage(prefix + "Warning: " + msg, Color.Orange);
        }

        public static void LogWarning(object msg)
        {
            if (Program.IsCommandLine)
                Console.WriteLine(msg);
            else
                Program.mainForm.LogMessage(prefix + "Warning: " + msg.ToString(), Color.Orange);
        }

        public static void LogError(string msg)
        {
            if (Program.IsCommandLine)
                Console.WriteLine(msg);
            else
                Program.mainForm.LogMessage(prefix + "Error: " + msg, Color.Red);
        }

        public static void LogError(object msg)
        {
            if (Program.IsCommandLine)
                Console.WriteLine(msg);
            else
                Program.mainForm.LogMessage(prefix + "Error: " + msg.ToString(), Color.Red);
        }
    }

    public enum LogType
    {
        Normal,
        Warning,
        Error
    }
}
