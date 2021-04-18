﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ExcelToByteFile
{
    public class MainConfig
    {
        public static MainConfig Ins = new MainConfig();

        private MainConfig() { }

        /// <summary>
        /// 上次选择Excel时的路径路径
        /// </summary>
        public string lastSelectExcelPath;

        /// <summary>
        /// 字节文件输出路径
        /// </summary>
        public string byteFileOutputDir;

        /// <summary>
        /// 日志文件输出路径
        /// </summary>
        public string logOutputDir;

		/// <summary>
		/// 存储在本地的配置文件名称
		/// </summary>
		private const string configFileStoreName = "ExcelExport.data";


		public void Init()
        {
            string appPath = Application.StartupPath;
            lastSelectExcelPath = appPath;
        }

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

				string excelPath = sr.ReadLine();
				if (Directory.Exists(excelPath))
					lastSelectExcelPath = excelPath;

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
