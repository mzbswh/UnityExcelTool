using System.Collections.Generic;
using UnityEngine;
using ExcelTool;

#pragma warning disable

public class EDC_Test_Sheet1
{
    public static Dictionary<string ,string>[] Ids => byteFileInfo.Ids;
    static bool cached = false;
    static ByteFileInfo<Dictionary<string ,string>> byteFileInfo;
    static Dictionary<Dictionary<string ,string>, EDC_Test_Sheet1> cacheDict = new Dictionary<Dictionary<string ,string>, EDC_Test_Sheet1>();

    /// <summary></summary>
    public int id { get; }
    /// <summary></summary>
    public List<int> lsi { get; }
    /// <summary></summary>
    public List<string> lss { get; }
    /// <summary></summary>
    public Vector2 v2 { get; }
    /// <summary></summary>
    public Vector3Int v3i { get; }
    /// <summary></summary>
    public Vector4 v4 { get; }
    /// <summary></summary>
    public Dictionary<int ,int> di { get; }
    /// <summary></summary>
    public Dictionary<string ,string> ds { get; }
    /// <summary></summary>
    public bool boolType { get; }
    /// <summary></summary>
    public sbyte sbyteType { get; }
    /// <summary></summary>
    public byte byteType { get; }
    /// <summary></summary>
    public ushort ushortType { get; }
    /// <summary></summary>
    public short shortType { get; }
    /// <summary></summary>
    public uint uintType { get; }
    /// <summary></summary>
    public int intType { get; }
    /// <summary></summary>
    public ulong ulongTYpe { get; }
    /// <summary></summary>
    public long longTYpe { get; }
    /// <summary></summary>
    public float floatType { get; }
    /// <summary></summary>
    public string StrType { get; }
    /// <summary></summary>
    public double douType { get; }

    public EDC_Test_Sheet1(Dictionary<string ,string> id)
    {
        this.id = ByteFileReader.Get<int>();
        this.lsi = ByteFileReader.Get<List<int>>();
        this.lss = ByteFileReader.Get<List<string>>();
        this.v2 = ByteFileReader.Get<Vector2>();
        this.v3i = ByteFileReader.Get<Vector3Int>();
        this.v4 = ByteFileReader.Get<Vector4>();
        this.di = ByteFileReader.GetDict<int, int>();
        this.ds = id;
        ByteFileReader.SkipOne();
        this.boolType = ByteFileReader.Get<bool>();
        this.sbyteType = ByteFileReader.Get<sbyte>();
        this.byteType = ByteFileReader.Get<byte>();
        this.ushortType = ByteFileReader.Get<ushort>();
        this.shortType = ByteFileReader.Get<short>();
        this.uintType = ByteFileReader.Get<uint>();
        this.intType = ByteFileReader.Get<int>();
        this.ulongTYpe = ByteFileReader.Get<ulong>();
        this.longTYpe = ByteFileReader.Get<long>();
        this.floatType = ByteFileReader.Get<float>();
        this.StrType = ByteFileReader.Get<string>();
        this.douType = ByteFileReader.Get<double>();

    }

    public static void CacheData()
    {
        if (cached) return;
        if (byteFileInfo == null)
        {
            byteFileInfo = ExcelDataMgr.GetByteFileInfo<Dictionary<string ,string>>((short)ExcelName.Test_Sheet1);
        }
        if (!byteFileInfo.ByteDataLoaded) byteFileInfo.LoadByteData();
        byteFileInfo.ResetByteFileReader();
        for (int i = 0; i < byteFileInfo.RowCount; i++)
        {
            Dictionary<string ,string> id = byteFileInfo.GetKey(i);
            EDC_Test_Sheet1 cache = new EDC_Test_Sheet1(id);
            cacheDict.Add(id, cache);
        }
    }

    public static EDC_Test_Sheet1 Get(Dictionary<string ,string> id)
    {
        if (cacheDict.TryGetValue(id, out var cache)) return cache;
        Debug.LogError($"{typeof(EDC_Test_Sheet1).Name}不存在主列值{id.ToString()}");
        return null;
    }
}
