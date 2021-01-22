using AspNetCore.ReportingServices.Rendering.ExcelRenderer.Excel.BIFF8;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AspNetCore.ReportingServices.Rendering.ExcelRenderer.ExcelGenerator.BIFF8.Records
{
	public static class RecordFactory
	{
		public enum BOFSubstreamType : ushort
		{
			WorkbookGlobal = 5,
			VisualBasicModule,
			Worksheet = 0x10,
			Chart = 0x20,
			Excel4Macro = 0x40,
			WorkspaceFile = 0x100
		}

		private const double ARIAL10ZEROWIDTH = 7.0;

		private const double POINTS_PER_INCH = 72.0;

		private const double PIXELS_PER_INCH = 96.0;

		private const ushort BUILTIN_DESCRIPTION_LENGTH = 11;

		private static UnicodeEncoding m_uniEncoding = new UnicodeEncoding();

		public static void WriteHeader(BinaryWriter output, short type, int size)
		{
			output.Write(type);
			output.Write((ushort)size);
		}

		private static void WriteCellHeader(BinaryWriter output, short type, int size, ushort row, ushort col, ushort ixfe)
		{
			RecordFactory.WriteHeader(output, type, size);
			output.Write(row);
			output.Write(col);
			output.Write(ixfe);
		}

		private static int ToColumnWidth(double valueInPoints)
		{
			double num = (double)RecordFactory.PointsToPixels(valueInPoints);
			double num2 = num / 7.0;
			if (num2 < 0.0)
			{
				num2 = 0.0;
			}
			else if (num2 > 255.0)
			{
				num2 = 255.0;
			}
			return (int)(num2 * 256.0);
		}

		private static int PointsToPixels(double points)
		{
			return (int)(points / 72.0 * 96.0);
		}

		public static long BOF(BinaryWriter output, BOFSubstreamType type)
		{
			long position = output.BaseStream.Position;
			RecordFactory.WriteHeader(output, 2057, 16);
			output.Write((ushort)1536);
			output.Write((ushort)type);
			output.Write((ushort)3515);
			output.Write((ushort)1996);
			output.Write(49353u);
			output.Write(518u);
			return output.BaseStream.Position - position;
		}

		public static long BOUNDSHEET(BinaryWriter output, uint offsetToBOF, string sheetName)
		{
			long position = output.BaseStream.Position;
			bool compressed = default(bool);
			int num = StringUtil.CalcBIFF8StringSize(sheetName, out compressed);
			RecordFactory.WriteHeader(output, 133, num + 8);
			output.Write(offsetToBOF);
			output.Write((ushort)0);
			output.Write((byte)sheetName.Length);
			StringUtil.WriteBIFF8String(output, sheetName, false, compressed);
			return output.BaseStream.Position - position;
		}

		public static long COLINFO(BinaryWriter output, ushort column, double colWidth, ushort outlineLevel, bool collapsed)
		{
			long position = output.BaseStream.Position;
			RecordFactory.WriteHeader(output, 125, 12);
			output.Write(column);
			output.Write(column);
			output.Write((ushort)RecordFactory.ToColumnWidth(colWidth));
			output.Write((ushort)15);
			ushort aDst = 0;
			outlineLevel = Math.Min(outlineLevel, (ushort)7);
			outlineLevel = Math.Max(outlineLevel, (ushort)0);
			aDst = BitField16.PutValue(aDst, 1792, outlineLevel);
			aDst = BitField16.PutValue(aDst, 4096, (ushort)(collapsed ? 1 : 0));
			aDst = BitField16.PutValue(aDst, 1, (ushort)(collapsed ? 1 : 0));
			output.Write(aDst);
			output.Write((ushort)0);
			return output.BaseStream.Position - position;
		}

		public static long DIMENSIONS(BinaryWriter output, uint rowStart, uint rowStop, ushort colStart, ushort colStop)
		{
			long position = output.BaseStream.Position;
			RecordFactory.WriteHeader(output, 512, 14);
			output.Write(rowStart);
			output.Write(rowStop + 1);
			output.Write(colStart);
			output.Write(colStop + 1);
			return output.BaseStream.Position - position;
		}

		public static long INDEX(BinaryWriter output, uint firstRow, uint lastRow, List<uint> dbCellOffsets)
		{
			long position = output.BaseStream.Position;
			RecordFactory.WriteHeader(output, 523, 16 + 4 * dbCellOffsets.Count);
			output.Write(0);
			output.Write(firstRow);
			output.Write(lastRow + 1);
			output.Write(0);
			foreach (uint dbCellOffset in dbCellOffsets)
			{
				output.Write(dbCellOffset);
			}
			return output.BaseStream.Position - position;
		}

		public static long DBCELL(BinaryWriter output, uint startRowOffset, List<ushort> streamOffsets)
		{
			long position = output.BaseStream.Position;
			RecordFactory.WriteHeader(output, 215, 4 + 2 * streamOffsets.Count);
			output.Write(startRowOffset);
			foreach (ushort streamOffset in streamOffsets)
			{
				output.Write(streamOffset);
			}
			return output.BaseStream.Position - position;
		}

		public static long ROW(BinaryWriter output, ushort rowIndex, ushort colMin, ushort colMax, ushort rowHeight, ushort outlineLevel, bool collapsed, bool autoSize)
		{
			long position = output.BaseStream.Position;
			RecordFactory.WriteHeader(output, 520, 16);
			output.Write(rowIndex);
			output.Write(colMin);
			output.Write(colMax);
			output.Write(rowHeight);
			output.Write((ushort)0);
			output.Write((ushort)0);
			ushort aDst = 320;
			outlineLevel = Math.Min(outlineLevel, (ushort)7);
			outlineLevel = Math.Max(outlineLevel, (ushort)0);
			aDst = BitField16.PutValue(aDst, 7, outlineLevel);
			aDst = BitField16.PutValue(aDst, 64, (ushort)((!autoSize) ? 1 : 0));
			aDst = BitField16.PutValue(aDst, 32, (ushort)(collapsed ? 1 : 0));
			aDst = BitField16.PutValue(aDst, 16, (ushort)(collapsed ? 1 : 0));
			output.Write(aDst);
			output.Write((ushort)15);
			return output.BaseStream.Position - position;
		}

		public static long BLANK(BinaryWriter output, ushort row, ushort col, ushort ixfe)
		{
			long position = output.BaseStream.Position;
			RecordFactory.WriteCellHeader(output, 513, 6, row, col, ixfe);
			return output.BaseStream.Position - position;
		}

		public static long MULBLANK(BinaryWriter output, ushort row, ushort colFirst, ushort colLast, ushort[] xfIndexes, int numValues)
		{
			long position = output.BaseStream.Position;
			RecordFactory.WriteHeader(output, 190, 6 + 2 * numValues);
			output.Write(row);
			output.Write(colFirst);
			for (int i = 0; i < numValues; i++)
			{
				output.Write(xfIndexes[i]);
			}
			output.Write(colLast);
			return output.BaseStream.Position - position;
		}

		public static long BOOLERR(BinaryWriter output, ushort row, ushort column, ushort ixfe, byte valOrErrorCode, bool isError)
		{
			long position = output.BaseStream.Position;
			RecordFactory.WriteCellHeader(output, 517, 8, row, column, ixfe);
			output.Write(valOrErrorCode);
			output.Write(isError);
			return output.BaseStream.Position - position;
		}

		public static long LABEL(BinaryWriter output, ushort rowIndex, ushort colIndex, ushort ixfe, string rgch)
		{
			long position = output.BaseStream.Position;
			bool compressed = default(bool);
			int num = StringUtil.CalcBIFF8StringSize(rgch, out compressed);
			RecordFactory.WriteCellHeader(output, 516, 9 + num, rowIndex, colIndex, ixfe);
			StringUtil.WriteBIFF8String(output, rgch, compressed);
			return output.BaseStream.Position - position;
		}

		public static long LABELSST(BinaryWriter output, ushort rowIndex, ushort colIndex, ushort ixfe, uint isst)
		{
			long position = output.BaseStream.Position;
			RecordFactory.WriteCellHeader(output, 253, 10, rowIndex, colIndex, ixfe);
			output.Write(isst);
			return output.BaseStream.Position - position;
		}

		public static long RK(BinaryWriter output, ushort rowIndex, ushort colIndex, ushort ixfe, uint rkValue)
		{
			long position = output.BaseStream.Position;
			RecordFactory.WriteCellHeader(output, 638, 10, rowIndex, colIndex, ixfe);
			output.Write(rkValue);
			return output.BaseStream.Position - position;
		}

		public static long MULRK(BinaryWriter output, ushort rowIndex, ushort colFirst, ushort colLast, ushort[] xfIndexes, uint[] rkValues, int numValues)
		{
			long position = output.BaseStream.Position;
			RecordFactory.WriteHeader(output, 189, 6 + 6 * numValues);
			output.Write(rowIndex);
			output.Write(colFirst);
			for (int i = 0; i < numValues; i++)
			{
				output.Write(xfIndexes[i]);
				output.Write(rkValues[i]);
			}
			output.Write(colLast);
			return output.BaseStream.Position - position;
		}

		public static long NUMBER(BinaryWriter output, ushort rowIndex, ushort colIndex, ushort ixfe, double value)
		{
			long position = output.BaseStream.Position;
			RecordFactory.WriteCellHeader(output, 515, 14, rowIndex, colIndex, ixfe);
			output.Write(value);
			return output.BaseStream.Position - position;
		}

		public static long MERGECELLS(BinaryWriter output, List<AreaInfo> mergeAreas)
		{
			long position = output.BaseStream.Position;
			int num = mergeAreas.Count;
			IEnumerator<AreaInfo> enumerator = (IEnumerator<AreaInfo>)(object)mergeAreas.GetEnumerator();
			while (num > 0)
			{
				int num2 = Math.Min(num, 1024);
				num -= num2;
				RecordFactory.WriteHeader(output, 229, 2 + num2 * 8);
				output.Write((ushort)num2);
				for (int i = 0; i < num2; i++)
				{
					enumerator.MoveNext();
					enumerator.Current.WriteToStream(output);
				}
			}
			return output.BaseStream.Position - position;
		}

		public static long FONT(BinaryWriter output, BIFF8Font font)
		{
			long position = output.BaseStream.Position;
			string text = font.Name;
			if (text.Length > 256)
			{
				text = text.Substring(0, 256);
			}
			bool flag = true;
			byte[] buffer = StringUtil.DecodeTo1Byte(text);
			char[] array = text.ToCharArray();
			for (int i = 0; i < text.Length; i++)
			{
				if (!flag)
				{
					break;
				}
				if ((array[i] & 0xFF00) > 0)
				{
					flag = false;
				}
			}
			int num = 0;
			if (flag)
			{
				num = text.Length;
			}
			else
			{
				num = text.Length * 2;
				buffer = StringUtil.DecodeTo2ByteLE(text);
			}
			RecordFactory.WriteHeader(output, 49, 16 + num);
			output.Write(font.RecordData, 0, font.RecordData.Length);
			output.Write((byte)text.Length);
			output.Write(!flag);
			output.Write(buffer);
			return output.BaseStream.Position - position;
		}

		public static long FORMAT(BinaryWriter output, string format, int ifmt)
		{
			long position = output.BaseStream.Position;
			bool compressed = default(bool);
			int num = StringUtil.CalcBIFF8StringSize(format, out compressed);
			RecordFactory.WriteHeader(output, 1054, 5 + num);
			output.Write((ushort)ifmt);
			StringUtil.WriteBIFF8String(output, format, compressed);
			return output.BaseStream.Position - position;
		}

		public static long XF(BinaryWriter output, byte[] styleData)
		{
			long position = output.BaseStream.Position;
			RecordFactory.WriteHeader(output, 224, 20);
			output.Write(styleData, 0, styleData.Length);
			return output.BaseStream.Position - position;
		}

		public static long PALETTE(BinaryWriter output, List<BIFF8Color> colors)
		{
			long position = output.BaseStream.Position;
			RecordFactory.WriteHeader(output, 146, 2 + colors.Count * 4);
			output.Write((ushort)colors.Count);
			foreach (BIFF8Color color in colors)
			{
				output.Write(color.Red);
				output.Write(color.Green);
				output.Write(color.Blue);
				output.Write((byte)0);
			}
			return output.BaseStream.Position - position;
		}

		public static long OBJ(BinaryWriter output, ushort objId)
		{
			long position = output.BaseStream.Position;
			RecordFactory.WriteHeader(output, 93, 38);
			output.Write((ushort)21);
			output.Write((ushort)18);
			output.Write((ushort)8);
			output.Write(objId);
			output.Write((ushort)24593);
			byte[] buffer = new byte[12];
			output.Write(buffer);
			output.Write(new byte[16]
			{
				7,
				0,
				2,
				0,
				255,
				255,
				8,
				0,
				2,
				0,
				1,
				0,
				0,
				0,
				0,
				0
			});
			return output.BaseStream.Position - position;
		}

		public static long HEADER(BinaryWriter output, string headerString)
		{
			long position = output.BaseStream.Position;
			bool compressed = default(bool);
			RecordFactory.WriteHeader(output, 20, StringUtil.CalcBIFF8StringSize(headerString, out compressed) + 3);
			StringUtil.WriteBIFF8String(output, headerString, compressed);
			return output.BaseStream.Position - position;
		}

		public static long FOOTER(BinaryWriter output, string footerString)
		{
			long position = output.BaseStream.Position;
			bool compressed = default(bool);
			RecordFactory.WriteHeader(output, 21, StringUtil.CalcBIFF8StringSize(footerString, out compressed) + 3);
			StringUtil.WriteBIFF8String(output, footerString, compressed);
			return output.BaseStream.Position - position;
		}

		public static long PANE(BinaryWriter output, ushort verticalSplit, ushort horizontalSplit, ushort topVisible, ushort leftVisible, ushort activePane)
		{
			long position = output.BaseStream.Position;
			RecordFactory.WriteHeader(output, 65, 10);
			output.Write(verticalSplit);
			output.Write(horizontalSplit);
			output.Write(topVisible);
			output.Write(leftVisible);
			output.Write(activePane);
			return output.BaseStream.Position - position;
		}

		public static long WINDOW2(BinaryWriter output, bool frozen, bool selected)
		{
			long position = output.BaseStream.Position;
			RecordFactory.WriteHeader(output, 574, 18);
			short aDst = BitField16.PutValue(180, 1536, (short)(selected ? 3 : 0));
			aDst = BitField16.PutValue(aDst, 8, (short)(frozen ? 1 : 0));
			output.Write(aDst);
			output.Write((ushort)0);
			output.Write((ushort)0);
			output.Write(64);
			output.Write((ushort)0);
			output.Write((ushort)0);
			output.Write(0);
			return output.BaseStream.Position - position;
		}

		public static long WSBOOL(BinaryWriter output, bool rowSummaryBelow, bool colSummaryToRight)
		{
			long position = output.BaseStream.Position;
			RecordFactory.WriteHeader(output, 129, 2);
			ushort num = 1025;
			if (rowSummaryBelow)
			{
				num = BitField16.PutValue(num, 64, 1);
			}
			if (colSummaryToRight)
			{
				num = BitField16.PutValue(num, 128, 1);
			}
			output.Write(num);
			return output.BaseStream.Position - position;
		}

		public static long HLINK(BinaryWriter output, HyperlinkInfo link)
		{
			long position = output.BaseStream.Position;
			int num = 40 + (link.Label.Length + 1) * 2 + (link.URL.Length + 1) * 2;
			if (!link.IsBookmark)
			{
				num += 16;
			}
			RecordFactory.WriteHeader(output, 440, num);
			link.WriteToStream(output);
			output.Write(new Guid("79EAC9D0-BAF9-11CE-8C82-00AA004BA90B").ToByteArray(), 0, 16);
			output.Write(2u);
			if (link.IsBookmark)
			{
				output.Write(28u);
			}
			else
			{
				output.Write(23u);
			}
			output.Write((uint)(link.Label.Length + 1));
			byte[] bytes = RecordFactory.m_uniEncoding.GetBytes(link.Label);
			output.Write(bytes, 0, bytes.Length);
			output.Write((ushort)0);
			if (!link.IsBookmark)
			{
				output.Write(new Guid("79EAC9E0-BAF9-11CE-8C82-00AA004BA90B").ToByteArray(), 0, 16);
			}
			if (link.IsBookmark)
			{
				output.Write((uint)(link.URL.Length + 1));
			}
			else
			{
				output.Write((uint)((link.URL.Length + 1) * 2));
			}
			bytes = RecordFactory.m_uniEncoding.GetBytes(link.URL);
			output.Write(bytes, 0, bytes.Length);
			output.Write((ushort)0);
			return output.BaseStream.Position - position;
		}

		public static long BACKGROUNDIMAGE(BinaryWriter output, Stream imageStream, ushort pictureWidth, ushort pictureHeight)
		{
			if (imageStream != null && pictureWidth >= 0 && pictureHeight >= 0)
			{
				long position = output.BaseStream.Position;
				int num = pictureWidth * 3;
				int num2 = num % 4;
				if (num2 > 0)
				{
					num2 = 4 - num2;
				}
				int num3 = num2 * pictureHeight;
				uint num4 = (uint)(imageStream.Length + num3 + 12);
				imageStream.Position = 0L;
				uint num5 = 0u;
				uint num6 = 0u;
				uint num7 = num4 + 8;
				if (num7 > 8212)
				{
					num5 = num7 / 8212u;
					num6 = num7 - num5 * 8212;
					num7 = 8212u;
				}
				RecordFactory.WriteHeader(output, 233, (int)num7);
				output.Write((ushort)9);
				output.Write((ushort)1);
				output.Write(num4);
				output.Write((ushort)12);
				output.Write((ushort)0);
				output.Write(pictureWidth);
				output.Write(pictureHeight);
				output.Write((ushort)1);
				output.Write((ushort)24);
				if (num5 == 0)
				{
					byte[] array = new byte[imageStream.Length];
					imageStream.Read(array, 0, array.Length);
					output.Write(array, 0, array.Length);
				}
				else
				{
					byte[] array2 = new byte[8192];
					imageStream.Read(array2, 0, array2.Length);
					output.Write(array2, 0, array2.Length);
					for (int i = 0; i < num5; i++)
					{
						int num8 = 8212;
						if (i == num5 - 1)
						{
							num8 = (ushort)num6;
						}
						RecordFactory.WriteHeader(output, 60, num8);
						byte[] array3 = new byte[num8];
						imageStream.Read(array3, 0, array3.Length);
						output.Write(array3, 0, array3.Length);
					}
				}
				return output.BaseStream.Position - position;
			}
			return 0L;
		}

		public static long SETUP(BinaryWriter output, ushort paperSize, bool isPortrait, double headerMargin, double footerMargin)
		{
			long position = output.BaseStream.Position;
			RecordFactory.WriteHeader(output, 161, 34);
			output.Write(paperSize);
			output.Write((ushort)100);
			output.Write((ushort)1);
			output.Write((ushort)1);
			output.Write((ushort)1);
			output.Write((ushort)(isPortrait ? 2 : 0));
			output.Write((ushort)0);
			output.Write((ushort)0);
			output.Write(headerMargin);
			output.Write(footerMargin);
			output.Write((ushort)1);
			return output.BaseStream.Position - position;
		}

		public static long GUTS(BinaryWriter output, byte maxRowOutline, byte maxColOutline)
		{
			long position = output.BaseStream.Position;
			RecordFactory.WriteHeader(output, 128, 8);
			if (maxRowOutline > 0)
			{
				maxRowOutline = (byte)(maxRowOutline + 1);
			}
			if (maxColOutline > 0)
			{
				maxColOutline = (byte)(maxColOutline + 1);
			}
			output.Write((ushort)(15 * maxRowOutline));
			output.Write((ushort)(15 * maxColOutline));
			output.Write((ushort)maxRowOutline);
			output.Write((ushort)maxColOutline);
			return output.BaseStream.Position - position;
		}

		public static long NAME_PRINTTITLE(BinaryWriter output, PrintTitleInfo printTitle)
		{
			long position = output.BaseStream.Position;
			RecordFactory.WriteHeader(output, 24, 27);
			output.Write(BitConverter.GetBytes((ushort)32));
			output.Write((byte)0);
			output.Write((byte)1);
			output.Write(BitConverter.GetBytes((ushort)11));
			output.Write(BitConverter.GetBytes((ushort)0));
			output.Write(BitConverter.GetBytes(printTitle.CurrentSheetIndex));
			output.Write((byte)0);
			output.Write((byte)0);
			output.Write((byte)0);
			output.Write((byte)0);
			output.Write((byte)0);
			output.Write((byte)7);
			output.Write((byte)59);
			output.Write(BitConverter.GetBytes(printTitle.ExternSheetIndex));
			output.Write(BitConverter.GetBytes(printTitle.FirstRow));
			output.Write(BitConverter.GetBytes(printTitle.LastRow));
			output.Write(BitConverter.GetBytes((ushort)0));
			output.Write(BitConverter.GetBytes((ushort)255));
			return output.BaseStream.Position - position;
		}

		public static long SUPBOOK(BinaryWriter output, ushort cTab)
		{
			long position = output.BaseStream.Position;
			RecordFactory.WriteHeader(output, 430, 4);
			output.Write(BitConverter.GetBytes(cTab));
			output.Write((byte)1);
			output.Write((byte)4);
			return output.BaseStream.Position - position;
		}

		public static long EXTERNSHEET(BinaryWriter output, ExternSheetInfo externSheetInfo)
		{
			long position = output.BaseStream.Position;
			int num = externSheetInfo.XTIStructures.Count;
			RecordFactory.WriteHeader(output, 23, 6 * Math.Min(num, 1370) + 2);
			output.Write(BitConverter.GetBytes((ushort)num));
			int num2 = 0;
			foreach (ExternSheetInfo.XTI xTIStructure in externSheetInfo.XTIStructures)
			{
				output.Write(xTIStructure.SupBookIndex);
				output.Write(xTIStructure.FirstTab);
				output.Write(xTIStructure.LastTab);
				num2++;
				if (num2 == 1370)
				{
					num -= 1370;
					RecordFactory.WriteHeader(output, 60, 6 * Math.Min(num, 1370));
					num2 = 0;
				}
			}
			return output.BaseStream.Position - position;
		}

		public static long MARGINS(BinaryWriter output, double topMargin, double bottomMargin, double leftMargin, double rightMargin)
		{
			long position = output.BaseStream.Position;
			RecordFactory.WriteHeader(output, 40, 8);
			output.Write(topMargin);
			RecordFactory.WriteHeader(output, 41, 8);
			output.Write(bottomMargin);
			RecordFactory.WriteHeader(output, 38, 8);
			output.Write(leftMargin);
			RecordFactory.WriteHeader(output, 39, 8);
			output.Write(rightMargin);
			return output.BaseStream.Position - position;
		}
	}
}
