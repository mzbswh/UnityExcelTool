using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelToByteFile
{
    public static class Optimize
    {
        /* 以下为可优化的条件 
         * 1. 主列必须的整数数值类型（可转为long且数据不丢失）且递增
         * 2. 主列的数据是连续的步长可不为1
         * 3. 主列数据不连续但可分成步长为1的有限个片段，
         * 4. 主列数据不连续且片段过多，但最大的片段比例大于某一数值, 且步长为1
         */

        public static long step = 0;
        public static List<int> segment;
        public static List<long> segmentStart;
        public static int partialContinuityStart = 0;

        public static OptimizeType GetOptimizeType(List<long> numberSequence)
        {
            OptimizeType type = OptimizeType.None;
            if (IsConstantStep(numberSequence))
            {
                type = OptimizeType.Continuity;
                step = numberSequence[1] - numberSequence[0];
            }
            else
            {
                segment = GetSegment(numberSequence);
                int maxSegment = (int)Math.Round(MathF.Log2(numberSequence.Count), 0) - 3;
                if (segment.Count <= maxSegment)
                {
                    type = OptimizeType.Segment;
                }
                else
                {
                    int maxRatio = 0;
                    partialContinuityStart = 0;
                    for (int i = 0; i < segment.Count; i++)
                    {
                        var seg = segment[i];
                        int ratio = (int)(((float)seg / numberSequence.Count) * 100);
                        if (ratio > maxRatio)
                        {
                            partialContinuityStart = i;
                            maxRatio = ratio;
                        }
                    }
                    // 如果片段过多且总数大于20且最大比例的高于80%，则这80%生成为连续，其它的20%加入字典
                    if (numberSequence.Count > 20 && maxRatio > 80)
                    {
                        type = OptimizeType.PartialContinuity;
                    }
                }
            }
            return type;
        }

        /// <returns>每一段的数量是多少</returns>
        static List<int> GetSegment(List<long> numberSequence)
        {
            long num = numberSequence[0];
            int cnt = 1;
            List<int> result = new List<int>();
            segmentStart = new List<long>();
            segmentStart.Add(numberSequence[0]);
            for (int i = 1; i < numberSequence.Count; i++)
            {
                long n = numberSequence[i];
                if (n != num + 1)
                {
                    segmentStart.Add(n);
                    result.Add(cnt);
                    cnt = 0;
                }
                num = n;
                cnt++;
                if (i == numberSequence.Count - 1)
                {
                    result.Add(cnt);
                    break;
                }
            }
            return result;
        }

        static bool IsConstantStep(List<long> numberSequence)
        {
            long step = numberSequence[1] - numberSequence[0];
            int i = 2;
            var last = numberSequence[1];
            while (i <= numberSequence.Count - 1)
            {
                var num = numberSequence[i];
                if (num - last == step)
                {
                    i++;
                    last = num;
                    continue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
