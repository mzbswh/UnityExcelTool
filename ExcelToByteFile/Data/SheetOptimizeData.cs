using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelToByteFile
{
    public struct SheetOptimizeData
    {
        public OptimizeType OptimizeType { get; }
        /// <summary>
        /// 每段有多少个元素
        /// </summary>
        public List<int> SegmentList { get; }
        /// <summary>
        /// 分段后每段开始的元素值
        /// </summary>
        public List<long> SegmentStartList { get; }
        /// <summary>
        /// OptimizeType为PartialContinuity时，这个片段在SegmentList的索引
        /// </summary>
        public int PartialContinuityStart { get; }
        public long Step { get; }

        public SheetOptimizeData(OptimizeType optimizeType, List<int> segment, List<long> segmentStart, int partialContinuityStart, long step)
        {
            OptimizeType = optimizeType;
            SegmentList = segment;
            SegmentStartList = segmentStart;
            PartialContinuityStart = partialContinuityStart;
            Step = step;
        }
         
    }
}
