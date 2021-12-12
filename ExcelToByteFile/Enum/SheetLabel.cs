using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelToByteFile
{
    public enum RowLabel
    {
        None,
        Type,
        Name,
        Comment,
        Note        // # 或 Note
    }

    public enum ColLabel
    {
        None,
        Primary,    // KEY 或 Primary
        Note
    }
}
