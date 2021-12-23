using System;
using System.Collections;
using System.Collections.Generic;
using ExcelToByteFile;
using UnityEngine;

namespace ExcelTool
{
    public static class ExcelDataMgr
    {
        public static void Init()
        {
            byte[] data = Resources.Load<TextAsset>("ByteData/manifest").bytes;
            if (data.Length > 0)
            {
                int index = 0;
                short fileCnt = ByteReader.ReadShort(data, index);
                index += 2;
                for (short i = 0; i < fileCnt; i++)
                {
                    ByteFileParam param = new ByteFileParam();
                    param.fileName = ByteReader.ReadString(data, index, false);
                    index += param.fileName.Length + 2;
                    param.idColIndex = ByteReader.ReadInt(data, index);
                    index += 4;
                    param.rowCount = ByteReader.ReadInt(data, index);
                    index += 4;
                    param.rowLen = ByteReader.ReadInt(data, index);
                    index += 4;
                    param.colOff = ByteReader.ReadListInt(data, index, false);
                    index += 4 * param.colOff.Count + 2;
                    param.types = ByteReader.ReadListInt(data, index, false);
                    index += 4 * param.types.Count + 2;
                    param.varNames = ByteReader.ReadListString(data, index, false);
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
                    param.extraInfo = ByteReader.ReadDict<string, string>(data, index, false);
                    index += GetDictStringLen(param.extraInfo);

                    object info = GetByteFileInfo(param);
                    byteFilefileInfoDict.Add(i, info);
                }
            }
        }

        private static readonly Dictionary<short, object> byteFilefileInfoDict = new Dictionary<short, object>();

        private static int GetListStringLen(List<string> ls)
        {
            int len = 2;
            foreach (var s in ls)
            {
                len += (s.Length + 2);
            }
            return len;
        }

        private static int GetDictStringLen(Dictionary<string, string> dict)
        {
            int len = 2;
            foreach (var s in dict)
            {
                len += (s.Key.Length + s.Value.Length + 4);
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

        public static ByteFileInfo<IdType> GetByteFileInfo<IdType>(short excel)
        {
            if (byteFilefileInfoDict.TryGetValue(excel, out var ret))
            {
                return (ByteFileInfo<IdType>)ret;
            }
            Debug.LogError($"未查找到excelId={excel}对应的ByteFileInfo信息");
            return null;
        }

        public static T Get<T, IdType>(short excel, IdType id, int variableName)
        {
            if (byteFilefileInfoDict.TryGetValue(excel, out object fileInfo))
            {
                return ((ByteFileInfo<IdType>)fileInfo).Get<T>(id, variableName);
            }
            else
            {
                Debug.LogError($"excelId={excel} 文件不存在");
                return default(T);
            }
        }

        public static Dictionary<K, V> GetDict<K, V, IdType>(short excel, IdType id, int variableName)
        {
            if (byteFilefileInfoDict.TryGetValue(excel, out object fileInfo))
            {
                return ((ByteFileInfo<IdType>)fileInfo).GetDict<K, V>(id, variableName);
            }
            else
            {
                Debug.LogError($"excelId={excel} 文件不存在");
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
}