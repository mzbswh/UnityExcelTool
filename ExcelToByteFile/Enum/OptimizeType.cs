using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelToByteFile
{
    public enum OptimizeType : byte
    {
        None,
        Continuity,
        Segment,
        PartialContinuity
    }
}
