using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExcelToByteFile
{
	public static class StringHelper
	{
		/// <summary>
		/// 获取存储文件完整的路径
		/// </summary>
		public static string MakeSaveFullPath(string path, string fileName)
		{
			return path + Path.DirectorySeparatorChar + fileName;
		}

		/// <summary>
		/// 首字母大写
		/// </summary>
		public static string ToUpperFirstChar(string content)
		{
			return char.ToUpper(content[0]) + content.Substring(1);
		}

		/// <summary>
		/// 首字母小写
		/// </summary>
		public static string ToLowerFirstChar(string content)
		{
			return char.ToLower(content[0]) + content.Substring(1);
		}

		/// <summary>
		/// 获取扩展类型
		/// </summary>
		public static string GetExtendType(string content)
		{
			int indexOfA = content.IndexOf('[');
			int indexOfB = content.IndexOf(']');
			return content.Substring(indexOfA + 1, indexOfB - indexOfA - 1);
		}

		/// <summary>
		/// 获取命名空间名字
		/// </summary>
		public static string GetNamespace(string content)
		{
			// 注意：因为不能规范输入内容，这里做了容错
			try
			{
				int indexOfA = content.IndexOf('[');
				int indexOfB = content.IndexOf(']');
				return content.Substring(indexOfA + 1, indexOfB - indexOfA - 1);
			}
			catch (Exception)
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// 获取导出类名字
		/// </summary>
		public static string GetExportName(string content)
		{
			// 注意：因为不能规范输入内容，这里做了容错
			try
			{
				int indexOf = content.IndexOf('[');
				return content.Substring(0, indexOf);
			}
			catch (Exception)
			{
				return string.Empty;
			}
		}

		public static string GetExportFileName(ExcelData excel, SheetData sheetData)
        {
			if (excel.sheetDataList.Count == 1)
				return excel.ExcelName;
			else return excel.ExcelName + "_" + sheetData.SheetName;
		}
	}
}
