using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ExcelDataCache
{
    sealed class FileInfoData
    {
        bool hasParsed = false;
        byte[] data;
        
        public string FileName { get; }
        public int RowCount { get; }
        public int RowLength { get; }
        public readonly List<int> colOffset;
        public readonly List<int> typeToken;
        public readonly List<string> variableNames;
        public readonly List<string> comments;

        public FileInfoData(string fileName, int rowCount, int rowLen, List<int> colOff, List<int> types)
        {
            this.FileName = fileName;
            this.RowCount = rowCount;
            this.RowLength = rowLen;
            this.colOffset = colOff;
            this.typeToken = types;

            Parse();
        }

        Dictionary<int, int> Id2RowIndex { get; } = new Dictionary<int, int>();

        private void Parse()
        {
            if (hasParsed) return;
            data = Resources.Load<TextAsset>("ByteData/" + FileName).bytes;
            if (data.Length > 0)
            {
                for (int i = 0; i < RowCount; i++)
                {
                    int id = ByteFileParseTool.ReadInt(data, i * RowLength);
                    Id2RowIndex.Add(id, i);
                }
            }
        }

        public T Get<T>(int id, int variableName)
        {
            if (Id2RowIndex.TryGetValue(id, out int rowIndex))
            {
                if (variableName >= colOffset.Count)
                {
                    Debug.LogError($"{FileName} 内不存在此变量: {id}列");
                    return default(T);
                }
                int rowStart = rowIndex * RowLength;
                int index = rowStart + colOffset[variableName];
                return ByteFileParseTool.ReadHelper<T>.Read(data, index);
            }
            else
            {
                Debug.LogError($"{FileName} 内不存在此id: {id}");
                return default(T);
            }
        }

        public List<T> GetList<T>(int id, int variableName)
        {
            if (Id2RowIndex.TryGetValue(id, out int rowIndex))
            {
                if (variableName >= colOffset.Count)
                {
                    Debug.LogError($"{FileName} 内不存在此变量: {id}列");
                    return null; ;
                }
                int rowStart = rowIndex * RowLength;
                int index = rowStart + colOffset[variableName];
                return ByteFileParseTool.ReadHelper<T>.ReadList(data, index);
            }
            else
            {
                Debug.LogError($"{FileName} 内不存在此id: {id}");
                return null;
            }
        }

        public Dictionary<K, V> GetDict<K, V>(int id, int variableName)
        {
            if (Id2RowIndex.TryGetValue(id, out int rowIndex))
            {
                if (variableName >= colOffset.Count)
                {
                    Debug.LogError($"{FileName} 内不存在此变量: {id}列");
                    return null; ;
                }
                int rowStart = rowIndex * RowLength;
                int index = rowStart + colOffset[variableName];
                return ByteFileParseTool.ReadDict<K, V>(data, index);
            }
            else
            {
                Debug.LogError($"{FileName} 内不存在此id: {id}");
                return null;
            }
        }
    }

    private readonly Dictionary<int, FileInfoData> fileInfoDict = new Dictionary<int, FileInfoData>();

    public void ReadManifest()
    {
        byte[] data = Resources.Load<TextAsset>("ByteData/manifest").bytes;
        if (data.Length > 0)
        {
            int index = 0;
            int count = ByteFileParseTool.ReadInt(data, index);
            index += 4;
            for (int i = 0; i < count; i++)
            {
                string fileName = ByteFileParseTool.ReadString(data, index);
                index += 4;
                int rowCount = ByteFileParseTool.ReadInt(data, index);
                index += 4;
                int rowLength = ByteFileParseTool.ReadInt(data, index);
                index += 4;
                List<int> colOff = ByteFileParseTool.ReadList<int>(data, index);
                index += 4;
                List<int> typeToken = ByteFileParseTool.ReadList<int>(data, index);
                index += 4;

                FileInfoData info = new FileInfoData(fileName, rowCount, rowLength, colOff, typeToken);
                fileInfoDict.Add(i, info);
            }
        }
    }

    public T Get<T>(ExcelName excelName, int id, int variableName)
    {
        if (fileInfoDict.TryGetValue((int)excelName, out FileInfoData fileInfo))
        {
            return fileInfo.Get<T>(id, variableName);
        }
        else
        {
            Debug.LogError($"{excelName} 文件不存在");
            return default(T);
        }
    }

    public List<T> GetList<T>(ExcelName excelName, int id, int variableName)
    {
        if (fileInfoDict.TryGetValue((int)excelName, out FileInfoData fileInfo))
        {
            return fileInfo.GetList<T>(id, variableName);
        }
        else
        {
            Debug.LogError($"{excelName} 文件不存在");
            return null;
        }
    }

    public Dictionary<K, V> GetDict<K, V>(ExcelName excelName, int id, int variableName)
    {
        if (fileInfoDict.TryGetValue((int)excelName, out FileInfoData fileInfo))
        {
            return fileInfo.GetDict<K, V>(id, variableName);
        }
        else
        {
            Debug.LogError($"{excelName} 文件不存在");
            return null;
        }
    }
}
