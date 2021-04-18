using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ExcelToByteFile
{
    public class ExportMgr
    {

        public static void Export(List<string> fileList)
        {

        }

        public static void ExportFile(string path)
        {

        }

        public static void RunCommand(string[] args)
        {
            MainConfig.Ins.Init();
            MainConfig.Ins.ReadConfig();
            Export(args.ToList());
        }

    }
}
