using System;
using System.Collections;
using System.Collections.Generic;
using ExcelToByteFile;
using UnityEngine;

public struct ByteFileParam
{
    public string fileName;
    public int idColIndex;
    public int rowCount;
    public int rowLen;
    public List<int> types;
    public List<int> colOff;
    public List<string> varNames;
    public bool cache;

    // -----优化相关-----
    public OptimizeType optimizeType;
    // 连续类型
    public int step;
    // 分段类型
    public List<int> segmentList;
    // 部分连续
    public int continuityStartOff;  // 连续部分起始偏移
    public int continuityCnt;       // 连续部分个数
    public long startVal;           // 连续部分开始值
}


public sealed class ByteFileInfo<TIdType>
{
    bool parsed = false;
    byte[] data;

    const int filter = 0xffff;

    /// <summary>
    /// 当优化类型为None时是所有的主列数据，为PartialContinuity时是不连续部分的主列数据，为其它优化类型时此值为空<br/>
    /// 可通过GetOptimizeInfo_得到相应的优化信息以获取所有的主列数据
    /// </summary>
    public TIdType[] Ids { get; private set; }
    public string Name { get; }
    public int IdColIndex { get; }
    public int RowCount { get; }
    public int RowLength { get; }
    public int ColCount => typeToken.Count;
    public string IdColName => varNames[IdColIndex];
    public OptimizeType OptimizeType { get; }
    public bool Cache { get; }
    private readonly List<int> typeToken;
    private readonly List<int> colOff;
    private readonly List<string> varNames;
    Dictionary<TIdType, int> id2RowStartOff;    // id对应行起始偏移

    /* ------ 优化 ------- */
    // 连续类型
    TIdType firstVal;
    readonly int step;
    // 分段类型
    readonly List<int> segmentList; // 每一段的长度
    List<TIdType> segmentStartList; // 每一段开始的元素值
    List<int> segmentStartOff;      // 每一段开始的偏移值
    // 部分连续
    readonly int continuityCnt;  // 连续部分个数
    readonly int continuityStartOff; // 连续部分起始偏移
    TIdType continuityStartVal; // 连续部分起始值
    /**********************/

    public ByteFileInfo(ByteFileParam param)
    {
        this.Name = param.fileName;
        this.IdColIndex = param.idColIndex;
        this.RowCount = param.rowCount;
        this.RowLength = param.rowLen;
        this.typeToken = param.types;
        this.colOff = param.colOff;
        this.varNames = param.varNames;
        this.OptimizeType = param.optimizeType;
        this.Cache = param.cache;
        if (OptimizeType == OptimizeType.Continuity)
        {
            this.step = param.step;
        }
        else if (OptimizeType == OptimizeType.Segment)
        {
            this.segmentList = param.segmentList;
        }
        else if (OptimizeType == OptimizeType.PartialContinuity)
        {
            this.continuityStartOff = param.continuityStartOff;
            this.continuityCnt = param.continuityCnt;
        }
        Parse();
    }

    public void Parse()
    {
        if (parsed) return;
        parsed = true;
        data = Resources.Load<TextAsset>("ByteData/" + Name).bytes;
        if (data.Length > 0)
        {
            switch (OptimizeType)
            {
                case OptimizeType.None:
                {
                    id2RowStartOff = new Dictionary<TIdType, int>();
                    int idColOff = colOff[IdColIndex];
                    Ids = new TIdType[RowCount];
                    for (int i = 0; i < RowCount; i++)
                    {
                        TIdType id = ByteReader.Read<TIdType>(data, i * RowLength + idColOff);
                        Ids[i] = id;
                        id2RowStartOff.Add(id, i * RowLength);
                    }
                    break;
                }
                case OptimizeType.Continuity:
                    firstVal = ByteReader.Read<TIdType>(data, colOff[IdColIndex]);
                    break;
                case OptimizeType.Segment:
                {
                    int idColOff = colOff[IdColIndex];
                    segmentStartOff = new List<int>(segmentList.Count);
                    segmentStartList = new List<TIdType>(segmentList.Count);
                    segmentStartOff.Add(0);
                    segmentStartList.Add(ByteReader.Read<TIdType>(data, idColOff));
                    for (int i = 1; i < segmentList.Count; i++)
                    {
                        segmentStartOff.Add(i * RowLength * segmentList[i - 1]);
                        segmentStartList.Add(ByteReader.Read<TIdType>(data, i * RowLength + idColOff));
                    }
                    break;
                }
                case OptimizeType.PartialContinuity:
                {
                    id2RowStartOff = new Dictionary<TIdType, int>();
                    int idColOff = colOff[IdColIndex];
                    int preCnt = continuityStartOff / RowLength;
                    Ids = new TIdType[RowCount - preCnt - continuityCnt];
                    for (int i = 0; i < preCnt; i++)
                    {
                        TIdType id = ByteReader.Read<TIdType>(data, i * RowLength + idColOff);
                        id2RowStartOff.Add(id, i * RowLength);
                        Ids[i] = id;
                    }
                    continuityStartVal = ByteReader.Read<TIdType>(data, preCnt * RowLength + idColOff);
                    var remainStart = preCnt + continuityCnt;
                    for (; remainStart < RowCount; remainStart++) 
                    {
                        TIdType id = ByteReader.Read<TIdType>(data, remainStart * RowLength + idColOff);
                        id2RowStartOff.Add(id, remainStart * RowLength);
                        Ids[remainStart - continuityCnt] = id;
                    }
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 获取变量在一行的索引，即第几个
    /// </summary>
    /// <param name="variableOff">变量偏移</param>
    public int GetIndex(int variableOff)
    {
        for (int i = 0; i < colOff.Count; i++)
        {
            if (colOff[i] == variableOff) return i;
        }
        return -1;
    }

    /// <summary>
    /// 获取优化类型为连续时的信息（* 仅优化类型为连续时可用）
    /// </summary>
    /// <param name="step">步长</param>
    /// <param name="firstValue">第一个元素值</param>
    public void GetOptimizeInfo_Continuity(out int step, out TIdType firstValue)
    {
        step = this.step;
        firstValue = firstVal;
    }

    /// <summary>
    /// 获取优化类型为部分连续时的信息（* 仅优化类型为部分时连续可用）
    /// </summary>
    /// <param name="startVal">连续部分起始主列值</param>
    /// <param name="continuityCnt">连续部分长度</param>
    public void GetOptimizeInfo_PartialContinuity(out TIdType startVal, out int continuityCnt)
    {
        startVal = continuityStartVal;
        continuityCnt = this.continuityCnt;
    }

    /// <summary>
    /// 获取优化类型为分段时的信息（* 仅优化类型为分段时可用）
    /// </summary>
    /// <param name="segmentList"></param>
    /// <param name="segmentStartList"></param>
    public void GetOptimizeInfo_Segment(out List<int> segmentList, out List<TIdType> segmentStartList)
    {
        segmentList = this.segmentList;
        segmentStartList = this.segmentStartList;
    }

    public T Get<T>(TIdType id, int variableOff)
    {
        var off = variableOff & filter;
        if (off >= RowLength)
        {
            Debug.LogError($"{Name} 内不存在此变量: {variableOff >> 16}列");
            return default(T);
        }
        switch (OptimizeType)
        {
            case OptimizeType.None:
            {
                if (id2RowStartOff.TryGetValue(id, out int rowStart))
                {
                    return ByteReader.Read<T>(data, rowStart + off);
                }
                break;
            }
            case OptimizeType.Continuity:
            {
                int diff = GenericCalc.SubToInt(id, firstVal);
                int diffCnt = diff / step;  // 与第一个元素相差几个元素（包含自身）
                if (diffCnt >= RowCount || ((diff % step) != 0))    // diffCnt最大值为RowCount - 1
                {
                    break;
                }
                return ByteReader.Read<T>(data, diffCnt * RowLength + off);
            }
            case OptimizeType.Segment:
            {
                for (int i = 0; i < segmentStartList.Count; i++)
                {
                    int cnt = segmentList[i];
                    int diff = GenericCalc.SubToInt(id, segmentStartList[i]);
                    if (diff >= cnt) continue; // diff最大值为cnt - 1
                    return ByteReader.Read<T>(data, segmentStartOff[i] + diff * RowLength + off);
                }
                break;
            }
            case OptimizeType.PartialContinuity:
            {
                int diff = GenericCalc.SubToInt(id, continuityStartVal);
                // 优先判断是否在连续范围内，因为至少80%概率是在连续范围内
                if (diff >= 0 && diff < continuityCnt) // 在连续范围内
                {
                    return ByteReader.Read<T>(data, continuityStartOff + diff * RowLength + variableOff);
                }
                if (id2RowStartOff.TryGetValue(id, out int rowStart))
                {
                    return ByteReader.Read<T>(data, rowStart + off);
                }
                break;
            }
        }
        Debug.LogError($"{Name} 内不存在此id: {id}");
        return default(T);
    }

    public List<T> GetOneCol<T>(int variableOff)
    {
        return GetOneCol<T>(variableOff, RowCount);
    }

    public List<T> GetOneCol<T>(int variableOff, int cnt)
    {
        var off = variableOff & filter;
        if (off >= RowLength || RowCount <= 0)
        {
            Debug.LogError($"{Name} 内不存在此变量: {variableOff >> 16}列");
            return default(List<T>);
        }
        List<T> ls = new List<T>(cnt);
        int index = off;
        for (int i = 0; i < cnt; i++)
        {
            ls.Add(ByteReader.Read<T>(data, index));
            index += RowLength;
        }
        return ls;
    }

    public Dictionary<K, V> GetDict<K, V>(TIdType id, int variableOff)
    {
        var off = variableOff & filter;
        if (off >= RowLength)
        {
            Debug.LogError($"{Name} 内不存在此变量: {variableOff >> 16}列");
            return null;
        }
        switch (OptimizeType)
        {
            case OptimizeType.None:
            {
                if (id2RowStartOff.TryGetValue(id, out int rowStart))
                {
                    return ByteReader.ReadDict<K, V>(data, rowStart + off);
                }
                break;
            }
            case OptimizeType.Continuity:
            {
                int diff = GenericCalc.SubToInt(id, firstVal);
                int diffCnt = diff / step;  // 与第一个元素相差几个元素（包含自身）
                if (diffCnt >= RowCount || ((diff % step) != 0))    // diffCnt最大值为RowCount - 1
                {
                    break;
                }
                return ByteReader.ReadDict<K, V>(data, diffCnt * RowLength + off);
            }
            case OptimizeType.Segment:
            {
                for (int i = 0; i < segmentStartList.Count; i++)
                {
                    int cnt = segmentList[i];
                    int diff = GenericCalc.SubToInt(id, segmentStartList[i]);
                    if (diff >= cnt) continue; // diff最大值为cnt - 1
                    return ByteReader.ReadDict<K, V>(data, segmentStartOff[i] + diff * RowLength + off);
                }
                break;
            }
            case OptimizeType.PartialContinuity:
            {
                int diff = GenericCalc.SubToInt(id, continuityStartVal);
                // 优先判断是否在连续范围内，因为至少80%概率是在连续范围内
                if (diff >= 0 && diff < continuityCnt) // 在连续范围内
                {
                    return ByteReader.ReadDict<K, V>(data, continuityStartOff + diff * RowLength + variableOff);
                }
                if (id2RowStartOff.TryGetValue(id, out int rowStart))
                {
                    return ByteReader.ReadDict<K, V>(data, rowStart + off);
                }
                break;
            }
        }
        Debug.LogError($"{Name} 内不存在此id: {id}");
        return null;
    }
}

public static class ExcelDataAccess
{
    public static void Init()
    {
        byte[] data = Resources.Load<TextAsset>("ByteData/manifest").bytes;
        if (data.Length > 0)
        {
            int index = 0;
            int fileCnt = ByteReader.ReadInt(data, index);
            index += 4;
            for (int i = 0; i < fileCnt; i++)
            {
                ByteFileParam param = new ByteFileParam();
                param.fileName = ByteReader.ReadString(data, index, false);
                index += param.fileName.Length;
                param.idColIndex = ByteReader.ReadInt(data, index);
                index += 4;
                param.rowCount = ByteReader.ReadInt(data, index);
                index += 4;
                param.rowLen = ByteReader.ReadInt(data, index);
                index += 4;
                param.colOff = ByteReader.ReadList<int>(data, index, false);
                index += 4 * param.colOff.Count + 2;
                param.types = ByteReader.ReadList<int>(data, index, false);
                index += 4 * param.types.Count + 2;
                param.varNames = ByteReader.ReadList<string>(data, index, false);
                index += GetListStringLen(param.varNames);
                param.cache = ByteReader.ReadBool(data, index);
                index++;
                param.optimizeType = (OptimizeType)ByteReader.ReadByte(data, index);
                index++;

                switch (param.optimizeType)
                {
                    case OptimizeType.Continuity:
                        param.step = ByteReader.ReadInt(data, index);
                        index += 4;
                        break;
                    case OptimizeType.Segment:
                        param.segmentList = ByteReader.ReadListInt(data, index, false);
                        index += 4 * param.segmentList.Count + 2;
                        break;
                    case OptimizeType.PartialContinuity:
                        param.continuityStartOff = ByteReader.ReadInt(data, index);
                        index += 4;
                        param.continuityCnt = ByteReader.ReadInt(data, index);
                        index += 4;
                        break;
                }
                object info = GetByteFileInfo(param);
                byteFilefileInfoDict.Add(i, info);
            }
        }
    }

    private static readonly Dictionary<int, object> byteFilefileInfoDict = new Dictionary<int, object>();

    private static int GetListStringLen(List<string> ls)
    {
        int len = 2;
        foreach (var s in ls)
        {
            len += (s.Length + 2);
        }
        return len;
    }

    private static object GetByteFileInfo(ByteFileParam param)
    {
        int idType = param.types[param.idColIndex];
        switch (idType)
        {
            case (int)TypeToken.Bool: return new ByteFileInfo<bool>(param);
            case (int)TypeToken.Sbyte: return new ByteFileInfo<sbyte>(param);
            case (int)TypeToken.Byte: return new ByteFileInfo<byte>(param);
            case (int)TypeToken.UShort: return new ByteFileInfo<ushort>(param);
            case (int)TypeToken.Short: return new ByteFileInfo<short>(param);
            case (int)TypeToken.UInt: return new ByteFileInfo<uint>(param);
            case (int)TypeToken.Int: return new ByteFileInfo<int>(param);
            case (int)TypeToken.Float: return new ByteFileInfo<float>(param);
            case (int)TypeToken.ULong: return new ByteFileInfo<ulong>(param);
            case (int)TypeToken.Long: return new ByteFileInfo<long>(param);
            case (int)TypeToken.Double: return new ByteFileInfo<double>(param);
            case (int)TypeToken.String: return new ByteFileInfo<string>(param);
            case (int)TypeToken.Vector + 200: return new ByteFileInfo<Vector2>(param);
            case (int)TypeToken.Vector + 300: return new ByteFileInfo<Vector3>(param);
            case (int)TypeToken.Vector + 400: return new ByteFileInfo<Vector4>(param);
            case (int)TypeToken.Vector + 200 + (int)TypeToken.Int: return new ByteFileInfo<Vector2Int>(param);
            case (int)TypeToken.Vector + 300 + (int)TypeToken.Int: return new ByteFileInfo<Vector3Int>(param);
            default: return null;
        }
    }

    public static T Get<T, IdType>(ExcelName excelName, IdType id, int variableName)
    {
        if (byteFilefileInfoDict.TryGetValue((int)excelName, out object fileInfo))
        {
            return ((ByteFileInfo<IdType>)fileInfo).Get<T>(id, variableName);
        }
        else
        {
            Debug.LogError($"{excelName} 文件不存在");
            return default(T);
        }
    }

    public static Dictionary<K, V> GetDict<K, V, IdType>(ExcelName excelName, IdType id, int variableName)
    {
        if (byteFilefileInfoDict.TryGetValue((int)excelName, out object fileInfo))
        {
            return ((ByteFileInfo<IdType>)fileInfo).GetDict<K, V>(id, variableName);
        }
        else
        {
            Debug.LogError($"{excelName} 文件不存在");
            return null;
        }
    }
}

public enum OptimizeType : byte
{
    None,
    /// <summary>
    /// 数据为等差数列形式，步长为固定值
    /// </summary>
    Continuity,
    /// <summary>
    /// 数据分为多段，每段内都是连续的，步长为1
    /// </summary>
    Segment,
    /// <summary>
    /// 连续部分为一段，占所有数据的80%以上
    /// </summary>
    PartialContinuity
}