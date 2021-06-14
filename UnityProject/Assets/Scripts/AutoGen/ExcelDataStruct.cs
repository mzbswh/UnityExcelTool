using System.Collections.Generic;
using UnityEngine;

public struct EDS_Test
{
    int primaryColVal;
    public EDS_Test(int val) { this.primaryColVal = val; }
    /// <summary></summary>
    public int id { get { return ExcelDataAccess.Get<int, int>(ExcelName.Test, primaryColVal, 0); } }
    /// <summary></summary>
    public List<int> lsi { get { return ExcelDataAccess.GetList<int, int>(ExcelName.Test, primaryColVal, 65540); } }
    /// <summary></summary>
    public List<string> lss { get { return ExcelDataAccess.GetList<string, int>(ExcelName.Test, primaryColVal, 131080); } }
    /// <summary></summary>
    public Vector2 v2 { get { return ExcelDataAccess.GetVector2<int>(ExcelName.Test, primaryColVal, 196620); } }
    /// <summary></summary>
    public Vector3Int v3i { get { return ExcelDataAccess.GetVector3Int<int>(ExcelName.Test, primaryColVal, 262164); } }
    /// <summary></summary>
    public Vector4 v4 { get { return ExcelDataAccess.GetVector4<int>(ExcelName.Test, primaryColVal, 327712); } }
    /// <summary></summary>
    public Dictionary<int, int> di { get { return ExcelDataAccess.GetDict<int, int, int>(ExcelName.Test, primaryColVal, 393264); } }
    /// <summary></summary>
    public Dictionary<string, string> ds { get { return ExcelDataAccess.GetDict<string, string, int>(ExcelName.Test, primaryColVal, 458804); } }
    /// <summary></summary>
    public bool boolType { get { return ExcelDataAccess.Get<bool, int>(ExcelName.Test, primaryColVal, 524344); } }
    /// <summary></summary>
    public sbyte sbyteType { get { return ExcelDataAccess.Get<sbyte, int>(ExcelName.Test, primaryColVal, 589881); } }
    /// <summary></summary>
    public byte byteType { get { return ExcelDataAccess.Get<byte, int>(ExcelName.Test, primaryColVal, 655418); } }
    /// <summary></summary>
    public ushort ushortType { get { return ExcelDataAccess.Get<ushort, int>(ExcelName.Test, primaryColVal, 720955); } }
    /// <summary></summary>
    public short shortType { get { return ExcelDataAccess.Get<short, int>(ExcelName.Test, primaryColVal, 786493); } }
    /// <summary></summary>
    public uint uintType { get { return ExcelDataAccess.Get<uint, int>(ExcelName.Test, primaryColVal, 852031); } }
    /// <summary></summary>
    public int intType { get { return ExcelDataAccess.Get<int, int>(ExcelName.Test, primaryColVal, 917571); } }
    /// <summary></summary>
    public ulong ulongTYpe { get { return ExcelDataAccess.Get<ulong, int>(ExcelName.Test, primaryColVal, 983111); } }
    /// <summary></summary>
    public long longTYpe { get { return ExcelDataAccess.Get<long, int>(ExcelName.Test, primaryColVal, 1048655); } }
    /// <summary></summary>
    public float floatType { get { return ExcelDataAccess.Get<float, int>(ExcelName.Test, primaryColVal, 1114199); } }
    /// <summary></summary>
    public string StrType { get { return ExcelDataAccess.Get<string, int>(ExcelName.Test, primaryColVal, 1179739); } }
    /// <summary></summary>
    public double douType { get { return ExcelDataAccess.Get<double, int>(ExcelName.Test, primaryColVal, 1245279); } }
}
