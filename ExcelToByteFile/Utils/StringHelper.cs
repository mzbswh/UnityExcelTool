using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExcelToByteFile
{
	public static class StringHelper
	{
		/// <summary>
		/// 拼接完整路径
		/// </summary>
		public static string MakeFullPath(string path, string fileName)
		{
			return path + Path.DirectorySeparatorChar + fileName;
		}
	}
}
