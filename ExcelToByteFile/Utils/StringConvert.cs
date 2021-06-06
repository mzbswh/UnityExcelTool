using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ExcelToByteFile
{
	public static class StringConvert
	{
		/// <summary>
		/// 字符串转换为BOOL
		/// </summary>
		public static bool StringToBool(string str)
		{
			int value = (int)Convert.ChangeType(str, typeof(int));
			return value > 0;
		}

		/// <summary>
		/// 字符串转换为数值
		/// </summary>
		public static T StringToValue<T>(string str)
		{
			return (T)Convert.ChangeType(str, typeof(T));
		}

		/// <summary>
		/// 字符串转换为数值列表
		/// </summary>
		/// <param name="separator">分隔符</param>
		public static List<T> StringToList<T>(string str, char separator)
		{
			List<T> result = new List<T>();
			if (!String.IsNullOrEmpty(str))
			{
				string[] splits = str.Split(separator);
				foreach (string split in splits)
				{
					if (!String.IsNullOrEmpty(split))
					{
						result.Add((T)Convert.ChangeType(split, typeof(T)));
					}
				}
			}
			return result;
		}

		/// <summary>
		/// 字符串转为字符串列表
		/// </summary>
		public static List<string> StringToStringList(string str)
		{
			List<string> result = new List<string>();
			if (!String.IsNullOrEmpty(str))
			{
				string pat = @"(?<!\\),";
				string[] splits = Regex.Split(str, pat);
				foreach (string split in splits)
				{
					string s = split.Trim();
					if (s.StartsWith('\"')) s = s[1..^1];
					s = s.Replace("\\,", ",");

					if (!String.IsNullOrEmpty(s))
					{
						result.Add(s);
					}
				}
			}
			return result;
		}

		public static Dictionary<T1, T2> StringToDict<T1, T2>(string str)
		{
			Dictionary<T1, T2> dict = new Dictionary<T1, T2>();

			string pat = @"(?<=\}\s*),";
			string[] elems = Regex.Split(str, pat);
			string pat2 = @"(?<!\\),";
			foreach (var elem in elems)
			{
				string s = elem.Trim();
				s = s[1..^1];   // 去两边大括号
				string[] keyValPair = Regex.Split(s, pat2);   // 分隔key value
				string key = keyValPair[0].Trim();
				string val = keyValPair[1].Trim();

				if (key.StartsWith('\"')) key = key[1..^1];
				if (val.StartsWith('\"')) val = val[1..^1];
				key = key.Replace("\\,", ",");
				val = val.Replace("\\,", ",");
				dict.Add((T1)Convert.ChangeType(key, typeof(T1)), (T2)Convert.ChangeType(val, typeof(T2)));
			}
			return dict;
		}

		public static Vector2Int StringToVec2Int(string str)
        {
			Vector2Int vec;
			string[] s = str.Split(ConstDefine.splitChar);
			int x = int.Parse(s[0].Trim());
			int y = int.Parse(s[1].Trim());
			vec = new Vector2Int(x, y);
			return vec;
		}
		public static Vector2 StringToVec2(string str)
		{
			Vector2 vec;
			string[] s = str.Split(ConstDefine.splitChar);
			float x = float.Parse(s[0].Trim());
			float y = float.Parse(s[1].Trim());
			vec = new Vector2(x, y);
			return vec;
		}
		public static Vector3Int StringToVec3Int(string str)
		{
			Vector3Int vec;
			string[] s = str.Split(ConstDefine.splitChar);
			int x = int.Parse(s[0].Trim());
			int y = int.Parse(s[1].Trim());
			int z = int.Parse(s[2].Trim());
			vec = new Vector3Int(x, y, z);
			return vec;
		}
		public static Vector3 StringToVec3(string str)
		{
			Vector3 vec;
			string[] s = str.Split(ConstDefine.splitChar);
			float x = float.Parse(s[0].Trim());
			float y = float.Parse(s[1].Trim());
			float z = float.Parse(s[2].Trim());
			vec = new Vector3(x, y, z);
			return vec;
		}
		public static Vector4Int StringToVec4Int(string str)
		{
			Vector4Int vec;
			string[] s = str.Split(ConstDefine.splitChar);
			int x = int.Parse(s[0].Trim());
			int y = int.Parse(s[1].Trim());
			int z = int.Parse(s[2].Trim());
			int w = int.Parse(s[3].Trim());
			vec = new Vector4Int(x, y, z, w);
			return vec;
		}
		public static Vector4 StringToVec4(string str)
		{
			Vector4 vec;
			string[] s = str.Split(ConstDefine.splitChar);
			float x = float.Parse(s[0].Trim());
			float y = float.Parse(s[1].Trim());
			float z = float.Parse(s[2].Trim());
			float w = float.Parse(s[3].Trim());
			vec = new Vector4(x, y, z, w);
			return vec;
		}
		
	}
}
