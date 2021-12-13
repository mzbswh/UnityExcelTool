using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelToByteFile
{
    public enum TypeToken
    {
        Null = 0,
        Sbyte = 1,
        Byte = 2,
        Bool = 3,
        UShort = 4,
        Short = 5,
        UInt = 6,
        Int = 7,
        Float = 8,
        Double = 9,
        ULong = 10,
        Long = 11,
        String = 12,
        List = 100,
        Dictionary = 10000,
        Vector = 20000,
    }
}
