using System.Collections.Generic;
using UnityEngine;
using ExcelTool;

#pragma warning disable

public struct ESD_Test_s_ttt
{
    List<int> primaryColVal;
    readonly ByteFileInfo<List<int>> byteFileInfo;
    public ESD_Test_s_ttt(List<int> val)
    {
        this.primaryColVal = val;
        this.byteFileInfo = ExcelDataMgr.GetByteFileInfo<List<int>>((short)ExcelName.Test_s_ttt);
    }
    public void SetPrimary(List<int> id) { this.primaryColVal = id; } 
    /// <summary></summary>
    public int id => byteFileInfo.Get<int>(primaryColVal, 0);
    /// <summary></summary>
    public List<int> lsi => byteFileInfo.Get<List<int>>(primaryColVal, 65540);
    /// <summary></summary>
    public List<string> lss => byteFileInfo.Get<List<string>>(primaryColVal, 131080);
    /// <summary></summary>
    public Vector2 v2 => byteFileInfo.Get<Vector2>(primaryColVal, 196620);
    /// <summary></summary>
    public Vector3Int v3i => byteFileInfo.Get<Vector3Int>(primaryColVal, 262164);
    /// <summary></summary>
    public Vector4 v4 => byteFileInfo.Get<Vector4>(primaryColVal, 327712);
    /// <summary></summary>
    public Dictionary<int, int> di => byteFileInfo.GetDict<int, int>(primaryColVal, 393264);
    /// <summary></summary>
    public Dictionary<string, string> ds => byteFileInfo.GetDict<string, string>(primaryColVal, 458804);
    /// <summary></summary>
    public bool boolType => byteFileInfo.Get<bool>(primaryColVal, 524344);
    /// <summary></summary>
    public sbyte sbyteType => byteFileInfo.Get<sbyte>(primaryColVal, 589881);
    /// <summary></summary>
    public byte byteType => byteFileInfo.Get<byte>(primaryColVal, 655418);
    /// <summary></summary>
    public ushort ushortType => byteFileInfo.Get<ushort>(primaryColVal, 720955);
    /// <summary></summary>
    public short shortType => byteFileInfo.Get<short>(primaryColVal, 786493);
    /// <summary></summary>
    public uint uintType => byteFileInfo.Get<uint>(primaryColVal, 852031);
    /// <summary></summary>
    public int intType => byteFileInfo.Get<int>(primaryColVal, 917571);
    /// <summary></summary>
    public ulong ulongTYpe => byteFileInfo.Get<ulong>(primaryColVal, 983111);
    /// <summary></summary>
    public long longTYpe => byteFileInfo.Get<long>(primaryColVal, 1048655);
    /// <summary></summary>
    public float floatType => byteFileInfo.Get<float>(primaryColVal, 1114199);
    /// <summary></summary>
    public string StrType => byteFileInfo.Get<string>(primaryColVal, 1179739);
    /// <summary></summary>
    public double douType => byteFileInfo.Get<double>(primaryColVal, 1245279);
}
public struct ESD_Test_Sheet1
{
    Dictionary<string ,string> primaryColVal;
    readonly ByteFileInfo<Dictionary<string ,string>> byteFileInfo;
    public ESD_Test_Sheet1(Dictionary<string ,string> val)
    {
        this.primaryColVal = val;
        this.byteFileInfo = ExcelDataMgr.GetByteFileInfo<Dictionary<string ,string>>((short)ExcelName.Test_Sheet1);
    }
    public void SetPrimary(Dictionary<string ,string> id) { this.primaryColVal = id; } 
    /// <summary></summary>
    public int id => byteFileInfo.Get<int>(primaryColVal, 0);
    /// <summary></summary>
    public List<int> lsi => byteFileInfo.Get<List<int>>(primaryColVal, 65540);
    /// <summary></summary>
    public List<string> lss => byteFileInfo.Get<List<string>>(primaryColVal, 131080);
    /// <summary></summary>
    public Vector2 v2 => byteFileInfo.Get<Vector2>(primaryColVal, 196620);
    /// <summary></summary>
    public Vector3Int v3i => byteFileInfo.Get<Vector3Int>(primaryColVal, 262164);
    /// <summary></summary>
    public Vector4 v4 => byteFileInfo.Get<Vector4>(primaryColVal, 327712);
    /// <summary></summary>
    public Dictionary<int, int> di => byteFileInfo.GetDict<int, int>(primaryColVal, 393264);
    /// <summary></summary>
    public Dictionary<string, string> ds => byteFileInfo.GetDict<string, string>(primaryColVal, 458804);
    /// <summary></summary>
    public bool boolType => byteFileInfo.Get<bool>(primaryColVal, 524344);
    /// <summary></summary>
    public sbyte sbyteType => byteFileInfo.Get<sbyte>(primaryColVal, 589881);
    /// <summary></summary>
    public byte byteType => byteFileInfo.Get<byte>(primaryColVal, 655418);
    /// <summary></summary>
    public ushort ushortType => byteFileInfo.Get<ushort>(primaryColVal, 720955);
    /// <summary></summary>
    public short shortType => byteFileInfo.Get<short>(primaryColVal, 786493);
    /// <summary></summary>
    public uint uintType => byteFileInfo.Get<uint>(primaryColVal, 852031);
    /// <summary></summary>
    public int intType => byteFileInfo.Get<int>(primaryColVal, 917571);
    /// <summary></summary>
    public ulong ulongTYpe => byteFileInfo.Get<ulong>(primaryColVal, 983111);
    /// <summary></summary>
    public long longTYpe => byteFileInfo.Get<long>(primaryColVal, 1048655);
    /// <summary></summary>
    public float floatType => byteFileInfo.Get<float>(primaryColVal, 1114199);
    /// <summary></summary>
    public string StrType => byteFileInfo.Get<string>(primaryColVal, 1179739);
    /// <summary></summary>
    public double douType => byteFileInfo.Get<double>(primaryColVal, 1245279);
}
