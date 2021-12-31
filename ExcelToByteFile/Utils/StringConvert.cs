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
		public static bool ToBool(string str)
		{
            try
            {
                int value = (int)Convert.ChangeType(str, typeof(int));
                return value > 0;
            }
			catch
            {
                Log.Error($"类型转bool失败:{str}");
                return false;
            }
		}

		/// <summary>
		/// 字符串转换为数值
		/// </summary>
		public static T ToValue<T>(string str)
		{
            try
            {
                return (T)Convert.ChangeType(str, typeof(T));
            }
			catch
            {
                Log.Error($"类型转{typeof(T)}失败:{str}");
                return default(T);
            }
		}

		/// <summary>
		/// 字符串转换为数值列表
		/// </summary>
		/// <param name="separator">分隔符</param>
		public static List<T> ToList<T>(string str, char separator)
		{
            try
            {
                List<T> result = new List<T>();
                if (!string.IsNullOrEmpty(str))
                {
                    string[] splits = str.Split(separator);
                    foreach (string split in splits)
                    {
                        if (!String.IsNullOrEmpty(split))
                        {
                            result.Add((T)Convert.ChangeType(split.Trim(), typeof(T)));
                        }
                    }
                }
                return result;
            }
			catch
            {
                Log.Error($"类型转{typeof(List<T>)}失败:{str}");
                return default(List<T>);
            }
		}

		/// <summary>
		/// 字符串转为字符串列表
		/// </summary>
		public static List<string> ToStringList(string str)
		{
            try
            {
                List<string> result = new List<string>();
                if (!string.IsNullOrEmpty(str))
                {
                    string pat = @"(?<!\\),";
                    string[] splits = Regex.Split(str, pat);
                    foreach (string split in splits)
                    {
                        string s = split.Trim();
                        if (s.StartsWith('\"') && s.EndsWith('\"')) s = s[1..^1];
                        s = s.Replace("\\,", ",");

                        if (!String.IsNullOrEmpty(s))
                        {
                            result.Add(s);
                        }
                    }
                }
                return result;
            }
            catch
            {
                Log.Error($"类型转List<string>失败:{str}");
                return default(List<string>);
            }
        }

		public static Dictionary<T1, T2> ToDict<T1, T2>(string str)
		{
            try
            {
                Dictionary<T1, T2> dict = new Dictionary<T1, T2>();

                //string pat = @"(?<=\}\s*),";
                string pat = @"({.*?(?<!\\),.*?})";
                //string[] elems = Regex.Split(str, pat);
                var elems = Regex.Matches(str, pat);
                string pat2 = @"(?<!\\),";
                foreach (var elem in elems)
                {
                    string s = elem.ToString().Trim();
                    s = s[1..^1];   // 去两边大括号
                    string[] keyValPair = Regex.Split(s, pat2);   // 分隔key value
                    string key = keyValPair[0].Trim();
                    string val = keyValPair[1].Trim();

                    if (key.StartsWith('\"') && key.EndsWith('\"')) key = key[1..^1];
                    if (val.StartsWith('\"') && val.StartsWith('\"')) val = val[1..^1];
                    key = key.Replace("\\,", ",");
                    val = val.Replace("\\,", ",");
                    dict.Add((T1)Convert.ChangeType(key, typeof(T1)), (T2)Convert.ChangeType(val, typeof(T2)));
                }
                return dict;
            }
            catch
            {
                Log.Error($"类型转{typeof(Dictionary<T1, T2>)}失败:{str}");
                return default(Dictionary<T1, T2>);
            }
        }

		public static Vector2Int ToVec2Int(string str)
        {
            try
            {
                Vector2Int vec;
                string[] s = str.Split(SymbolDef.splitChar);
                int x = int.Parse(s[0].Trim());
                int y = int.Parse(s[1].Trim());
                vec = new Vector2Int(x, y);
                return vec;
            }
            catch
            {
                Log.Error($"类型转Vector2Int失败:{str}");
                return default(Vector2Int);
            }
        }
		public static Vector2 ToVec2(string str)
		{
            try
            {
                Vector2 vec;
                string[] s = str.Split(SymbolDef.splitChar);
                float x = float.Parse(s[0].Trim());
                float y = float.Parse(s[1].Trim());
                vec = new Vector2(x, y);
                return vec;
            }
            catch
            {
                Log.Error($"类型转Vector2失败:{str}");
                return default(Vector2);
            }
        }
		public static Vector3Int ToVec3Int(string str)
		{
            try
            {
                Vector3Int vec;
                string[] s = str.Split(SymbolDef.splitChar);
                int x = int.Parse(s[0].Trim());
                int y = int.Parse(s[1].Trim());
                int z = int.Parse(s[2].Trim());
                vec = new Vector3Int(x, y, z);
                return vec;
            }
			catch
            {
                Log.Error($"类型转Vector3Int失败:{str}");
                return default(Vector3Int);
            }
		}
		public static Vector3 ToVec3(string str)
		{
            try
            {
                Vector3 vec;
                string[] s = str.Split(SymbolDef.splitChar);
                float x = float.Parse(s[0].Trim());
                float y = float.Parse(s[1].Trim());
                float z = float.Parse(s[2].Trim());
                vec = new Vector3(x, y, z);
                return vec;
            }
            catch
            {
                Log.Error($"类型转Vector3失败:{str}");
                return default(Vector3);
            }
        }
		public static Vector4Int ToVec4Int(string str)
		{
            try
            {
                Vector4Int vec;
                string[] s = str.Split(SymbolDef.splitChar);
                int x = int.Parse(s[0].Trim());
                int y = int.Parse(s[1].Trim());
                int z = int.Parse(s[2].Trim());
                int w = int.Parse(s[3].Trim());
                vec = new Vector4Int(x, y, z, w);
                return vec;
            }
            catch
            {
                Log.Error($"类型转Vector4Int失败:{str}");
                return default(Vector4Int);
            }
        }
		public static Vector4 ToVec4(string str)
		{
            try
            {
                Vector4 vec;
                string[] s = str.Split(SymbolDef.splitChar);
                float x = float.Parse(s[0].Trim());
                float y = float.Parse(s[1].Trim());
                float z = float.Parse(s[2].Trim());
                float w = float.Parse(s[3].Trim());
                vec = new Vector4(x, y, z, w);
                return vec;
            }
			catch
            {
                Log.Error($"类型转Vector4失败:{str}");
                return default(Vector4);
            }
		}
		
	}
}
