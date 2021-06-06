using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExcelDataAccess
{
    private static ExcelDataCache dataSource;

    public static void Init()
    {
        ByteFileParseTool.Init();
        dataSource = new ExcelDataCache();
        dataSource.ReadManifest();
    }

    /// <param name="variableName">变量名称：值为此变量在这一列的的字节偏移</param>
    public static T Get<T, IdType>(ExcelName excelName, IdType id, int variableName)
    {
        return dataSource.Get<T, IdType>(excelName, id, variableName);
    }

    public static List<T> GetList<T, IdType>(ExcelName excelName, IdType id, int variableName)
    {
        return dataSource.GetList<T, IdType>(excelName, id, variableName);
    }

    public static Dictionary<K, V> GetDict<K, V, IdType>(ExcelName excelName, IdType id, int variableName)
    {
        return dataSource.GetDict<K ,V, IdType>(excelName, id, variableName);
    }

    public static Vector2 GetVector2<IdType>(ExcelName excelName, IdType id, int variableName)
    {
        return dataSource.GetVector2<IdType>(excelName, id, variableName);
    }
    public static Vector2Int GetVector2Int<IdType>(ExcelName excelName, IdType id, int variableName)
    {
        return dataSource.GetVector2Int<IdType>(excelName, id, variableName);
    }
    public static Vector3 GetVector3<IdType>(ExcelName excelName, IdType id, int variableName)
    {
        return dataSource.GetVector3<IdType>(excelName, id, variableName);
    }
    public static Vector3Int GetVector3Int<IdType>(ExcelName excelName, IdType id, int variableName)
    {
        return dataSource.GetVector3Int<IdType>(excelName, id, variableName);
    }
    public static Vector4 GetVector4<IdType>(ExcelName excelName, IdType id, int variableName)
    {
        return dataSource.GetVector4<IdType>(excelName, id, variableName);
    }
}
