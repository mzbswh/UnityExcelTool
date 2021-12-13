using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelToByteFile
{
    public class SheetOptimizeData
    {
        public OptimizeType OptimizeType { get; }
        public List<int> SegmentList { get; }
        /// <summary>
        /// OptimizeType为PartialContinuity时，这个片段在SegmentList的索引
        /// </summary>
        public int PartialContinuityStart { get; }
        public ulong Step { get; }

        public SheetOptimizeData(OptimizeType optimizeType, List<int> segment, int partialContinuityStart, ulong step)
        {
            OptimizeType = optimizeType;
            SegmentList = segment;
            PartialContinuityStart = partialContinuityStart;
            Step = step;
        }
         
    }
}
