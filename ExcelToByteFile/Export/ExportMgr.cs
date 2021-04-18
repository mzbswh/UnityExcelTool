using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace ExcelToByteFile
{
    public class ExportMgr
    {

        public static void Export(List<string> fileList)
        {
            // 加载选择的Excel文件列表
            for (int i = 0; i < fileList.Count; i++)
            {
                string filePath = fileList[i];
                ExcelData excelFile = new ExcelData(filePath);
                if (excelFile.Load())
                {
                    if (excelFile.Export())
                    {
                        //// 导出成功后，我们解析表格的多语言数据
                        //var data = LanguageMgr.ParseLanguage(excelFile);
                        //LanguageMgr.Instance.CacheLanguage(data);
                    }
                }
                excelFile.Dispose();
            }
        }

        public static void ExportFile(string path, SheetData _sheet)
        {
            ByteBuffer fileBuffer = new ByteBuffer(1024*1024*128);
            ByteBuffer tableBuffer = new ByteBuffer(1024*256);

            for (int i = 0; i < _sheet.tables.Count; i++)
            {
                TableData table = _sheet.tables[i];

                // 写入行标记
                fileBuffer.WriteShort(0x2b2b);

                // 清空缓存
                tableBuffer.Clear();

                // 写入数据
                for (int j = 0; j < _sheet.heads.Count; j++)
                {
                    HeadData head = _sheet.heads[j];
                    string value = table.GetCellValue(head.CellNum);
                    WriteCell(tableBuffer, head, value, "");
                }

                // 检测数据大小有效性
                int tabSize = tableBuffer.ReadableBytes;
                if (tabSize == 0)
                    throw new Exception($"{_sheet.SheetName} tableBuffer readable bytes is zero.");

                // 写入到总缓存
                fileBuffer.WriteInt(tabSize);
                fileBuffer.WriteBytes(tableBuffer.ReadBytes(tabSize));
            }

            // 创建文件
            string filePath = StringHelper.MakeSaveFullPath(path, $"{_sheet.SheetName}.bytes");
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                byte[] data = fileBuffer.GetBuffer();
                int length = fileBuffer.ReadableBytes;
                fs.Write(data, 0, length);
            }
        }

		private static void WriteCell(ByteBuffer buffer, HeadData head, string value, string createLogo)
		{
			if (head.IsNotes )//|| head.Logo.Contains(createLogo) == false)
				return;

			if (head.Type == "int")
			{
				buffer.WriteInt(StringConvert.StringToValue<int>(value));
			}
			else if (head.Type == "long")
			{
				buffer.WriteLong(StringConvert.StringToValue<long>(value));
			}
			else if (head.Type == "float")
			{
				buffer.WriteFloat(StringConvert.StringToValue<float>(value));
			}
			else if (head.Type == "double")
			{
				buffer.WriteDouble(StringConvert.StringToValue<double>(value));
			}

			else if (head.Type == "List<int>")
			{
				buffer.WriteListInt(StringConvert.StringToValueList<int>(value, ConstDefine.splitChar));
			}
			else if (head.Type == "List<long>")
			{
				buffer.WriteListLong(StringConvert.StringToValueList<long>(value, ConstDefine.splitChar));
			}
			else if (head.Type == "List<float>")
			{
				buffer.WriteListFloat(StringConvert.StringToValueList<float>(value, ConstDefine.splitChar));
			}
			else if (head.Type == "List<double>")
			{
				buffer.WriteListDouble(StringConvert.StringToValueList<double>(value, ConstDefine.splitChar));
			}

			// bool
			else if (head.Type == "bool")
			{
				buffer.WriteBool(StringConvert.StringToBool(value));
			}

			// string
			else if (head.Type == "string")
			{
				buffer.WriteUTF(value);
			}
			else if (head.Type == "List<string>")
			{
				buffer.WriteListUTF(StringConvert.StringToStringList(value, ConstDefine.splitChar));
			}

			// NOTE：多语言在字节流会是哈希值
			else if (head.Type == "language")
			{
				buffer.WriteInt(value.GetHashCode());
			}
			else if (head.Type == "List<language>")
			{
				List<string> langList = StringConvert.StringToStringList(value, ConstDefine.splitChar);
				List<int> hashList = new List<int>();
				for (int i = 0; i < langList.Count; i++)
				{
					hashList.Add(langList[i].GetHashCode());
				}
				buffer.WriteListInt(hashList);
			}

			// wrapper
			else if (head.Type.Contains("class"))
			{
				buffer.WriteUTF(value);
			}

			// enum
			else if (head.Type.Contains("enum"))
			{
				buffer.WriteInt(StringConvert.StringToValue<int>(value));
			}

			else
			{
				throw new Exception($"Not support head type {head.Type}");
			}
		}

		public static void RunCommand(string[] args)
        {
            MainConfig.Ins.Init();
            MainConfig.Ins.ReadConfig();
            Export(args.ToList());
        }

    }
}
