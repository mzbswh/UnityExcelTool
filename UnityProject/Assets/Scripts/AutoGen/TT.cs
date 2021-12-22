using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

#pragma warning disable

public class DataCache
{
    public int a { get; }
    public int b { get; }
    public int c { get; }
    public string d { get; }
    public List<int> e { get; }

    public DataCache(int id, int row)
    {
        this.a = byteFileInfo.GetByRowAndIndex<int>(row, 0);
        this.b = id;
        this.c = byteFileInfo.GetByRowAndIndex<int>(id, 1);
        this.d = byteFileInfo.GetByRowAndIndex<string>(id, 2);
        this.e = byteFileInfo.GetByRowAndIndex<List<int>>(id, 3);
    }

    static bool cached = false;
    static Dictionary<int, DataCache> cacheDict = new Dictionary<int, DataCache>();
    static ByteFileInfo<int> byteFileInfo;

    public static void CacheData()
    {
        if (cached) return;
        if (byteFileInfo == null)
        {
            byteFileInfo = ExcelDataMgr.GetByteFileInfo<int>(ExcelName.Test);
        }
        if (!byteFileInfo.ByteDataLoaded) byteFileInfo.LoadByteData();
        for (int i = 0; i < byteFileInfo.RowCount; i++)
        {
            int id = byteFileInfo.GetKey(i);
            DataCache cache = new DataCache(id, i);
            cacheDict.Add(id, cache);
        }
    }

    public static DataCache Get(int id)
    {
        if (cacheDict.TryGetValue(id, out var cache)) return cache;
        Debug.LogError($"{typeof(DataCache).Name}不存在主列值{id.ToString()}");
        return null;
    }
}

public class ExcelDataCacheMgr
{
    static void CacheData()
    {
        DataCache.CacheData();
        // 其它缓存
    }
}