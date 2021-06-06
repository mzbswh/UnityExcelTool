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
		/// 是否自动补全
		/// </summary>
		public bool autoCompletion = false;

		/// <summary>
		/// 自动补全的值
		/// </summary>
		public string autoCompletionVal = "0";

		/// <summary>
		/// 类型为空时认为是注释列
		/// </summary>
		public bool typeNullIsNoteCol = true;

		/// <summary>
		/// 开始读取数据时跳过的行，即从第3（头部固定行）+ 此值 行开始读取
		/// </summary>
		public int skipRowBeginRead = 0;

		/// <summary>
		/// true: 变量注释在第一行，false：注释是第三行
		/// </summary>
		public bool commentInFirstRow = false;

		/// <summary>
		/// 只读取一个sheet
		/// </summary>
		public bool onlyOneSheet = true;

		/// <summary>
		/// 存储在本地的配置文件名称
		/// </summary>
		private const string configFileStoreName = "config.data";

		/// <summary>
		/// id列名称
		/// </summary>
		public string idColName = "id";

		/// <summary>
		/// 读取配置文件
		/// </summary>
		public void ReadConfig()
        {
			string appPath = Application.StartupPath;
			string configPath = Path.Combine(appPath, configFileStoreName);

			// 如果配置文件不存在
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
				autoCompletion = str == "True";
				str = sr.ReadLine();
				autoCompletionVal = str;
				str = sr.ReadLine();
				typeNullIsNoteCol = str == "True";
				str = sr.ReadLine();
				skipRowBeginRead = int.Parse(str);
				str = sr.ReadLine();
				commentInFirstRow = str == "True";
				str = sr.ReadLine();
				onlyOneSheet = str == "True";
				str = sr.ReadLine();
				idColName = str;

				sr.Dispose();
				sr.Close();
			}
			catch (Exception e)
			{
				throw e;
			}
			finally
			{
				fs.Dispose();
				fs.Close();
			}
		}

		/// <summary>
		/// 存储配置文件
		/// </summary>
		public void SaveConfig()
		{
			string appPath = Application.StartupPath;
			string configPath = Path.Combine(appPath, configFileStoreName);

			// 删除旧文件
			if (File.Exists(configPath))
				File.Delete(configPath);

			FileStream fs = new FileStream(configPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
			try
			{
				StreamWriter sw = new StreamWriter(fs);
				sw.Flush();

				sw.WriteLine(lastSelectExcelPath);
				sw.WriteLine(byteFileOutputDir);
				sw.WriteLine(codeFileOutputDir);
				sw.WriteLine(autoCompletion.ToString());    // True False
				sw.WriteLine(autoCompletionVal);
				sw.WriteLine(typeNullIsNoteCol.ToString());
				sw.WriteLine(skipRowBeginRead.ToString());
				sw.WriteLine(commentInFirstRow.ToString());
				sw.WriteLine(onlyOneSheet.ToString());
				sw.WriteLine(idColName);

				sw.Flush();
				sw.Dispose();
				sw.Close();
			}
			catch (Exception e)
			{
				throw e;
			}
			finally
			{
				fs.Dispose();
				fs.Close();
			}
		}
	}
}
