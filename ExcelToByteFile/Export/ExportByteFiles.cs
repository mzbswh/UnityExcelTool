using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            fileBuffer.WriteShort((short)fileDatas.Count, true);   // 先写入总个数
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
                        //fileBuffer.WriteList(optimize.SegmentStartList, true);
                        break;
                    case OptimizeType.PartialContinuity:
                        int preCnt = 0;
                        for (int c = 0; c < optimize.PartialContinuityStart; c++)
                        {
                            preCnt += optimize.SegmentList[c];
                        }
                        fileBuffer.WriteInt(preCnt * data.RowLength, true); // 连续部分起始偏移
                        fileBuffer.WriteInt(optimize.SegmentList[optimize.PartialContinuityStart], true); // 连续的有多少个元素
                        //fileBuffer.WriteLong(optimize.PartialContinuityStart, true);
                        break;
                }
                fileBuffer.WriteDict(data.SheetConfig.ExtraInfo, TypeDef.stringType, TypeDef.stringType, true);
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
                    buffer.WriteSbyte(StringConvert.ToValue<sbyte>(value));
                    break;
                case TypeDef.uintType:
                    buffer.WriteUInt(StringConvert.ToValue<uint>(value));
                    break;
                case TypeDef.ulongType:
                    buffer.WriteULong(StringConvert.ToValue<ulong>(value));
                    break;
                case TypeDef.ushortType:
                    buffer.WriteUShort(StringConvert.ToValue<ushort>(value));
                    break;
                case TypeDef.boolType:
                    buffer.WriteBool(StringConvert.ToBool(value));
                    break;
                case TypeDef.byteType:
                    buffer.WriteByte(StringConvert.ToValue<byte>(value));
                    break;
                case TypeDef.shortType:
                    buffer.WriteShort(StringConvert.ToValue<short>(value));
                    break;
                case TypeDef.intType:
                    buffer.WriteInt(StringConvert.ToValue<int>(value));
                    break;
                case TypeDef.floatType:
                    buffer.WriteFloat(StringConvert.ToValue<float>(value));
                    break;
                case TypeDef.stringType:
                    buffer.WriteString(value);
                    break;
                case TypeDef.longType:
                    buffer.WriteLong(StringConvert.ToValue<long>(value));
                    break;
                case TypeDef.doubleType:
                    buffer.WriteDouble(StringConvert.ToValue<double>(value));
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
                    buffer.WriteListSbyte(StringConvert.ToList<sbyte>(value, SymbolDef.splitChar));
                    break;
                case TypeDef.uintType:
                    buffer.WriteListUInt(StringConvert.ToList<uint>(value, SymbolDef.splitChar));
                    break;
                case TypeDef.ulongType:
                    buffer.WriteListULong(StringConvert.ToList<ulong>(value, SymbolDef.splitChar));
                    break;
                case TypeDef.ushortType:
                    buffer.WriteListUShort(StringConvert.ToList<ushort>(value, SymbolDef.splitChar));
                    break;
                case TypeDef.boolType:
                    buffer.WriteListBool(StringConvert.ToList<bool>(value, SymbolDef.splitChar));
                    break;
                case TypeDef.byteType:
                    buffer.WriteListByte(StringConvert.ToList<byte>(value, SymbolDef.splitChar));
                    break;
                case TypeDef.shortType:
                    buffer.WriteListShort(StringConvert.ToList<short>(value, SymbolDef.splitChar));
                    break;
                case TypeDef.intType:
                    buffer.WriteListInt(StringConvert.ToList<int>(value, SymbolDef.splitChar));
                    break;
                case TypeDef.floatType:
                    buffer.WriteListFloat(StringConvert.ToList<float>(value, SymbolDef.splitChar)); break;
                case TypeDef.stringType:
                    buffer.WriteListString(StringConvert.ToStringList(value)); break;
                case TypeDef.longType:
                    buffer.WriteListLong(StringConvert.ToList<long>(value, SymbolDef.splitChar)); break;
                case TypeDef.doubleType:
                    buffer.WriteListDouble(StringConvert.ToList<double>(value, SymbolDef.splitChar)); break;
            }
        }

        private static void WriteDict(ByteWriterBuffer buffer, string[] subType, string value)
        {
            var dict = StringConvert.ToDict<string, string>(value);
            buffer.WriteDict(dict, subType[0], subType[1]);
        }

        private static void WriteVect(ByteWriterBuffer buffer, string[] subType, string value)
        {
            int dimen = int.Parse(subType[0]);
            if (dimen == 2)
            {
                if (subType[1] == TypeDef.intType)
                {
                    Vector2Int vec = StringConvert.ToVec2Int(value);
                    buffer.WriteVec2Int(vec);
                }
                else
                {
                    Vector2 vec = StringConvert.ToVec2(value);
                    buffer.WriteVec2(vec);
                }
            }
            else if (dimen == 3)
            {
                if (subType[1] == TypeDef.intType)
                {
                    Vector3Int vec = StringConvert.ToVec3Int(value);
                    buffer.WriteVec3Int(vec);
                }
                else
                {
                    Vector3 vec = StringConvert.ToVec3(value);
                    buffer.WriteVec3(vec);
                }
            }
            else
            {
                if (subType[1] == TypeDef.intType)
                {
                    Vector4Int vec = StringConvert.ToVec4Int(value);
                    buffer.WriteVec4Int(vec);
                }
                else
                {
                    Vector4 vec = StringConvert.ToVec4(value);
                    buffer.WriteVec4(vec);
                }
            }
        }
    }
}
