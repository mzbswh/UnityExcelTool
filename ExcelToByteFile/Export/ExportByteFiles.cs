using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ExcelToByteFile
{
    internal class ExportByteFiles
    {
        public static void Export(string path, ManifestData manifest, SheetData sheet)
        {
            var fileBuffer = ExportMgr.fileBuffer;
            fileBuffer.Clear();
            int heapStart = manifest.AlignLength;
            fileBuffer.SetNonAlignStartPos(heapStart); // 设置引用类型起始地址

            for (int i = 0; i < sheet.rows.Count; i++)
            {
                RowData row = sheet.rows[i];
                // 写入数据
                for (int j = 0; j < sheet.heads.Count; j++)
                {
                    HeadData head = sheet.heads[j];
                    string value = row[j];
                    WriteCell(fileBuffer, head, value);
                }
            }

            // 创建文件
            string filePath = StringHelper.MakeFullPath(path + Path.DirectorySeparatorChar, $"{manifest.ByteFileName}.bytes");
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    byte[] data = fileBuffer.GetBuffer();
                    int length = fileBuffer.ReadableBytes;
                    fs.Write(data, 0, length);
                }
            }
            catch (Exception ex)
            {
                Log.Error($"生成{manifest.ByteFileName}.bytes错误:" + ex.Message);
            }
        }

        public static void ExportManifest(List<ManifestData> fileDatas)
        {
            var fileBuffer = ExportMgr.fileBuffer;
            Program.mainForm?.Invoke(new Action(() => Program.mainForm.SetProgress(1, "正在生成：manifest.bytess")));
            string path = GlobalConfig.Ins.byteFileOutputDir;
            fileBuffer.Clear();
            // 写入信息
            fileBuffer.SetNonAlignStartPos(0);
            fileBuffer.WriteInt(fileDatas.Count, true);   // 先写入总个数
            for (int i = 0, len = fileDatas.Count; i < len; i++)
            {
                ManifestData data = fileDatas[i];
                fileBuffer.WriteString(data.ByteFileName, true);
                fileBuffer.WriteInt(data.IdColIndex, true);
                fileBuffer.WriteInt(data.RowCount, true);
                fileBuffer.WriteInt(data.RowLength, true);
                fileBuffer.WriteListInt(data.ColOffset, true);
                fileBuffer.WriteListInt(data.Tokens, true);
                fileBuffer.WriteListString(data.VariableNames, true);
                fileBuffer.WriteBool(data.SheetConfig.CacheData, true);
                var optimize = data.SheetOptimizeData;
                fileBuffer.WriteByte((byte)optimize.OptimizeType, true);
                switch (optimize.OptimizeType)
                {
                    case OptimizeType.Continuity:
                        fileBuffer.WriteInt((int)optimize.Step, true);
                        break;
                    case OptimizeType.Segment:
                        fileBuffer.WriteList(optimize.SegmentList, true);
                        switch (data.Tokens[data.IdColIndex])
                        {
                            case (int)TypeToken.Sbyte:
                            {
                                List<sbyte> ls = new List<sbyte>(optimize.SegmentStartList.Count);
                                for (int j = 0; j < optimize.SegmentStartList.Count; j++)
                                {
                                    ls.Add((sbyte)optimize.SegmentStartList[j]);
                                }
                                fileBuffer.WriteList(ls, true);
                                break;
                            }
                            case (int)TypeToken.Byte:
                            {
                                List<byte> ls = new List<byte>(optimize.SegmentStartList.Count);
                                for (int j = 0; j < optimize.SegmentStartList.Count; j++)
                                {
                                    ls.Add((byte)optimize.SegmentStartList[j]);
                                }
                                fileBuffer.WriteList(ls, true);
                                break;
                            }
                            case (int)TypeToken.UShort:
                            {
                                List<ushort> ls = new List<ushort>(optimize.SegmentStartList.Count);
                                for (int j = 0; j < optimize.SegmentStartList.Count; j++)
                                {
                                    ls.Add((ushort)optimize.SegmentStartList[j]);
                                }
                                fileBuffer.WriteList(ls, true);
                                break;
                            }
                            case (int)TypeToken.Short:
                            {
                                List<short> ls = new List<short>(optimize.SegmentStartList.Count);
                                for (int j = 0; j < optimize.SegmentStartList.Count; j++)
                                {
                                    ls.Add((short)optimize.SegmentStartList[j]);
                                }
                                fileBuffer.WriteList(ls, true);
                                break;
                            }
                            case (int)TypeToken.UInt:
                            {
                                List<uint> ls = new List<uint>(optimize.SegmentStartList.Count);
                                for (int j = 0; j < optimize.SegmentStartList.Count; j++)
                                {
                                    ls.Add((uint)optimize.SegmentStartList[j]);
                                }
                                fileBuffer.WriteList(ls, true);
                                break;
                            }
                            case (int)TypeToken.Int:
                            {
                                List<int> ls = new List<int>(optimize.SegmentStartList.Count);
                                for (int j = 0; j < optimize.SegmentStartList.Count; j++)
                                {
                                    ls.Add((int)optimize.SegmentStartList[j]);
                                }
                                fileBuffer.WriteList(ls, true);
                                break;
                            }
                            case (int)TypeToken.ULong:
                            {
                                List<ulong> ls = new List<ulong>(optimize.SegmentStartList.Count);
                                for (int j = 0; j < optimize.SegmentStartList.Count; j++)
                                {
                                    ls.Add((ulong)optimize.SegmentStartList[j]);
                                }
                                fileBuffer.WriteList(ls, true);
                                break;
                            }
                            case (int)TypeToken.Long:
                            {
                                List<long> ls = new List<long>(optimize.SegmentStartList.Count);
                                for (int j = 0; j < optimize.SegmentStartList.Count; j++)
                                {
                                    ls.Add(optimize.SegmentStartList[j]);
                                }
                                fileBuffer.WriteList(ls, true);
                                break;
                            }
                        }
                        break;
                    case OptimizeType.PartialContinuity:
                        int preCnt = 0;
                        for (int c = 0; c < optimize.PartialContinuityStart; c++)
                        {
                            preCnt += optimize.SegmentList[c];
                        }
                        fileBuffer.WriteInt(preCnt, true); // 前面有多少个元素
                        fileBuffer.WriteInt(optimize.SegmentList[optimize.PartialContinuityStart], true); // 连续的有多少个元素
                        // 连续部分的开始元素值
                        switch (data.Tokens[data.IdColIndex])
                        {
                            case (int)TypeToken.Sbyte:
                            {
                                var value = (sbyte)optimize.SegmentStartList[optimize.PartialContinuityStart];
                                fileBuffer.WriteSbyte(value, true);
                                break;
                            }
                            case (int)TypeToken.Byte:
                            {
                                var value = (byte)optimize.SegmentStartList[optimize.PartialContinuityStart];
                                fileBuffer.WriteByte(value, true);
                                break;
                            }
                            case (int)TypeToken.UShort:
                            {
                                var value = (ushort)optimize.SegmentStartList[optimize.PartialContinuityStart];
                                fileBuffer.WriteUShort(value, true);
                                break;
                            }
                            case (int)TypeToken.Short:
                            {
                                var value = (short)optimize.SegmentStartList[optimize.PartialContinuityStart];
                                fileBuffer.WriteShort(value, true);
                                break;
                            }
                            case (int)TypeToken.UInt:
                            {
                                var value = (uint)optimize.SegmentStartList[optimize.PartialContinuityStart];
                                fileBuffer.WriteUInt(value, true);
                                break;
                            }
                            case (int)TypeToken.Int:
                            {
                                var value = (int)optimize.SegmentStartList[optimize.PartialContinuityStart];
                                fileBuffer.WriteInt(value, true);
                                break;
                            }
                            case (int)TypeToken.ULong:
                            {
                                var value = (ulong)optimize.SegmentStartList[optimize.PartialContinuityStart];
                                fileBuffer.WriteULong(value, true);
                                break;
                            }
                            case (int)TypeToken.Long:
                            {
                                var value = optimize.SegmentStartList[optimize.PartialContinuityStart];
                                fileBuffer.WriteLong(value, true);
                                break;
                            }
                        }
                        break;
                }
            }

            // 创建文件
            string filePath = StringHelper.MakeFullPath(path + Path.DirectorySeparatorChar, "manifest.bytes");
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    byte[] data = fileBuffer.GetBuffer();
                    int length = fileBuffer.ReadableBytes;
                    fs.Write(data, 0, length);
                }
            }
            catch (Exception ex)
            {
                Log.Error($"生成manifest.bytes错误:" + ex.Message);
            }
        }

        private static void WriteCell(ByteWriterBuffer buffer, HeadData head, string value)
        {
            switch (head.MainType)
            {
                case TypeDef.sbyteType:
                    buffer.WriteSbyte(StringConvert.StringToValue<sbyte>(value));
                    break;
                case TypeDef.uintType:
                    buffer.WriteUInt(StringConvert.StringToValue<uint>(value));
                    break;
                case TypeDef.ulongType:
                    buffer.WriteULong(StringConvert.StringToValue<ulong>(value));
                    break;
                case TypeDef.ushortType:
                    buffer.WriteUShort(StringConvert.StringToValue<ushort>(value));
                    break;
                case TypeDef.boolType:
                    buffer.WriteBool(StringConvert.StringToBool(value));
                    break;
                case TypeDef.byteType:
                    buffer.WriteByte(StringConvert.StringToValue<byte>(value));
                    break;
                case TypeDef.shortType:
                    buffer.WriteShort(StringConvert.StringToValue<short>(value));
                    break;
                case TypeDef.intType:
                    buffer.WriteInt(StringConvert.StringToValue<int>(value));
                    break;
                case TypeDef.floatType:
                    buffer.WriteFloat(StringConvert.StringToValue<float>(value));
                    break;
                case TypeDef.stringType:
                    buffer.WriteString(value);
                    break;
                case TypeDef.longType:
                    buffer.WriteLong(StringConvert.StringToValue<long>(value));
                    break;
                case TypeDef.doubleType:
                    buffer.WriteDouble(StringConvert.StringToValue<double>(value));
                    break;
                case TypeDef.listType:
                    WriteList(buffer, head.SubType[0], value);
                    break;
                case TypeDef.dictType:
                    WriteDict(buffer, head.SubType, value);
                    break;
                case TypeDef.vecType:
                    WriteVect(buffer, head.SubType, value);
                    break;
                default:
                    Log.Error($"不支持的类型 {head.MainType}");
                    break;
            }
        }

        private static void WriteList(ByteWriterBuffer buffer, string subType, string value)
        {
            switch (subType)
            {
                case TypeDef.sbyteType:
                    buffer.WriteListSbyte(StringConvert.StringToList<sbyte>(value, SymbolDef.splitChar));
                    break;
                case TypeDef.uintType:
                    buffer.WriteListUInt(StringConvert.StringToList<uint>(value, SymbolDef.splitChar));
                    break;
                case TypeDef.ulongType:
                    buffer.WriteListULong(StringConvert.StringToList<ulong>(value, SymbolDef.splitChar));
                    break;
                case TypeDef.ushortType:
                    buffer.WriteListUShort(StringConvert.StringToList<ushort>(value, SymbolDef.splitChar));
                    break;
                case TypeDef.boolType:
                    buffer.WriteListBool(StringConvert.StringToList<bool>(value, SymbolDef.splitChar));
                    break;
                case TypeDef.byteType:
                    buffer.WriteListByte(StringConvert.StringToList<byte>(value, SymbolDef.splitChar));
                    break;
                case TypeDef.shortType:
                    buffer.WriteListShort(StringConvert.StringToList<short>(value, SymbolDef.splitChar));
                    break;
                case TypeDef.intType:
                    buffer.WriteListInt(StringConvert.StringToList<int>(value, SymbolDef.splitChar));
                    break;
                case TypeDef.floatType:
                    buffer.WriteListFloat(StringConvert.StringToList<float>(value, SymbolDef.splitChar)); break;
                case TypeDef.stringType:
                    buffer.WriteListString(StringConvert.StringToStringList(value)); break;
                case TypeDef.longType:
                    buffer.WriteListLong(StringConvert.StringToList<long>(value, SymbolDef.splitChar)); break;
                case TypeDef.doubleType:
                    buffer.WriteListDouble(StringConvert.StringToList<double>(value, SymbolDef.splitChar)); break;
            }
        }

        private static void WriteDict(ByteWriterBuffer buffer, string[] subType, string value)
        {
            var dict = StringConvert.StringToDict<string, string>(value);
            buffer.WriteDict(dict, subType[0], subType[1]);
        }

        private static void WriteVect(ByteWriterBuffer buffer, string[] subType, string value)
        {
            int dimen = int.Parse(subType[0]);
            if (dimen == 2)
            {
                if (subType[1] == TypeDef.intType)
                {
                    Vector2Int vec = StringConvert.StringToVec2Int(value);
                    buffer.WriteVec2Int(vec);
                }
                else
                {
                    Vector2 vec = StringConvert.StringToVec2(value);
                    buffer.WriteVec2(vec);
                }
            }
            else if (dimen == 3)
            {
                if (subType[1] == TypeDef.intType)
                {
                    Vector3Int vec = StringConvert.StringToVec3Int(value);
                    buffer.WriteVec3Int(vec);
                }
                else
                {
                    Vector3 vec = StringConvert.StringToVec3(value);
                    buffer.WriteVec3(vec);
                }
            }
            else
            {
                if (subType[1] == TypeDef.intType)
                {
                    Vector4Int vec = StringConvert.StringToVec4Int(value);
                    buffer.WriteVec4Int(vec);
                }
                else
                {
                    Vector4 vec = StringConvert.StringToVec4(value);
                    buffer.WriteVec4(vec);
                }
            }
        }
    }
}
