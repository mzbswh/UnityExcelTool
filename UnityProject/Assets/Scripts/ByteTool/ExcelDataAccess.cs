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
}

public sealed class ByteFileInfo<TIdType>
{
    bool parsed = false;
    byte[] data;

    const int filter = 0xffff;

    public string Name { get; }
    public int IdColIndex { get; }
    public int RowCount { get; }
    public int RowLength { get; }
    public int ColCount => typeToken.Count;
    public string IdColName => varNames[IdColIndex];
    public TIdType[] Ids { get; private set; }
    private readonly List<int> typeToken;
    private readonly List<int> colOff;
    private readonly List<string> varNames;

    public ByteFileInfo(ByteFileParam param)
    {
        this.Name = param.fileName;
        this.IdColIndex = param.idColIndex;
        this.RowCount = param.rowCount;
        this.RowLength = param.rowLen;
        this.typeToken = param.types;
        this.colOff = param.colOff;
        this.varNames = param.varNames;

        Parse();
    }

    Dictionary<TIdType, int> Id2RowStartOff { get; } = new Dictionary<TIdType, int>();

    public void Parse()
    {
        if (parsed) return;
        parsed = true;
        data = Resources.Load<TextAsset>("ByteData/" + Name).bytes;
        if (data.Length > 0)
        {
            int idColOff = colOff[IdColIndex];
            Ids = new TIdType[RowCount];
            for (int i = 0; i < RowCount; i++)
            {
                TIdType id = ByteReader.Read<TIdType>(data, i * RowLength + idColOff);
                Ids[i] = id;
                Id2RowStartOff.Add(id, i * RowLength);
            }
        }
    }

    /// <summary>
    /// 获取变量在这一行的偏移字节数
    /// </summary>
    /// <param name="var">index << 16 + off</param>
    /// <returns></returns>
    private int GetOff(int var)
    {
        return var & filter;
    }

    /// <summary>
    /// 获取变量在一行的索引，即第几个
    /// </summary>
    /// <param name="var">index << 16 + off</param>
    private int GetIndex(int var)
    {
        return var >> 16;
    }

    public T Get<T>(TIdType id, int variableOff)
    {
        if (Id2RowStartOff.TryGetValue(id, out int rowStart))
        {
            variableOff &= filter;
            if (variableOff >= RowLength)
            {
                Debug.LogError($"{Name} 内不存在此变量: {variableOff >> 16}列");
                return default(T);
            }
            int index = rowStart + variableOff;
            return ByteReader.Read<T>(data, index);
        }
        else
        {
            Debug.LogError($"{Name} 内不存在此id: {id}");
            return default(T);
        }
    }

    public List<T> GetOneCol<T>(int variableOff)
    {
        variableOff &= filter;
        if (variableOff >= RowLength || RowCount <= 0)
        {
            Debug.LogError($"{Name} 内不存在此变量: {variableOff >> 16}列");
            return default(List<T>);
        }
        List<T> ls = new List<T>();
        int index = variableOff;
        for (int i = 0; i < RowCount; i++)
        {
            ls.Add(ByteReader.Read<T>(data, index));
            index += RowLength;
        }
        return ls;
    }

    public List<T> GetList<T>(TIdType id, int variableOff)
    {
        if (Id2RowStartOff.TryGetValue(id, out int rowStart))
        {
            variableOff &= filter;
            if (variableOff >= RowLength)
            {
                Debug.LogError($"{Name} 内不存在此变量: {variableOff >> 16}列");
                return null;
            }
            int index = rowStart + variableOff;
            return ByteReader.Read<List<T>>(data, index);
        }
        else
        {
            Debug.LogError($"{Name} 内不存在此id: {id}");
            return null;
        }
    }

    public Dictionary<K, V> GetDict<K, V>(TIdType id, int variableOff)
    {
        if (Id2RowStartOff.TryGetValue(id, out int rowStart))
        {
            variableOff &= filter;
            if (variableOff >= RowLength)
            {
                Debug.LogError($"{Name} 内不存在此变量: {variableOff >> 16}列");
                return null; ;
            }
            int index = rowStart + variableOff;
            return ByteReader.ReadDict<K, V>(data, index);
        }
        else
        {
            Debug.LogError($"{Name} 内不存在此id: {id}");
            return null;
        }
    }

    public Vector2 GetVector2(TIdType id, int variableOff)
    {
        if (Id2RowStartOff.TryGetValue(id, out int rowStart))
        {
            variableOff &= filter;
            if (variableOff >= RowLength)
            {
                Debug.LogError($"{Name} 内不存在此变量: {variableOff >> 16}列");
                return new Vector2();
            }
            int index = rowStart + variableOff;
            return ByteReader.ReadVector2(data, index);
        }
        else
        {
            Debug.LogError($"{Name} 内不存在此id: {id}");
            return new Vector2();
        }
    }
    public Vector2Int GetVector2Int(TIdType id, int variableOff)
    {
        if (Id2RowStartOff.TryGetValue(id, out int rowStart))
        {
            variableOff &= filter;
            if (variableOff >= RowLength)
            {
                Debug.LogError($"{Name} 内不存在此变量: {variableOff >> 16}列");
                return new Vector2Int();
            }
            int index = rowStart + variableOff;
            return ByteReader.ReadVector2Int(data, index);
        }
        else
        {
            Debug.LogError($"{Name} 内不存在此id: {id}");
            return new Vector2Int();
        }
    }
    public Vector3 GetVector3(TIdType id, int variableOff)
    {
        if (Id2RowStartOff.TryGetValue(id, out int rowStart))
        {
            variableOff &= filter;
            if (variableOff >= RowLength)
            {
                Debug.LogError($"{Name} 内不存在此变量: {variableOff >> 16}列");
                return new Vector3();
            }
            int index = rowStart + variableOff;
            return ByteReader.ReadVector3(data, index);
        }
        else
        {
            Debug.LogError($"{Name} 内不存在此id: {id}");
            return new Vector3();
        }
    }
    public Vector3Int GetVector3Int(TIdType id, int variableOff)
    {
        if (Id2RowStartOff.TryGetValue(id, out int rowStart))
        {
            variableOff &= filter;
            if (variableOff >= RowLength)
            {
                Debug.LogError($"{Name} 内不存在此变量: {variableOff >> 16}列");
                return new Vector3Int();
            }
            int index = rowStart + variableOff;
            return ByteReader.ReadVector3Int(data, index);
        }
        else
        {
            Debug.LogError($"{Name} 内不存在此id: {id}");
            return new Vector3Int();
        }
    }
    public Vector4 GetVector4(TIdType id, int variableOff)
    {
        if (Id2RowStartOff.TryGetValue(id, out int rowStart))
        {
            variableOff &= filter;
            if (variableOff >= RowLength)
            {
                Debug.LogError($"{Name} 内不存在此变量: {variableOff >> 16}列");
                return new Vector4();
            }
            int index = rowStart + variableOff;
            return ByteReader.ReadVector4(data, index);
        }
        else
        {
            Debug.LogError($"{Name} 内不存在此id: {id}");
            return new Vector4();
        }
    }
}

public static class ExcelDataAccess
{
    public static void Init()
    {
        ReadManifest();
    }

    private static readonly Dictionary<int, object> byteFilefileInfoDict = new Dictionary<int, object>();

    public static void ReadManifest()
    {
        byte[] data = Resources.Load<TextAsset>("ByteData/manifest").bytes;
        if (data.Length > 0)
        {
            int index = 0;
            int count = ByteReader.ReadInt(data, index);
            index += 4;
            for (int i = 0; i < count; i++)
            {
                ByteFileParam param = new ByteFileParam();
                param.fileName = ByteReader.ReadString(data, index, false);
                index += 4;
                param.idColIndex = ByteReader.ReadInt(data, index);
                index += 4;
                param.rowCount = ByteReader.ReadInt(data, index);
                index += 4;
                param.rowLen = ByteReader.ReadInt(data, index);
                index += 4;
                param.types = ByteReader.ReadList<int>(data, index, false);
                index += 4;
                param.colOff = ByteReader.ReadList<int>(data, index, false);
                index += 4;
                param.varNames = ByteReader.ReadList<string>(data, index, false);
                index += 4;
                object info = GetByteFileInfo(param);
                byteFilefileInfoDict.Add(i, info);
            }
        }
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

    public static List<T> GetList<T, IdType>(ExcelName excelName, IdType id, int variableName)
    {
        if (byteFilefileInfoDict.TryGetValue((int)excelName, out object fileInfo))
        {
            return ((ByteFileInfo<IdType>)fileInfo).GetList<T>(id, variableName);
        }
        else
        {
            Debug.LogError($"{excelName} 文件不存在");
            return null;
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

    public static Vector2 GetVector2<IdType>(ExcelName excelName, IdType id, int variableName)
    {
        if (byteFilefileInfoDict.TryGetValue((int)excelName, out object fileInfo))
        {
            return ((ByteFileInfo<IdType>)fileInfo).GetVector2(id, variableName);
        }
        else
        {
            Debug.LogError($"{excelName} 文件不存在");
            return new Vector2();
        }
    }
    public static Vector2Int GetVector2Int<IdType>(ExcelName excelName, IdType id, int variableName)
    {
        if (byteFilefileInfoDict.TryGetValue((int)excelName, out object fileInfo))
        {
            return ((ByteFileInfo<IdType>)fileInfo).GetVector2Int(id, variableName);
        }
        else
        {
            Debug.LogError($"{excelName} 文件不存在");
            return new Vector2Int();
        }
    }
    public static Vector3 GetVector3<IdType>(ExcelName excelName, IdType id, int variableName)
    {
        if (byteFilefileInfoDict.TryGetValue((int)excelName, out object fileInfo))
        {
            return ((ByteFileInfo<IdType>)fileInfo).GetVector3(id, variableName);
        }
        else
        {
            Debug.LogError($"{excelName} 文件不存在");
            return new Vector3();
        }
    }
    public static Vector3Int GetVector3Int<IdType>(ExcelName excelName, IdType id, int variableName)
    {
        if (byteFilefileInfoDict.TryGetValue((int)excelName, out object fileInfo))
        {
            return ((ByteFileInfo<IdType>)fileInfo).GetVector3Int(id, variableName);
        }
        else
        {
            Debug.LogError($"{excelName} 文件不存在");
            return new Vector3Int();
        }
    }
    public static Vector4 GetVector4<IdType>(ExcelName excelName, IdType id, int variableName)
    {
        if (byteFilefileInfoDict.TryGetValue((int)excelName, out object fileInfo))
        {
            return ((ByteFileInfo<IdType>)fileInfo).GetVector4(id, variableName);
        }
        else
        {
            Debug.LogError($"{excelName} 文件不存在");
            return new Vector4();
        }
    }
}
