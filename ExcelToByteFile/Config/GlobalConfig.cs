using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ExcelToByteFile
{
    public class GlobalConfig
    {
        public static GlobalConfig Ins = new GlobalConfig();

        private GlobalConfig() { }

		/// <summary>
		/// 上次选择Excel时的路径路径
		/// </summary>
		public string lastSelectExcelPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

		/// <summary>
		/// 字节文件输出路径
		/// </summary>
		public string byteFileOutputDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

		/// <summary>
		/// 代码文件输出路径
		/// </summary>
		public string codeFileOutputDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

        /// <summary>
        /// 是否生成结构体信息文件
        /// </summary>
		public bool generateStructInfoCode = true;

		/// <summary>
		/// 存储在本地的配置文件名称  
		/// </summary>
		private const string localConfigName = "config.data";

		public void ReadConfig()
        {
			string appPath = Application.StartupPath;
			string configPath = Path.Combine(appPath, localConfigName);

			if (!File.Exists(configPath))
				return;

			FileStream fs = new FileStream(configPath, FileMode.Open, FileAccess.Read);
			try
			{
				StreamReader sr = new StreamReader(fs);

				string str = sr.ReadLine();
				if (Directory.Exists(str)) lastSelectExcelPath = str;
				str = sr.ReadLine();
				if (Directory.Exists(str)) byteFileOutputDir = str;
				str = sr.ReadLine();
				if (Directory.Exists(str)) codeFileOutputDir = str;
				str = sr.ReadLine();
				generateStructInfoCode = str == SymbolDef.trueWord;

				sr.Dispose();
				sr.Close();
			}
			catch (Exception)
            {
                throw;
            }
			finally
			{
				fs.Dispose();
				fs.Close();
			}
		}

		public void SaveConfig()
		{
			string configPath = Path.Combine(Application.StartupPath, localConfigName);

			if (File.Exists(configPath)) File.Delete(configPath);

			FileStream fs = new FileStream(configPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
			try
			{
				StreamWriter sw = new StreamWriter(fs);
				sw.Flush();

				sw.WriteLine(lastSelectExcelPath);
				sw.WriteLine(byteFileOutputDir);
				sw.WriteLine(codeFileOutputDir);
				sw.WriteLine(generateStructInfoCode ? SymbolDef.trueWord : SymbolDef.falseWord);

				sw.Flush();
				sw.Dispose();
				sw.Close();
			}
			catch (Exception)
			{
                throw;
            }
			finally
			{
				fs.Dispose();
				fs.Close();
			}
		}
	}
}
