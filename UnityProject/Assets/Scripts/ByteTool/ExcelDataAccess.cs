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

    /// <param name="variableName">变量名称：对应表格的第几列（从0开始）</param>
    public static T Get<T>(ExcelName excelName, int id, int variableName)
    {
        return dataSource.Get<T>(excelName, id, variableName);
    }

    public static List<T> GetList<T>(ExcelName excelName, int id, int variableName)
    {
        return dataSource.GetList<T>(excelName, id, variableName);
    }

    public static Dictionary<K, V> GetDict<K, V>(ExcelName excelName, int id, int variableName)
    {
        return dataSource.GetDict<K ,V>(excelName, id, variableName);
    }
}
