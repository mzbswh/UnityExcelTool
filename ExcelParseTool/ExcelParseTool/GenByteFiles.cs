using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ExcelParseTool
{
    partial class ParseTool
    {
        // 存储各类型的数量
        static int totalFileNum = 0;           // 总文件个数
        static int intDataNum = 0;             // int数据数量
        static int floatDataNum = 0;           // float数据数量
        static int stringDataNum = 0;          // string数据数量
        static List<byte> allFileNames = new List<byte>();        // 存储所有的文件名称，带后缀

        /// <summary>
        /// 输入字节文件生成模式
        /// </summary>
        public static void InputByteFileGenMode()
        {
            Console.Write("是否生成偏移数据（0：不生成，1：生成每行起始偏移，2：生成每个数据的偏移）：");
            key = Console.ReadKey();
            Console.WriteLine();
            if (key.KeyChar == '0')
                byteFileOffLevel = 0;
            else if (key.KeyChar == '1')
                byteFileOffLevel = 1;
            else
                byteFileOffLevel = 2;
        }
        /// <summary>
        /// 生成单个字节数据文件
        /// </summary>
        public static void GenSingleByteDataFile(FileInfo file, IWorkbook wb)
        {
            List<byte> data = new List<byte>();
            ISheet sheet = wb.GetSheetAt(0);
            int colNum = GetTotalCols(sheet);
            int rowNum = GetTotalRows(sheet);
            ElemType[] elemTypes = GetAllElemType(sheet.GetRow(1), colNum);
            Console.WriteLine("列 = " + colNum + "  行 = " + rowNum + "(实际数据的行数，不包括前4行)");
            logStr += "列 = " + colNum + "  行 = " + rowNum + "(实际数据的行数，不包括前4行)\n";
            //添加头数据
            data.Add((byte)byteFileOffLevel);//添加文件信息字节
            data.AddRange(GenIntData(rowNum, true));//添加行数信息，实际数据的行数
            data.AddRange(GenIntData(colNum, true));//添加列数信息
            data.AddRange(GenTypeData(sheet.GetRow(1), colNum));//添加变量类型信息

            //循环读取数据内容，实际内容从4行开始
            List<byte> mainData = new List<byte>();//存储主要数据
            int[,] offData_Twice = new int[rowNum, colNum];//存储二次偏移数据
            byte[] oneData;//单个数据
            int[] lens = new int[rowNum]; //每行数据所占的字节长度，用于统计一次偏移
            int[] ids = new int[rowNum];//存储每行的id，因为可能不连续
            for (int i = 0; i < rowNum; i++)
            {
                int dataLen = 0;
                for (int j = 0; j < colNum; j++)
                {
                    ICell cell = sheet.GetRow(i + 4).GetCell(j);
                    ElemType elemType = elemTypes[j];
                    oneData = GenDataByCellAndElemType(cell, elemType, (i + 5), (j + 1));
                    mainData.AddRange(oneData);
                    dataLen += oneData.Length;
                    offData_Twice[i, j] = dataLen - oneData.Length;
                }
                lens[i] = dataLen;
                ids[i] = (int)sheet.GetRow(i + 4).GetCell(0).NumericCellValue;
            }

            //存储所有的id
            //for (int i = 0; i < rowNum; i++)
            //{
            //    byte[] bys = BitConverter.GetBytes(ids[i]);
            //    data.Add(bys[0]);
            //    data.Add(bys[1]);
            //    data.Add(bys[2]);
            //}

            //计算偏移(一次偏移，有无二次偏移会影响偏移值) ：第一个数据偏移 = 头部数据长度 + 偏移长度  第二个偏移 = 前一个偏移+前一个总长度  
            //固定使用 3 字节（能索引总15M长度的字节数组，完全够用）(如果变长那么实际数据索引也会跟着变动，则会无法定位数据位置)
            //每个2次偏移所有2字节索引
            if (byteFileOffLevel == 2)
            {
                int headLen = data.Count;//头部长度
                byte[] offData_Once = new byte[(lens.Length * 3)];
                int len = headLen + offData_Once.Length + offData_Twice.Length * 2;
                byte[] by = BitConverter.GetBytes(len);
                offData_Once[0] = by[0];//只取前三个，因为第四个字节必定是0，除非总字节长度超过16777215（15M)
                offData_Once[1] = by[1];
                offData_Once[2] = by[2];
                int index = 3;
                for (int i = 0; i < lens.Length - 1; i++)
                {
                    len += lens[i];
                    byte[] bys = BitConverter.GetBytes(len);
                    offData_Once[index] = bys[0];
                    offData_Once[index + 1] = bys[1];
                    offData_Once[index + 2] = bys[2];
                    index += 3;
                }
                //Console.WriteLine(offData_Once.Length);
                //添加偏移数据和主要数据
                data.AddRange(offData_Once);
                //添加2次偏移数据
                for (int i = 0; i < offData_Twice.GetLength(0); i++)
                {
                    for (int j = 0; j < offData_Twice.GetLength(1); j++)
                    {
                        byte[] off = BitConverter.GetBytes(offData_Twice[i, j]);
                        data.Add(off[0]);
                        data.Add(off[1]);
                    }
                }
            }
            else if (byteFileOffLevel == 1)
            {
                int headLen = data.Count;//头部长度
                byte[] offData_Once = new byte[(lens.Length * 3)];
                int len = headLen + offData_Once.Length;
                byte[] by = BitConverter.GetBytes(len);
                offData_Once[0] = by[0];//只取前三个，因为第四个字节必定是0，除非总字节长度超过16777215（15M)
                offData_Once[1] = by[1];
                offData_Once[2] = by[2];
                int index = 3;
                for (int i = 0; i < lens.Length - 1; i++)
                {
                    len += lens[i];
                    byte[] bys = BitConverter.GetBytes(len);
                    offData_Once[index] = bys[0];
                    offData_Once[index + 1] = bys[1];
                    offData_Once[index + 2] = bys[2];
                    index += 3;
                }
                //Console.WriteLine(offData_Once.Length);
                //添加偏移数据和主要数据
                data.AddRange(offData_Once);
            }
            data.AddRange(mainData);
            //生成字节文件
            string fileName = Path.GetFileNameWithoutExtension(file.Name).ToLower() + ".bytes";
            string path = parentDir + outputDir + byteFilesDir + "/" + fileName;
            using (FileStream fs1 = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                Console.WriteLine("解析完成，开始生成bytes文件，" + "长度 = " + data.Count + "字节  " + (data.Count / 1024) + "KB");
                logStr += "解析完成，开始生成bytes文件，" + "长度 = " + data.Count + "字节  " + (data.Count / 1024) + "KB\n";
                byte[] write = data.ToArray();
                fs1.Write(write, 0, write.Length);
            }
            Console.WriteLine("生成" + Path.GetFileNameWithoutExtension(file.Name) + ".bytes文件成功");
            logStr += "生成" + Path.GetFileNameWithoutExtension(file.Name) + ".bytes文件成功\n";
            //数据记录
            totalFileNum++;
            allFileNames.AddRange(GenStringData(fileName));
        }
        /// <summary>
        /// 根据单元格的类型和自身定义的类型生成数据，单元格不为null时使用此方法
        /// </summary>
        /// <returns></returns>
        public static byte[] GenDataByCellAndElemType(ICell cell, ElemType elemType, int row, int col)
        {
            byte[] oneData;
            switch (elemType)
            {
                case ElemType.Int:
                    intDataNum++;
                    oneData = GenIntData(GetIntValueInCell(cell, row, col), false);
                    break;
                case ElemType.Float:
                    floatDataNum++;
                    oneData = GenFloatData(GetFloatValueInCell(cell, row, col));
                    break;
                case ElemType.String:
                    stringDataNum++;
                    oneData = GenStringData(GetStringValueInCell(cell, row, col));
                    break;
                default:
                    throw new Exception("错误的变量类型 = " + elemType.ToString());
            }
            return oneData;
        }
        /// <summary>
        /// 生成表示int类型的字节数组，对于行列的长度 1字节最大127，2字节最大256， 3字节最大65535，4字节16777215，5字节4294967295
        /// 对于数据长度 1字节63（第二位被符号位占了） 2字节256 3字节65535 4字节16777215 5字节 4294967295
        /// </summary>
        /// <param name="data">数据值</param>
        /// <param name="isOnlyPositive">是否一定是正整数，这样不需要符号位，就会使用7个bit位存数据</param>
        /// <returns>生成的字节数组</returns>
        public static byte[] GenIntData(int data, bool isOnlyPositive)
        {
            byte[] ret;
            if (isOnlyPositive)
            {
                //只需要一个字节表示
                if (data <= 127)
                {
                    ret = new byte[1];
                    ret[0] = (byte)(128 + data);
                }
                else if ((data > 127) && (data <= 256))
                {
                    ret = new byte[2];
                    //第一个字节为需要字节的长度信息
                    ret[0] = (byte)(1);
                    ret[1] = (byte)(data);
                }
                else if ((data > 256) && (data <= ushort.MaxValue))
                {
                    ret = new byte[3];
                    ret[0] = (byte)(2);
                    ret[1] = (byte)(data & filter8);
                    ret[2] = (byte)((data >> 8) & filter8);
                }
                else if ((data > ushort.MaxValue) && (data <= 16777215))
                {
                    ret = new byte[4];
                    ret[0] = (byte)(3);
                    ret[1] = (byte)(data & filter8);
                    ret[2] = (byte)((data >> 8) & filter8);
                    ret[3] = (byte)((data >> 16) & filter8);
                }
                else if ((data > 16777215))
                {
                    ret = new byte[5];
                    ret[0] = (byte)(4);
                    ret[1] = (byte)(data & filter8);
                    ret[2] = (byte)((data >> 8) & filter8);
                    ret[3] = (byte)((data >> 16) & filter8);
                    ret[4] = (byte)((data >> 24) & filter8);
                }
                else
                {
                    Console.WriteLine("数值超出int类型最大值");
                    throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                bool isPositive = data >= 0 ? true : false;//是否是正数
                int absData = Math.Abs(data);
                if (absData <= 63)//只需要一个字节
                {
                    ret = new byte[1];
                    if (isPositive)
                        ret[0] = (byte)(128 + absData); //128=10000000 最高位为1和第二个符号位为0
                    else
                        ret[0] = (byte)(192 + absData); //192=11000000
                }
                else if ((absData > 63) && (absData <= 255))
                {
                    ret = new byte[2];
                    if (isPositive)
                        ret[0] = (byte)(1);
                    else
                        ret[0] = (byte)(64 + 1);//64 = 01000000
                    ret[1] = (byte)absData;
                }
                else if ((absData > 255) && (absData <= ushort.MaxValue))
                {
                    ret = new byte[3];
                    if (isPositive)
                        ret[0] = (byte)(2);
                    else
                        ret[0] = (byte)(64 + 2);
                    ret[1] = (byte)(absData & filter8);
                    ret[2] = (byte)((absData >> 8) & filter8);
                }
                else if ((absData > ushort.MaxValue) && (absData <= 16777215))
                {
                    ret = new byte[4];
                    if (isPositive)
                        ret[0] = (byte)(3);
                    else
                        ret[0] = (byte)(64 + 3);
                    ret[1] = (byte)(absData & filter8);
                    ret[2] = (byte)((absData >> 8) & filter8);
                    ret[3] = (byte)((absData >> 16) & filter8);
                }
                else if ((absData > 16777215))
                {
                    ret = new byte[5];
                    if (isPositive)
                        ret[0] = (byte)(4);
                    else
                        ret[0] = (byte)(64 + 4);
                    ret[1] = (byte)(absData & filter8);
                    ret[2] = (byte)((absData >> 8) & filter8);
                    ret[3] = (byte)((absData >> 16) & filter8);
                    ret[4] = (byte)((absData >> 24) & filter8);
                }
                else
                {
                    Console.WriteLine("数值超出int类型最大值");
                    throw new ArgumentOutOfRangeException();
                }
            }
            return ret;
        }
        /// <summary>
        /// 生成表示浮点类型的字节数组
        /// </summary>
        /// <param name="data">浮点值</param>
        /// <returns></returns>
        public static byte[] GenFloatData(float data)
        {
            byte[] ret;
            int temp = (int)data;
            if (temp == data)//说明这个float是一个整数类型,按整数处理
            {
                bool isPositive = (temp >= 0 ? true : false);//是否是正数
                int absData = Math.Abs(temp);
                if (absData <= 31)//只需要一个字节
                {
                    ret = new byte[1];
                    if (isPositive)
                        ret[0] = (byte)(192 + absData); //192=11000000 最高位为1(按整数处理)和第二位为1，一个字节，第三位为0 正数
                    else
                        ret[0] = (byte)(224 + absData); //224=11100000
                }
                else if ((absData > 31) && (absData <= 255))
                {
                    ret = new byte[2];
                    if (isPositive)
                        ret[0] = (byte)(128 + 1);
                    else
                        ret[0] = (byte)(160 + 1);//160 = 10100000 第三位负数
                    ret[1] = (byte)absData;
                }
                else if ((absData > 255) && (absData <= ushort.MaxValue))
                {
                    ret = new byte[3];
                    if (isPositive)
                        ret[0] = (byte)(128 + 2);
                    else
                        ret[0] = (byte)(160 + 2);
                    ret[1] = (byte)(absData & filter8);
                    ret[2] = (byte)((absData >> 8) & filter8);
                }
                else if ((absData > ushort.MaxValue) && (absData <= 16777215))
                {
                    ret = new byte[4];
                    if (isPositive)
                        ret[0] = (byte)(128 + 3);
                    else
                        ret[0] = (byte)(160 + 3);
                    ret[1] = (byte)(absData & filter8);
                    ret[2] = (byte)((absData >> 8) & filter8);
                    ret[3] = (byte)((absData >> 16) & filter8);
                }
                else if ((absData > 16777215))
                {
                    ret = new byte[5];
                    if (isPositive)
                        ret[0] = (byte)(128 + 4);
                    else
                        ret[0] = (byte)(160 + 4);
                    ret[1] = (byte)(absData & filter8);
                    ret[2] = (byte)((absData >> 8) & filter8);
                    ret[3] = (byte)((absData >> 16) & filter8);
                    ret[4] = (byte)((absData >> 24) & filter8);
                }
                else
                {
                    Console.WriteLine("数值超出int类型最大值");
                    throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                ret = new byte[5];
                ret[0] = (byte)0;
                byte[] tmp = BitConverter.GetBytes(data);
                ret[1] = tmp[0];
                ret[2] = tmp[1];
                ret[3] = tmp[2];
                ret[4] = tmp[3];
            }
            return ret;
        }
        /// <summary>
        /// 生成表示字符串类型的字节数组
        /// </summary>
        /// <param name="str">字符串数据</param>
        /// <returns></returns>
        public static byte[] GenStringData(string str)
        {
            byte[] ret;
            int temp;
            bool canToInt = int.TryParse(str, out temp);
            int absInt = Math.Abs(temp);
            if (canToInt && absInt <= 63)
            {
                ret = new byte[1];
                if (temp >= 0)
                    ret[0] = (byte)(128 + absInt);
                else
                    ret[0] = (byte)(192 + absInt); //11000000
            }
            else
            {
                byte[] by = Encoding.UTF8.GetBytes(str);
                int len = by.Length;
                if (len <= 63)
                {
                    ret = new byte[len + 1];
                    ret[0] = (byte)(64 + len);  //01 000000 此时第二位为1 表示后六字节是长度
                    for (int i = 0; i < len; i++)
                    {
                        ret[i + 1] = by[i];
                    }
                }
                else if ((len > 63) && len <= 255) //额外一个字节存长度
                {
                    ret = new byte[len + 1 + 1];
                    ret[0] = (byte)(1);
                    ret[1] = (byte)(len);
                    for (int i = 0; i < len; i++)
                    {
                        ret[i + 2] = by[i];
                    }
                }
                else if ((len > 255) && len < ushort.MaxValue) //额外两个字节存长度
                {
                    ret = new byte[len + 1 + 2];
                    ret[0] = (byte)(2);
                    ret[1] = (byte)(len & filter8);
                    ret[2] = (byte)((len >> 8) & filter8);
                    for (int i = 0; i < len; i++)
                    {
                        ret[i + 3] = by[i];
                    }
                }
                else if ((len > ushort.MaxValue) && len <= 16777215) //额外两个字节存长度
                {
                    ret = new byte[len + 1 + 3];
                    ret[0] = (byte)(3);
                    ret[1] = (byte)(len & filter8);
                    ret[2] = (byte)((len >> 8) & filter8);
                    ret[3] = (byte)((len >> 16) & filter8);
                    for (int i = 0; i < len; i++)
                    {
                        ret[i + 4] = by[i];
                    }
                }
                else
                {
                    Console.WriteLine("字符串长度大于16777215个字节？？？");
                    throw new Exception("错误");
                }
            }
            return ret;
        }
        /// <summary>
        /// 生成类型的字节表示，共列长个字节
        /// </summary>
        /// <param name="rowData">类型那一行数据</param>
        /// <returns>生成的字节数组</returns>
        public static byte[] GenTypeData(IRow rowData, int colNum)
        {
            bool front = true;//是否是前半个字节
            byte[] ret = new byte[(int)Math.Ceiling((double)colNum / 2)];
            for (int i = 0; i < colNum; i++)
            {
                string typeStr = rowData.GetCell(i).StringCellValue.ToLower();
                switch (typeStr)
                {
                    case IntString:
                        if (front)
                        {
                            ret[i / 2] = ((byte)ElemType.Int << 4);
                            front = false;
                        }
                        else
                        {
                            ret[i / 2] = (byte)(ret[i / 2] + (byte)ElemType.Int);
                            front = true;
                        }

                        break;
                    case FloatString:
                        if (front)
                        {
                            ret[i / 2] = ((byte)ElemType.Float << 4);
                            front = false;
                        }
                        else
                        {
                            ret[i / 2] = (byte)(ret[i / 2] + (byte)ElemType.Float);
                            front = true;
                        }
                        break;
                    case StringString:
                        if (front)
                        {
                            ret[i / 2] = ((byte)ElemType.String << 4);
                            front = false;
                        }
                        else
                        {
                            ret[i / 2] = (byte)(ret[i / 2] + (byte)ElemType.String);
                            front = true;
                        }
                        break;
                    default:
                        Console.WriteLine("错误：未添加的变量类型 = " + typeStr);
                        throw new Exception("错误的类型");
                }
            }
            return ret;
        }

       

        //检查单元格类型是否正确
        /// <summary>
        /// 检查数字单元格是否是int类型的
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool CheckIsIntType(double value)
        {
            return ((int)value).ToString() == value.ToString() ? true : false;
        }
        /// <summary>
        /// 检查数字单元格是否是int类型的
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool CheckIsIntType(string value)
        {
            int i;
            bool b = int.TryParse(value, out i);
            return b;
        }
        /// <summary>
        /// 检查数字单元格是否是float类型的
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool CheckIsFloatType(double value)
        {
            return ((float)value).ToString() == value.ToString() ? true : false;
        }
        /// <summary>
        /// 检查数字单元格是否是float类型的
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool CheckIsFloatType(string value)
        {
            float i;
            bool b = float.TryParse(value, out i);
            return b;
        }
    }
}
