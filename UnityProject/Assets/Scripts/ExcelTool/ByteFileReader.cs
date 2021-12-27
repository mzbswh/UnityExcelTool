using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ICSharpCode.NRefactory.Ast;

namespace ExcelToByteFile
{
    /// <summary>
    /// 用于读取所有的数据，自动增长
    /// </summary>
    public class ByteFileReader
    {
        static byte[] data;
        static int row = 0;
        static int col = 0;
        static int colCnt;
        static int rowLength;
        static List<int> colOff;

        //public ByteFileReader(byte[] data, int rowLength, List<int> colOff)
        //{
        //    this.data = data;
        //    this.colCnt = colOff.Count;
        //    this.rowLength = rowLength;
        //    this.colOff = colOff;
        //}

        public static void Reset(byte[] data1, int rowLength1, List<int> colOff1)
        {
            data = data1;
            colCnt = colOff1.Count;
            rowLength = rowLength1;
            colOff = colOff1;
            row = 0; 
            col = 0;
        }

        public static T Get<T>()
        {
            var ret = ByteReader.Read<T>(data, row * rowLength + colOff[col]);
            col++;
            if (col >= colCnt)
            {
                col = 0;
                row++;
            }
            return ret;
        }

        public static Dictionary<K, V> GetDict<K, V>()
        {
            var ret = ByteReader.ReadDict<K, V>(data, row * rowLength + colOff[col]);
            col++;
            if (col >= colCnt)
            {
                col = 0;
                row++;
            }
            return ret;
        }

        public static void SkipOne()
        {
            col++;
            if (col >= colCnt)
            {
                col = 0;
                row++;
            }
        }
    }
}
