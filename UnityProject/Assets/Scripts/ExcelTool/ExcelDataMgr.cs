using System;
using System.Collections;
using System.Collections.Generic;
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
                // Vector Vector + Dimension * 100 + Type
                case (int)TypeToken.Vector + 200: return new ByteFileInfo<Vector2>(param);
                case (int)TypeToken.Vector + 300: return new ByteFileInfo<Vector3>(param);
                case (int)TypeToken.Vector + 400: return new ByteFileInfo<Vector4>(param);
                case (int)TypeToken.Vector + 200 + (int)TypeToken.Int: return new ByteFileInfo<Vector2Int>(param);
                case (int)TypeToken.Vector + 300 + (int)TypeToken.Int: return new ByteFileInfo<Vector3Int>(param);
                // List
                case (int)TypeToken.List + (int)TypeToken.Sbyte: return new ByteFileInfo<List<sbyte>>(param);
                case (int)TypeToken.List + (int)TypeToken.Byte: return new ByteFileInfo<List<byte>>(param);
                case (int)TypeToken.List + (int)TypeToken.Bool: return new ByteFileInfo<List<bool>>(param);
                case (int)TypeToken.List + (int)TypeToken.UShort: return new ByteFileInfo<List<ushort>>(param);
                case (int)TypeToken.List + (int)TypeToken.Short: return new ByteFileInfo<List<short>>(param);
                case (int)TypeToken.List + (int)TypeToken.UInt: return new ByteFileInfo<List<uint>>(param);
                case (int)TypeToken.List + (int)TypeToken.Int: return new ByteFileInfo<List<int>>(param);
                case (int)TypeToken.List + (int)TypeToken.Float: return new ByteFileInfo<List<float>>(param);
                case (int)TypeToken.List + (int)TypeToken.Double: return new ByteFileInfo<List<double>>(param);
                case (int)TypeToken.List + (int)TypeToken.ULong: return new ByteFileInfo<List<ulong>>(param);
                case (int)TypeToken.List + (int)TypeToken.Long: return new ByteFileInfo<List<long>>(param);
                case (int)TypeToken.List + (int)TypeToken.String: return new ByteFileInfo<List<string>>(param);
                #region Dict
                case (int)TypeToken.Dictionary + (int)TypeToken.Sbyte * 100 + (int)TypeToken.Sbyte: return new ByteFileInfo<Dictionary<sbyte, sbyte>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Sbyte * 100 + (int)TypeToken.Byte: return new ByteFileInfo<Dictionary<sbyte, byte>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Sbyte * 100 + (int)TypeToken.Bool: return new ByteFileInfo<Dictionary<sbyte, bool>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Sbyte * 100 + (int)TypeToken.UShort: return new ByteFileInfo<Dictionary<sbyte, ushort>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Sbyte * 100 + (int)TypeToken.Short: return new ByteFileInfo<Dictionary<sbyte, short>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Sbyte * 100 + (int)TypeToken.UInt: return new ByteFileInfo<Dictionary<sbyte, uint>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Sbyte * 100 + (int)TypeToken.Int: return new ByteFileInfo<Dictionary<sbyte, int>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Sbyte * 100 + (int)TypeToken.Float: return new ByteFileInfo<Dictionary<sbyte, float>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Sbyte * 100 + (int)TypeToken.Double: return new ByteFileInfo<Dictionary<sbyte, double>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Sbyte * 100 + (int)TypeToken.ULong: return new ByteFileInfo<Dictionary<sbyte, ulong>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Sbyte * 100 + (int)TypeToken.Long: return new ByteFileInfo<Dictionary<sbyte, long>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Sbyte * 100 + (int)TypeToken.String: return new ByteFileInfo<Dictionary<sbyte, string>>(param);

                case (int)TypeToken.Dictionary + (int)TypeToken.Byte * 100 + (int)TypeToken.Sbyte: return new ByteFileInfo<Dictionary<byte, sbyte>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Byte * 100 + (int)TypeToken.Byte: return new ByteFileInfo<Dictionary<byte, byte>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Byte * 100 + (int)TypeToken.Bool: return new ByteFileInfo<Dictionary<byte, bool>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Byte * 100 + (int)TypeToken.UShort: return new ByteFileInfo<Dictionary<byte, ushort>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Byte * 100 + (int)TypeToken.Short: return new ByteFileInfo<Dictionary<byte, short>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Byte * 100 + (int)TypeToken.UInt: return new ByteFileInfo<Dictionary<byte, uint>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Byte * 100 + (int)TypeToken.Int: return new ByteFileInfo<Dictionary<byte, int>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Byte * 100 + (int)TypeToken.Float: return new ByteFileInfo<Dictionary<byte, float>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Byte * 100 + (int)TypeToken.Double: return new ByteFileInfo<Dictionary<byte, double>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Byte * 100 + (int)TypeToken.ULong: return new ByteFileInfo<Dictionary<byte, ulong>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Byte * 100 + (int)TypeToken.Long: return new ByteFileInfo<Dictionary<byte, long>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Byte * 100 + (int)TypeToken.String: return new ByteFileInfo<Dictionary<byte, string>>(param);

                case (int)TypeToken.Dictionary + (int)TypeToken.Bool * 100 + (int)TypeToken.Sbyte: return new ByteFileInfo<Dictionary<bool, sbyte>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Bool * 100 + (int)TypeToken.Byte: return new ByteFileInfo<Dictionary<bool, byte>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Bool * 100 + (int)TypeToken.Bool: return new ByteFileInfo<Dictionary<bool, bool>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Bool * 100 + (int)TypeToken.UShort: return new ByteFileInfo<Dictionary<bool, ushort>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Bool * 100 + (int)TypeToken.Short: return new ByteFileInfo<Dictionary<bool, short>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Bool * 100 + (int)TypeToken.UInt: return new ByteFileInfo<Dictionary<bool, uint>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Bool * 100 + (int)TypeToken.Int: return new ByteFileInfo<Dictionary<bool, int>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Bool * 100 + (int)TypeToken.Float: return new ByteFileInfo<Dictionary<bool, float>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Bool * 100 + (int)TypeToken.Double: return new ByteFileInfo<Dictionary<bool, double>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Bool * 100 + (int)TypeToken.ULong: return new ByteFileInfo<Dictionary<bool, ulong>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Bool * 100 + (int)TypeToken.Long: return new ByteFileInfo<Dictionary<bool, long>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Bool * 100 + (int)TypeToken.String: return new ByteFileInfo<Dictionary<bool, string>>(param);

                case (int)TypeToken.Dictionary + (int)TypeToken.UShort * 100 + (int)TypeToken.Sbyte: return new ByteFileInfo<Dictionary<ushort, sbyte>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.UShort * 100 + (int)TypeToken.Byte: return new ByteFileInfo<Dictionary<ushort, byte>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.UShort * 100 + (int)TypeToken.Bool: return new ByteFileInfo<Dictionary<ushort, bool>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.UShort * 100 + (int)TypeToken.UShort: return new ByteFileInfo<Dictionary<ushort, ushort>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.UShort * 100 + (int)TypeToken.Short: return new ByteFileInfo<Dictionary<ushort, short>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.UShort * 100 + (int)TypeToken.UInt: return new ByteFileInfo<Dictionary<ushort, uint>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.UShort * 100 + (int)TypeToken.Int: return new ByteFileInfo<Dictionary<ushort, int>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.UShort * 100 + (int)TypeToken.Float: return new ByteFileInfo<Dictionary<ushort, float>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.UShort * 100 + (int)TypeToken.Double: return new ByteFileInfo<Dictionary<ushort, double>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.UShort * 100 + (int)TypeToken.ULong: return new ByteFileInfo<Dictionary<ushort, ulong>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.UShort * 100 + (int)TypeToken.Long: return new ByteFileInfo<Dictionary<ushort, long>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.UShort * 100 + (int)TypeToken.String: return new ByteFileInfo<Dictionary<ushort, string>>(param);

                case (int)TypeToken.Dictionary + (int)TypeToken.Short * 100 + (int)TypeToken.Sbyte: return new ByteFileInfo<Dictionary<short, sbyte>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Short * 100 + (int)TypeToken.Byte: return new ByteFileInfo<Dictionary<short, byte>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Short * 100 + (int)TypeToken.Bool: return new ByteFileInfo<Dictionary<short, bool>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Short * 100 + (int)TypeToken.UShort: return new ByteFileInfo<Dictionary<short, ushort>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Short * 100 + (int)TypeToken.Short: return new ByteFileInfo<Dictionary<short, short>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Short * 100 + (int)TypeToken.UInt: return new ByteFileInfo<Dictionary<short, uint>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Short * 100 + (int)TypeToken.Int: return new ByteFileInfo<Dictionary<short, int>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Short * 100 + (int)TypeToken.Float: return new ByteFileInfo<Dictionary<short, float>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Short * 100 + (int)TypeToken.Double: return new ByteFileInfo<Dictionary<short, double>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Short * 100 + (int)TypeToken.ULong: return new ByteFileInfo<Dictionary<short, ulong>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Short * 100 + (int)TypeToken.Long: return new ByteFileInfo<Dictionary<short, long>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Short * 100 + (int)TypeToken.String: return new ByteFileInfo<Dictionary<short, string>>(param);

                case (int)TypeToken.Dictionary + (int)TypeToken.UInt * 100 + (int)TypeToken.Sbyte: return new ByteFileInfo<Dictionary<uint, sbyte>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.UInt * 100 + (int)TypeToken.Byte: return new ByteFileInfo<Dictionary<uint, byte>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.UInt * 100 + (int)TypeToken.Bool: return new ByteFileInfo<Dictionary<uint, bool>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.UInt * 100 + (int)TypeToken.UShort: return new ByteFileInfo<Dictionary<uint, ushort>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.UInt * 100 + (int)TypeToken.Short: return new ByteFileInfo<Dictionary<uint, short>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.UInt * 100 + (int)TypeToken.UInt: return new ByteFileInfo<Dictionary<uint, uint>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.UInt * 100 + (int)TypeToken.Int: return new ByteFileInfo<Dictionary<uint, int>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.UInt * 100 + (int)TypeToken.Float: return new ByteFileInfo<Dictionary<uint, float>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.UInt * 100 + (int)TypeToken.Double: return new ByteFileInfo<Dictionary<uint, double>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.UInt * 100 + (int)TypeToken.ULong: return new ByteFileInfo<Dictionary<uint, ulong>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.UInt * 100 + (int)TypeToken.Long: return new ByteFileInfo<Dictionary<uint, long>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.UInt * 100 + (int)TypeToken.String: return new ByteFileInfo<Dictionary<uint, string>>(param);

                case (int)TypeToken.Dictionary + (int)TypeToken.Int * 100 + (int)TypeToken.Sbyte: return new ByteFileInfo<Dictionary<int, sbyte>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Int * 100 + (int)TypeToken.Byte: return new ByteFileInfo<Dictionary<int, byte>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Int * 100 + (int)TypeToken.Bool: return new ByteFileInfo<Dictionary<int, bool>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Int * 100 + (int)TypeToken.UShort: return new ByteFileInfo<Dictionary<int, ushort>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Int * 100 + (int)TypeToken.Short: return new ByteFileInfo<Dictionary<int, short>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Int * 100 + (int)TypeToken.UInt: return new ByteFileInfo<Dictionary<int, uint>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Int * 100 + (int)TypeToken.Int: return new ByteFileInfo<Dictionary<int, int>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Int * 100 + (int)TypeToken.Float: return new ByteFileInfo<Dictionary<int, float>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Int * 100 + (int)TypeToken.Double: return new ByteFileInfo<Dictionary<int, double>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Int * 100 + (int)TypeToken.ULong: return new ByteFileInfo<Dictionary<int, ulong>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Int * 100 + (int)TypeToken.Long: return new ByteFileInfo<Dictionary<int, long>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Int * 100 + (int)TypeToken.String: return new ByteFileInfo<Dictionary<int, string>>(param);

                case (int)TypeToken.Dictionary + (int)TypeToken.Float * 100 + (int)TypeToken.Sbyte: return new ByteFileInfo<Dictionary<float, sbyte>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Float * 100 + (int)TypeToken.Byte: return new ByteFileInfo<Dictionary<float, byte>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Float * 100 + (int)TypeToken.Bool: return new ByteFileInfo<Dictionary<float, bool>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Float * 100 + (int)TypeToken.UShort: return new ByteFileInfo<Dictionary<float, ushort>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Float * 100 + (int)TypeToken.Short: return new ByteFileInfo<Dictionary<float, short>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Float * 100 + (int)TypeToken.UInt: return new ByteFileInfo<Dictionary<float, uint>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Float * 100 + (int)TypeToken.Int: return new ByteFileInfo<Dictionary<float, int>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Float * 100 + (int)TypeToken.Float: return new ByteFileInfo<Dictionary<float, float>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Float * 100 + (int)TypeToken.Double: return new ByteFileInfo<Dictionary<float, double>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Float * 100 + (int)TypeToken.ULong: return new ByteFileInfo<Dictionary<float, ulong>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Float * 100 + (int)TypeToken.Long: return new ByteFileInfo<Dictionary<float, long>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Float * 100 + (int)TypeToken.String: return new ByteFileInfo<Dictionary<float, string>>(param);

                case (int)TypeToken.Dictionary + (int)TypeToken.Double * 100 + (int)TypeToken.Sbyte: return new ByteFileInfo<Dictionary<double, sbyte>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Double * 100 + (int)TypeToken.Byte: return new ByteFileInfo<Dictionary<double, byte>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Double * 100 + (int)TypeToken.Bool: return new ByteFileInfo<Dictionary<double, bool>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Double * 100 + (int)TypeToken.UShort: return new ByteFileInfo<Dictionary<double, ushort>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Double * 100 + (int)TypeToken.Short: return new ByteFileInfo<Dictionary<double, short>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Double * 100 + (int)TypeToken.UInt: return new ByteFileInfo<Dictionary<double, uint>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Double * 100 + (int)TypeToken.Int: return new ByteFileInfo<Dictionary<double, int>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Double * 100 + (int)TypeToken.Float: return new ByteFileInfo<Dictionary<double, float>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Double * 100 + (int)TypeToken.Double: return new ByteFileInfo<Dictionary<double, double>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Double * 100 + (int)TypeToken.ULong: return new ByteFileInfo<Dictionary<double, ulong>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Double * 100 + (int)TypeToken.Long: return new ByteFileInfo<Dictionary<double, long>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Double * 100 + (int)TypeToken.String: return new ByteFileInfo<Dictionary<double, string>>(param);

                case (int)TypeToken.Dictionary + (int)TypeToken.ULong * 100 + (int)TypeToken.Sbyte: return new ByteFileInfo<Dictionary<ulong, sbyte>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.ULong * 100 + (int)TypeToken.Byte: return new ByteFileInfo<Dictionary<ulong, byte>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.ULong * 100 + (int)TypeToken.Bool: return new ByteFileInfo<Dictionary<ulong, bool>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.ULong * 100 + (int)TypeToken.UShort: return new ByteFileInfo<Dictionary<ulong, ushort>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.ULong * 100 + (int)TypeToken.Short: return new ByteFileInfo<Dictionary<ulong, short>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.ULong * 100 + (int)TypeToken.UInt: return new ByteFileInfo<Dictionary<ulong, uint>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.ULong * 100 + (int)TypeToken.Int: return new ByteFileInfo<Dictionary<ulong, int>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.ULong * 100 + (int)TypeToken.Float: return new ByteFileInfo<Dictionary<ulong, float>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.ULong * 100 + (int)TypeToken.Double: return new ByteFileInfo<Dictionary<ulong, double>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.ULong * 100 + (int)TypeToken.ULong: return new ByteFileInfo<Dictionary<ulong, ulong>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.ULong * 100 + (int)TypeToken.Long: return new ByteFileInfo<Dictionary<ulong, long>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.ULong * 100 + (int)TypeToken.String: return new ByteFileInfo<Dictionary<ulong, string>>(param);

                case (int)TypeToken.Dictionary + (int)TypeToken.Long * 100 + (int)TypeToken.Sbyte: return new ByteFileInfo<Dictionary<long, sbyte>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Long * 100 + (int)TypeToken.Byte: return new ByteFileInfo<Dictionary<long, byte>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Long * 100 + (int)TypeToken.Bool: return new ByteFileInfo<Dictionary<long, bool>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Long * 100 + (int)TypeToken.UShort: return new ByteFileInfo<Dictionary<long, ushort>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Long * 100 + (int)TypeToken.Short: return new ByteFileInfo<Dictionary<long, short>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Long * 100 + (int)TypeToken.UInt: return new ByteFileInfo<Dictionary<long, uint>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Long * 100 + (int)TypeToken.Int: return new ByteFileInfo<Dictionary<long, int>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Long * 100 + (int)TypeToken.Float: return new ByteFileInfo<Dictionary<long, float>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Long * 100 + (int)TypeToken.Double: return new ByteFileInfo<Dictionary<long, double>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Long * 100 + (int)TypeToken.ULong: return new ByteFileInfo<Dictionary<long, ulong>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Long * 100 + (int)TypeToken.Long: return new ByteFileInfo<Dictionary<long, long>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.Long * 100 + (int)TypeToken.String: return new ByteFileInfo<Dictionary<long, string>>(param);

                case (int)TypeToken.Dictionary + (int)TypeToken.String * 100 + (int)TypeToken.Sbyte: return new ByteFileInfo<Dictionary<string, sbyte>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.String * 100 + (int)TypeToken.Byte: return new ByteFileInfo<Dictionary<string, byte>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.String * 100 + (int)TypeToken.Bool: return new ByteFileInfo<Dictionary<string, bool>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.String * 100 + (int)TypeToken.UShort: return new ByteFileInfo<Dictionary<string, ushort>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.String * 100 + (int)TypeToken.Short: return new ByteFileInfo<Dictionary<string, short>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.String * 100 + (int)TypeToken.UInt: return new ByteFileInfo<Dictionary<string, uint>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.String * 100 + (int)TypeToken.Int: return new ByteFileInfo<Dictionary<string, int>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.String * 100 + (int)TypeToken.Float: return new ByteFileInfo<Dictionary<string, float>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.String * 100 + (int)TypeToken.Double: return new ByteFileInfo<Dictionary<string, double>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.String * 100 + (int)TypeToken.ULong: return new ByteFileInfo<Dictionary<string, ulong>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.String * 100 + (int)TypeToken.Long: return new ByteFileInfo<Dictionary<string, long>>(param);
                case (int)TypeToken.Dictionary + (int)TypeToken.String * 100 + (int)TypeToken.String: return new ByteFileInfo<Dictionary<string, string>>(param);
                #endregion
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