using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Diagnostics;

namespace ExcelParseTool 
{
    //excel结构 行数 0：变量提示 1：变量类型 2：变量值 3：注释行（无用处）
    /*储存结构：01 111011 
     * 第一个字节存储文件信息，00 000001 后两位： 00无偏移 01一级偏移 10 二级偏移（有二级偏移必有一级偏移）
     * 第二个字节存储 存储行长所占的字节数 的数目 第一位为1表示剩余7字节为行长 否则表示行长度需要的字节数，即第三个字节到3+行长字节 行长度
     * 下一个字节 存储 列长所占的字节数 的数目 同上一步
     * 列长/2个字节 向上取整 存储每列的类型 0011 0001 半个字节存储 全为0代表没有数据（只最后一个字节的最后半个字节可能为全0） 1为int 2为float 3为string
     * 下一个字节为数据或者偏移信息
     * 无偏移，必须读取转为类数据
     * 一级偏移：开头存每行数据起始地址 长度为行数个字节，
     * 二级偏移：每行数据存储其内元素的偏移 长度为列长个字节
     * 数据：int类型 第二位为符号位，第一位为1时代表剩余的6位存储的为具体数值，不为1时后六位存储数据所占的字节数
     * float类型 如果第一位为0，则下4个字节存数据，如果为1，表示按整数处理，第3位存符号 看第2位，为1表示最后5为是具体数值，否则后5位存数据所占字节（一定是小于等于4）
     * string类型 第一位为1 第二位符号位 表示可以转为正数字且能只用一字节表示 后6位为值 最大63 第一位不为1时，第二位为1表示后六位存储的是字符串所占字节长度 最大63
     * 不为1表示数据长度大于63，因此存的是接下来长度信息所占的是几个字节，例如：为1则下一个字节是字符串长度，为2下两个字节的值是要读取的长度
     * 低位在前
     */
    partial class ParseTool 
    {
        /// <summary>excel表所处的目录名称和目录父目录名称</summary>
        public const string excelDir = "/data";
        /// <summary>data目录下output目录</summary>
        public const string outputDir = "/excelParseOutput";
        /// <summary>excelParseOutput目录下byteFiles目录</summary>
        public const string byteFilesDir = "/byteFiles";
        /// <summary>excelParseOutput目录下c#代码目录</summary>
        public const string csCodeDir = "/csCode";
        /// <summary>日志文件路径</summary>
        public const string logDir = "\\excelParseLog.txt";

        /// <summary>取最后8位</summary>
        public const short filter8 = 0xff;
        /// <summary>取最后7位</summary>
        public const short filter7 = 0x7f;
        /// <summary>取最后6位</summary>
        public const short filter6 = 0x3f;
        /// <summary>取最后5位</summary>
        public const short filter5 = 0x1f;
        /// <summary>取最后4位</summary>
        public const short filter4 = 0xf;

        public const string IntString = "int";
        public const string FloatString = "float";
        public const string StringString = "string";
        public const string XlsType = ".xls";
        public const string XlsxType = ".xlsx";

        /// <summary>按键信息</summary>
        public static ConsoleKeyInfo key;
        /// <summary>excel目录的父目录的全路径</summary>
        public static string parentDir;
        /// <summary>生成的字节文件偏移等级，0不生成偏移数据，1生成每行数据起始偏移，2生成每个数据相对行首偏移</summary>
        public static int byteFileOffLevel;
        /// <summary>生成c#代码的类型，0：将数据全部读取转为c#的数据，1：只生成每行行首的索引，2：生成每个数据的索引</summary>
        public static GenIndexType genType = GenIndexType.EveryData;
        /// <summary>log日志文件</summary>
        public static string logStr = "";

        static void Main(string[] args)
        {
            //InputByteFileGenMode();
            //InputCsCodeGenMode();
            byteFileOffLevel = int.Parse(args[0]);
            int csType = int.Parse(args[1]);
            genType = (GenIndexType)csType;
            if (byteFileOffLevel < csType)
            {
                throw new Exception("第一个参数值小于第二个参数值，这将导致解析错误！");
            }

            List<FileInfo> files = FindAllExcelFiles();
            CheckOutPutDir(parentDir);
            allClassName = new List<string>();

            logStr += "字节文件偏移级别：" + byteFileOffLevel + "   ---   0不生成偏移数据，1生成每行数据起始偏移，2生成每个数据相对行首偏移\n";
            logStr += "生成代码类型：" + (int)genType + "   ---   0：将数据全部读取转为c#的数据，1：只生成每行行首的索引，2：生成每个数据的索引\n";
            logStr += "\n----------------------\n";
            Console.WriteLine("\n----------------------");
            
            // 循环读取所有的excel文件
            for (int i = 0; i < files.Count; i++)
            {
                FileInfo file = files[i];
                Console.WriteLine("开始解析Excel文件: " + file.Name);
                logStr += "开始解析Excel文件: " + file.Name + "\n";
                using (FileStream fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
                {
                    IWorkbook wb = null;
                    if (file.Extension == XlsType)
                        wb = new HSSFWorkbook(fs);
                    else
                        wb = new XSSFWorkbook(fs);
                    GenSingleByteDataFile(file, wb);
                    GenSingleCSCodeFile(file, wb);
                }
                Console.WriteLine("----------------------");
                logStr += "----------------------\n";
            }

            // 生成info.bytes文件  
            // 结构：所有文件个数（不包括此文件）4 字节 
            //      int类型数量 4 字节
            //      float类型数量 4 字节
            //      string类型数量 4 字节
            //      所有文件的名称
            //string path = parentDir + outputDir + byteFilesDir + "/" + "info.bytes";
            //using (FileStream fs1 = new FileStream(path, FileMode.Create, FileAccess.Write))
            //{
            //    List<byte> infoData = new List<byte>();
            //    infoData.AddRange(GenIntData(totalFileNum, true));
            //    infoData.AddRange(GenIntData(intDataNum, true));
            //    infoData.AddRange(GenIntData(floatDataNum, true));
            //    infoData.AddRange(GenIntData(stringDataNum, true));
            //    infoData.AddRange(allFileNames);
            //    Console.WriteLine("开始生成info.bytes文件，" + "长度 = " + infoData.Count + "字节  " + (infoData.Count / 1024) + "KB");
            //    logStr += "开始生成info.bytes文件，" + "长度 = " + infoData.Count + "字节  " + (infoData.Count / 1024) + "KB\n";
            //    byte[] write = infoData.ToArray();
            //    fs1.Write(write, 0, write.Length);
            //}

            GenDictManager();

            Console.WriteLine("----------------------");

            //生成日志文件
            logStr += "\n*******所有文件生成完成*******\n";
            using (FileStream fs1 = new FileStream(parentDir + logDir, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs1))
                {
                    sw.WriteLine(logStr);
                }
            }

            Console.WriteLine("*******所有文件生成完成*******");
            Console.WriteLine("日志文件路径：" + parentDir + logDir);

            //Console.ReadLine();
        }

        /// <summary>
        /// 查找所有Excel文件
        /// </summary>
        /// <returns>所有excel文件信息</returns>
        public static List<FileInfo> FindAllExcelFiles()
        {
            //查找当前目录是否是data目录,excel文件位于police\data\data目录下
            DirectoryInfo directoryInfo = new DirectoryInfo("./");
            string curDirectoryName = directoryInfo.Name;
            while (curDirectoryName != "data" && directoryInfo.Parent != null)
            {
                directoryInfo = directoryInfo.Parent;
                curDirectoryName = directoryInfo.Name;
            }
            if (curDirectoryName != "data")
            {
                if (System.Environment.CurrentDirectory.EndsWith("Tools"))
                {
                    directoryInfo = new DirectoryInfo(Environment.CurrentDirectory + "/police/data");
                }
                else
                {
                    //Console.WriteLine("错误：未找到excel目录！请确保exe文件位于excel文件目录（data）的父目录（data）之下（可以是在多级子目录下，程序会自动查找当前所在目录及沿目录树向上查找）");
                    throw new Exception("错误：未找到excel目录！请确保exe文件位于excel文件目录（data）的父目录（data）之下（可以是在多级子目录下，程序会自动查找当前所在目录及沿目录树向上查找）");
                }
            }
            string path = directoryInfo.FullName + excelDir;
            if (!Directory.Exists(path))
            {
                //Console.WriteLine("目录不存在：" + path);
                throw new Exception("目录不存在：" + path);
            }
            parentDir = directoryInfo.FullName;
            directoryInfo = new DirectoryInfo(path);
            FileInfo[] files = directoryInfo.GetFiles();
            List<FileInfo> ret = new List<FileInfo>();
            directoryInfo.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                if (!files[i].Name.StartsWith("~$"))
                {
                    ret.Add(files[i]);
                }
            }
            return ret;
        }
        /// <summary>
        /// 检查输出文件目录
        /// </summary>
        /// <param name="dataDir"></param>
        public static void CheckOutPutDir(string dataDir)
        {
            string path = dataDir + outputDir;
            if (Directory.Exists(path))
            {
                if (Directory.Exists(path + byteFilesDir))
                {
                    DirectoryInfo info = new DirectoryInfo(path + byteFilesDir);
                    foreach (FileInfo file in info.GetFiles())
                    {
                        File.Delete(file.FullName);
                    }
                }
                else
                {
                    Directory.CreateDirectory(path + byteFilesDir);
                }
                if (Directory.Exists(path + csCodeDir))
                {
                    DirectoryInfo info = new DirectoryInfo(path + csCodeDir);
                    foreach (FileInfo file in info.GetFiles())
                    {
                        File.Delete(file.FullName);
                    }
                }
                else
                {
                    Directory.CreateDirectory(path + csCodeDir);
                }
            }
            else
            {
                Directory.CreateDirectory(path + byteFilesDir);
                Directory.CreateDirectory(path + csCodeDir);
            }
            if (File.Exists(parentDir + logDir))
            {
                File.Delete(parentDir + logDir);
            }
        }
        /// <summary>
        /// 获取总行数，即有多少行有效数据
        /// </summary>
        /// <returns></returns>
        public static int GetTotalRows(ISheet sheet)
        {
            int ret = 0;
            int index = 4;
            bool flag = true;
            while (flag)
            {
                IRow row = sheet.GetRow(index);
                if (row != null && row.GetCell(0) != null && row.GetCell(0).ToString() != "")
                {
                    ret++;
                    index++;
                }
                else
                {
                    flag = false;
                }
            }
            return ret;
        }
        /// <summary>
        /// 获取数据的列数
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        public static int GetTotalCols(ISheet sheet)
        {
            IRow row = sheet.GetRow(1);
            int ret = 0;
            for (int i = 0; i < row.LastCellNum; i++)
            {
                ICell cell = row.GetCell(i);
                if (cell != null && cell.CellType == CellType.String
                    && (cell.StringCellValue == IntString || cell.StringCellValue == FloatString || cell.StringCellValue == StringString))
                    ret++;
                else
                    break;
            }
            return ret;
        }
        /// <summary>
        /// 获取单元格内的int数据，当自身定义的类型的int时
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <param name="row">行数，用于输出错误位置</param>
        /// <param name="col">列数，用于输出错误位置</param>
        /// <returns>int值</returns>
        public static int GetIntValueInCell(ICell cell, int row, int col)
        {
            if (cell == null)
                return 0;
            int ret;
            switch (cell.CellType)
            {
                case CellType.Blank:
                case CellType.Unknown:
                    ret = 0;
                    break;
                case CellType.Boolean:
                    ret = cell.BooleanCellValue ? 1 : 0;
                    break;
                case CellType.Error:
                case CellType.Formula:
                    ret = (int)cell.NumericCellValue;
                    break;
                //Console.WriteLine(cell.NumericCellValue);
                //throw new Exception("错误的单元格类型：" + (row + 5) + "行 " + (col + 1) + "列！" + "(Excel最左上角为1,1)");
                case CellType.Numeric:
                    if (CheckIsIntType(cell.NumericCellValue))
                        ret = (int)cell.NumericCellValue;
                    else
                        throw new Exception("单元格不是int整数类型：" + row + "行 " + col + "列！" + "(Excel最左上角为1,1)");
                    break;
                case CellType.String:
                    if (CheckIsIntType(cell.StringCellValue))
                        ret = int.Parse(cell.StringCellValue);
                    else
                        throw new Exception("单元格不是int整数类型：" + row + "行 " + col + "列！" + "(Excel最左上角为1,1)");
                    break;
                default:
                    throw new Exception("未做处理的单元格类型：" + cell.CellType.ToString() + row + "行 " + col + "列！" + "(Excel最左上角为1,1)");
            }
            return ret;
        }
        /// <summary>
        /// 获取单元格内的float数据，当自身定义的类型的float时
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <param name="row">行数，用于输出错误位置</param>
        /// <param name="col">列数，用于输出错误位置</param>
        /// <returns>float值</returns>
        public static float GetFloatValueInCell(ICell cell, int row, int col)
        {
            if (cell == null)
                return 0;
            float ret;
            switch (cell.CellType)
            {
                case CellType.Blank:
                case CellType.Unknown:
                    ret = 0;
                    break;
                case CellType.Boolean:
                    ret = cell.BooleanCellValue ? 1 : 0;
                    break;
                case CellType.Error:
                case CellType.Formula:
                    ret = (float)cell.NumericCellValue;
                    break;
                //throw new Exception("错误的单元格类型：" + (row + 5) + "行 " + (col + 1) + "列！" + "(Excel最左上角为1,1)");
                case CellType.Numeric:
                    if (CheckIsFloatType(cell.NumericCellValue))
                        ret = (float)cell.NumericCellValue;
                    else
                        throw new Exception("单元格不是float浮点类型：" + row + "行 " + col + "列！" + "(Excel最左上角为1,1)");
                    break;
                case CellType.String:
                    if (CheckIsFloatType(cell.StringCellValue))
                        ret = float.Parse(cell.StringCellValue);
                    else
                        throw new Exception("单元格不是float浮点类型：" + row + "行 " + col + "列！" + "(Excel最左上角为1,1)");
                    break;
                default:
                    throw new Exception("未做处理的单元格类型：" + cell.CellType.ToString() + row + "行 " + col + "列！" + "(Excel最左上角为1,1)");
            }
            return ret;
        }
        /// <summary>
        /// 获取单元格内的string数据，当自身定义的类型的string时
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <param name="row">行数，用于输出错误位置</param>
        /// <param name="col">列数，用于输出错误位置</param>
        /// <returns>string值</returns>
        public static string GetStringValueInCell(ICell cell, int row, int col)
        {
            if (cell == null)
                return "";
            string ret;
            switch (cell.CellType)
            {
                case CellType.Blank:
                case CellType.Unknown:
                    ret = "";
                    break;
                case CellType.Boolean:
                    ret = cell.BooleanCellValue.ToString();
                    break;
                case CellType.Error:
                case CellType.Formula:
                    ret = cell.StringCellValue;
                    break;
                //Console.WriteLine(cell.StringCellValue);
                //throw new Exception("错误的单元格类型：" + (row + 5) + "行 " + (col + 1) + "列！" + "(Excel最左上角为1,1)");
                case CellType.Numeric:
                    ret = cell.NumericCellValue.ToString();
                    break;
                case CellType.String:
                    ret = cell.StringCellValue;
                    break;
                default:
                    throw new Exception("未做处理的单元格类型：" + cell.CellType.ToString() + row + "行 " + col + "列！" + "(Excel最左上角为1,1)");
            }
            return ret;
        }
        /// <summary>
        /// 获取变量类型
        /// </summary>
        /// <param name="rowData"></param>
        /// <returns></returns>
        public static ElemType[] GetAllElemType(IRow rowData, int cols)
        {
            ElemType[] ret = new ElemType[cols];
            for (int i = 0; i < cols; i++)
            {
                string typeStr = rowData.GetCell(i).StringCellValue.ToLower();
                switch (typeStr)
                {
                    case IntString:
                        ret[i] = ElemType.Int;
                        break;
                    case FloatString:
                        ret[i] = ElemType.Float;
                        break;
                    case StringString:
                        ret[i] = ElemType.String;
                        break;
                    default:
                        Console.WriteLine("错误：未添加的变量类型 = " + typeStr + "  第" + (i + 1) + "列");
                        throw new Exception("错误的类型");
                }
            }
            return ret;
        }

        //测试取数据
        static int index = 1;//从第二个字节开始
        public static void RestoreData()
        {
            FileStream fs;
            using (fs = new FileStream("data.bytes", FileMode.Open, FileAccess.Read))
            {
                byte[] data = new byte[fs.Length];
                fs.Read(data, 0, (int)fs.Length);
                int rowNum = ParseRowColValue(data);
                int colNum = ParseRowColValue(data);
                ElemType[] elemTypes = ParseElemType(data, colNum);
                #region 类变量声明

                #endregion


                Model md = GetModel(data, 0);
                Model md1 = GetModel(data, 259);
                Model md2 = GetModel(data, 586);
                Model md3 = GetModel(data, 5255);
                Model md4 = GetModel(data, 9369);

                Console.WriteLine(md.path + "--" + md.xzPos + "--" + md.scaleY);
                Console.WriteLine(md1.path + "--" + md1.xzPos + "--" + md1.scaleY);
                Console.WriteLine(md2.path + "--" + md2.xzPos + "--" + md2.scaleY);
                Console.WriteLine(md3.path + "--" + md3.xzPos + "--" + md3.scaleY);
                Console.WriteLine(md4.path + "--" + md4.xzPos + "--" + md4.scaleY);

                Console.ReadLine();
                //解析具体数据
                for (int i = 4; i < rowNum; i++)
                {
                    for (int j = 0; j < colNum; j++)
                    {
                        switch (elemTypes[j])
                        {
                            case ElemType.Int:
                                //int[] tempInt = colIndex2IntArray[j];
                                //tempInt[i - 4] = ParseInt(data);
                                //Console.WriteLine("int = " + tempInt[i - 4]);
                                break;
                            case ElemType.Float:
                                //float[] tempFloat = colIndex2FloatArray[j];
                                //tempFloat[i - 4] = ParseFloat(data);
                                //Console.WriteLine("float = " + tempFloat[i - 4]);
                                break;
                            case ElemType.String:
                                //string[] tempStr = colIndex2StringArray[j];
                                //tempStr[i - 4] = ParseString(data);
                                //Console.WriteLine("str = " + tempStr[i - 4]);
                                break;
                        }
                    }
                    //Console.WriteLine("id = " + dataClass.id[i - 4]);
                    //id2ArrayIndex.Add(dataClass.id[i - 4], i - 4);
                }
            }
            //数据测试

        }
        /// <summary>
        /// 解析int类型的数据
        /// </summary>
        /// <returns></returns>
        public static int ParseInt(byte[] data)
        {
            byte by = data[index];
            int sign = ((by >> 6) & 1) == 1 ? -1 : 1;
            int last6 = (by & filter6);
            if ((by >> 7) == 1)
            {
                index += 1;
                return last6 * sign;
            }
            int num = 0;
            for (int i = 0; i < last6; i++)
            {
                num += data[index + i + 1] << (8 * i);
            }
            index += (last6 + 1);
            return num * sign;
        }
        /// <summary>
        /// 解析float类型的数据
        /// </summary>
        /// <returns></returns>
        public static float ParseFloat(byte[] data)
        {
            byte by = data[index];
            if (((by >> 7) & 1) == 0)
            {
                float f = BitConverter.ToSingle(data, index + 1);
                index += 5;
                return f;
            }
            int sign = ((by >> 5) & 1) == 1 ? -1 : 1;
            int last5 = (by & filter5);
            if (((by >> 6 & 1) == 1))
            {
                index += 1;
                return last5 * sign;
            }
            int num = 0;
            for (int i = 0; i < last5; i++)
            {
                num += data[index + i + 1] << (8 * i);
            }
            index += (last5 + 1);
            return num * sign;
        }
        /// <summary>
        /// 解析string类型的数据
        /// </summary>
        /// <returns></returns>
        public static string ParseString(byte[] data)
        {
            byte by = data[index];
            int first = (by >> 7);
            int second = ((by >> 6) & 1);
            int last6 = (by & filter6);
            if (first == 1)
            {
                int sign = second == 1 ? -1 : 1;
                index += 1;
                return (last6 * sign).ToString();
            }
            if (second == 1)
            {
                string s1 = Encoding.UTF8.GetString(data, index + 1, last6);
                index += (last6 + 1);
                return s1;
            }
            //此时las6表示字符串长度的字节数
            int strLen = 0;
            for (int i = 0; i < last6; i++)
            {
                strLen += (data[index + i + 1] << (8 * i));
            }
            //Console.WriteLine("last6 = " + last6);
            //Console.WriteLine("strLen = " + strLen);
            //Console.WriteLine("数组长度 = " + data.Length + "  索引起始 = " + (index + 1) + "  长度 = " + last6);
            string s2 = Encoding.UTF8.GetString(data, index + 1 + last6, strLen);
            index += (1 + last6 + strLen);
            return s2;
        }
        /// <summary>
        /// 解析行列数据，自动调整index值
        /// </summary>
        /// <returns>行长度或列长度</returns>
        public static int ParseRowColValue(byte[] data)
        {
            byte by = data[index];
            if (((by >> 7) & 1) == 1)  //说明后7字节为行长
            {
                index += 1;
                return (by & filter7);
            }
            else
            {
                int len = by;
                int num = 0;
                for (int i = 0; i < len; i++)
                {
                    num += (data[index + i + 1] << (8 * i));
                }
                index += (len + 1);
                return num;
            }
        }
        /// <summary>
        /// 解析所有列对应的类型，自动调整index
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="colNum">列数</param>
        /// <returns>类型数组</returns>
        public static ElemType[] ParseElemType(byte[] data, int colNum)
        {
            ElemType[] ret = new ElemType[colNum];
            int len = (int)Math.Ceiling((double)colNum / 2);
            bool front = true;
            for (int i = 0; i < colNum; i++)
            {
                if (front)
                {
                    ret[i] = GetElemTypeByInt((data[index + (i / 2)] >> 4));
                    front = false;
                }

                else
                {
                    ret[i] = GetElemTypeByInt((data[index + (i / 2)] & filter4));
                    front = true;
                }

            }
            index += len;
            return ret;
        }
        /// <summary>
        /// 获取int值对应的变量类型
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static ElemType GetElemTypeByInt(int val)
        {
            switch (val)
            {
                case 1:
                    return ElemType.Int;
                case 2:
                    return ElemType.Float;
                case 3:
                    return ElemType.String;
                default:
                    throw new Exception("错误的类型数值 = " + val);
            }
        }

        /// <summary>
        /// 解析int类型的数据
        /// </summary>
        /// <returns></returns>
        public static int ParseInt1(byte[] data, ref int index)
        {
            byte by = data[index];
            int sign = ((by >> 6) & 1) == 1 ? -1 : 1;
            int last6 = (by & filter6);
            if ((by >> 7) == 1)
            {
                index += 1;
                return last6 * sign;
            }
            int num = 0;
            for (int i = 0; i < last6; i++)
            {
                num += data[index + i + 1] << (8 * i);
            }
            index += (last6 + 1);
            return num * sign;
        }
        public static int ParseInt3(byte[] data)
        {
            return data[0] + (data[1] << 8) + (data[2] << 16);
        }
        /// <summary>
        /// 解析float类型的数据
        /// </summary>
        /// <returns></returns>
        public static float ParseFloat1(byte[] data, ref int index)
        {
            byte by = data[index];
            if (((by >> 7) & 1) == 0)
            {
                float f = BitConverter.ToSingle(data, index + 1);
                index += 5;
                return f;
            }
            int sign = ((by >> 5) & 1) == 1 ? -1 : 1;
            int last5 = (by & filter5);
            if (((by >> 6 & 1) == 1))
            {
                index += 1;
                return last5 * sign;
            }
            int num = 0;
            for (int i = 0; i < last5; i++)
            {
                num += data[index + i + 1] << (8 * i);
            }
            index += (last5 + 1);
            return num * sign;
        }
        /// <summary>
        /// 解析string类型的数据
        /// </summary>
        /// <returns></returns>
        public static string ParseString1(byte[] data, ref int index)
        {
            byte by = data[index];
            int first = (by >> 7);
            int second = ((by >> 6) & 1);
            int last6 = (by & filter6);
            if (first == 1)
            {
                int sign = second == 1 ? -1 : 1;
                index += 1;
                return (last6 * sign).ToString();
            }
            if (second == 1)
            {
                string s1 = Encoding.UTF8.GetString(data, index + 1, last6);
                index += (last6 + 1);
                return s1;
            }
            //此时las6表示字符串长度的字节数
            int strLen = 0;
            for (int i = 0; i < last6; i++)
            {
                strLen += (data[index + i + 1] << (8 * i));
            }
            Console.WriteLine("last6 = " + last6);
            Console.WriteLine("strLen = " + strLen);
            Console.WriteLine("数组长度 = " + data.Length + "  索引起始 = " + (index + 1) + "  长度 = " + last6);
            string s2 = Encoding.UTF8.GetString(data, index + 1 + last6, strLen);
            index += (1 + last6 + strLen);
            return s2;
        }

        public static Dictionary<int, int> ParseIndexData(byte[] data, int length)
        {
            Dictionary<int, int> ret = new Dictionary<int, int>();
            int indexoff = length * 3;

            for (int i = 0; i < length; i++)
            {
                int id = data[index] + (data[index + 1] << 8) + (data[index + 2] << 16);
                int pos = data[index + indexoff] + (data[index + indexoff + 1] << 8) + (data[index + indexoff + 2] << 16);
                //Console.WriteLine(id);
                ret.Add(id, pos);
                index += 3;
            }
            index += indexoff;
            return ret;
        }

        public static Model GetModel(byte[] data, int id)
        {
            Model md = new Model();
            //int index = id2startIndex[id];
            Console.WriteLine("index = " + index);
            md.id = ParseInt1(data, ref index);
            md.sceneid = ParseInt1(data, ref index);
            md.name = ParseString1(data, ref index);
            md.type = ParseInt1(data, ref index);
            md.path = ParseString1(data, ref index);
            md.display = ParseInt1(data, ref index);
            md.loadPriority = ParseInt1(data, ref index);
            md.xzPos = ParseInt1(data, ref index);
            md.x = ParseFloat1(data, ref index);
            md.y = ParseFloat1(data, ref index);
            md.z = ParseFloat1(data, ref index);
            md.quaterX = ParseFloat1(data, ref index);
            md.quaterY = ParseFloat1(data, ref index);
            md.quaterZ = ParseFloat1(data, ref index);
            md.quaterW = ParseFloat1(data, ref index);
            md.scaleX = ParseFloat1(data, ref index);
            md.scaleY = ParseFloat1(data, ref index);
            md.scaleZ = ParseFloat1(data, ref index);
            md.layer = ParseInt1(data, ref index);
            md.box = ParseInt1(data, ref index);
            md.triger = ParseInt1(data, ref index);
            md.convex = ParseInt1(data, ref index);
            md.rigi = ParseInt1(data, ref index);
            md.boxX = ParseInt1(data, ref index);
            md.boxY = ParseInt1(data, ref index);
            md.boxZ = ParseInt1(data, ref index);
            md.boxCenterX = ParseFloat1(data, ref index);
            md.boxCenterY = ParseFloat1(data, ref index);
            md.boxCenterZ = ParseFloat1(data, ref index);
            md.coliType = ParseInt1(data, ref index);
            md.coliValue = ParseInt1(data, ref index);
            md.coliCondition = ParseInt1(data, ref index);
            md.nvX = ParseFloat1(data, ref index);
            md.nvY = ParseFloat1(data, ref index);
            md.nvZ = ParseFloat1(data, ref index);
            md.lod = ParseInt1(data, ref index);
            md.sceneLV = ParseInt1(data, ref index);
            md.lightmapIndex = ParseInt1(data, ref index);
            md.lightmapScaleOffsetX = ParseFloat1(data, ref index);
            md.lightmapScaleOffsetY = ParseFloat1(data, ref index);
            md.lightmapScaleOffsetZ = ParseFloat1(data, ref index);
            md.lightmapScaleOffsetW = ParseFloat1(data, ref index);
            md.lightAniPath = ParseString1(data, ref index);
            //Console.WriteLine("index = " + index);
            return md;
        }

        public struct Model
        {
            /// <summary>
            /// 字典id
            /// </summary>
            public int id;
            /// <summary>
            /// 场景id
            /// </summary>
            public int sceneid;
            /// <summary>
            /// name
            /// </summary>
            public string name;
            /// <summary>
            /// 类型
            /// </summary>
            public int type;
            /// <summary>
            /// 预制体路径
            /// </summary>
            public string path;
            /// <summary>
            /// 显示状态
            /// </summary>
            public int display;
            /// <summary>
            /// 加载等级
            /// </summary>
            public int loadPriority;
            /// <summary>
            /// Grass位置
            /// </summary>
            public int xzPos;
            /// <summary>
            /// X坐标
            /// </summary>
            public float x;
            /// <summary>
            /// Y坐标
            /// </summary>
            public float y;
            /// <summary>
            /// Z坐标
            /// </summary>
            public float z;
            /// <summary>
            /// 四元数X
            /// </summary>
            public float quaterX;
            /// <summary>
            /// 四元数Y
            /// </summary>
            public float quaterY;
            /// <summary>
            /// 四元数Z
            /// </summary>
            public float quaterZ;
            /// <summary>
            /// 四元数W
            /// </summary>
            public float quaterW;
            /// <summary>
            /// X缩放
            /// </summary>
            public float scaleX;
            /// <summary>
            /// Y缩放
            /// </summary>
            public float scaleY;
            /// <summary>
            /// Z缩放
            /// </summary>
            public float scaleZ;
            /// <summary>
            /// layer
            /// </summary>
            public int layer;
            /// <summary>
            /// box当lightType
            /// </summary>
            public int box;
            /// <summary>
            /// triger当shadow
            /// </summary>
            public int triger;
            /// <summary>
            /// 是否是convex
            /// </summary>
            public int convex;
            /// <summary>
            /// rigi当cull
            /// </summary>
            public int rigi;
            /// <summary>
            /// boxX当r
            /// </summary>
            public float boxX;
            /// <summary>
            /// boxY当g
            /// </summary>
            public float boxY;
            /// <summary>
            /// boxZ当b
            /// </summary>
            public float boxZ;
            /// <summary>
            /// boxCenterX
            /// </summary>
            public float boxCenterX;
            /// <summary>
            /// boxCenterY
            /// </summary>
            public float boxCenterY;
            /// <summary>
            /// boxCenterZ
            /// </summary>
            public float boxCenterZ;
            /// <summary>
            /// coliType当mode
            /// </summary>
            public int coliType;
            /// <summary>
            /// coliValue
            /// </summary>
            public int coliValue;
            /// <summary>
            /// coliCondition
            /// </summary>
            public int coliCondition;
            /// <summary>
            /// nvX当intensity
            /// </summary>
            public float nvX;
            /// <summary>
            /// nvY
            /// </summary>
            public float nvY;
            /// <summary>
            /// nvZ
            /// </summary>
            public float nvZ;
            /// <summary>
            /// lod
            /// </summary>
            public int lod;
            /// <summary>
            /// 场景等级标记
            /// </summary>
            public int sceneLV;
            /// <summary>
            /// 光照贴图Index
            /// </summary>
            public int lightmapIndex;
            /// <summary>
            /// 光照贴图偏移X
            /// </summary>
            public float lightmapScaleOffsetX;
            /// <summary>
            /// 光照贴图偏移Y
            /// </summary>
            public float lightmapScaleOffsetY;
            /// <summary>
            /// 光照贴图偏移Z
            /// </summary>
            public float lightmapScaleOffsetZ;
            /// <summary>
            /// 光照贴图偏移W
            /// </summary>
            public float lightmapScaleOffsetW;
            /// <summary>
            /// 灯光路径动画
            /// </summary>
            public string lightAniPath;
        }
    }

    /// <summary>
    /// 定义的元素数据类型
    /// </summary>
    public enum ElemType
    {
        Int = 1,
        Float,
        String,
    }
}
