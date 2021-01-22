using System;
using System.Globalization;
using System.IO;

namespace AspNetCore.ReportingServices.Rendering.ExcelRenderer.Excel.BIFF8
{
	public sealed class LittleEndianHelper
	{
		public const int BYTE_SIZE = 1;

		public const int SHORT_SIZE = 2;

		public const int CHAR_SIZE = 2;

		public const int INT_SIZE = 4;

		public const int LONG_SIZE = 8;

		public const int FLOAT_SIZE = 4;

		public const int DOUBLE_SIZE = 8;

		public static readonly short UBYTE_MAX = 511;

		public static readonly int USHORT_MAX = 65535;

		public static readonly long UINT_MAX = 4294967295L;

		public static short ReadShort(byte[] aBuf, int aOff)
		{
			return (short)((aBuf[aOff + 1] & 0xFF) << 8 | (aBuf[aOff] & 0xFF));
		}

		public static char readChar(byte[] aBuf, int aOff)
		{
			return (char)(ushort)LittleEndianHelper.ReadShort(aBuf, aOff);
		}

		public static int ReadInt(byte[] aBuf, int aOff)
		{
			int num = 0;
			for (int i = 0; i < 32; i += 8)
			{
				num |= (int)((long)(aBuf[aOff + i / 8] & 0xFF) << i);
			}
			return num;
		}

		public static long ReadLong(byte[] aBuf, int aOff)
		{
			long num = 0L;
			for (int i = 0; i < 64; i += 8)
			{
				num |= (long)(aBuf[aOff + i / 8] & 0xFF) << i;
			}
			return num;
		}

		public static float ReadFloat(byte[] aBuf, int aOff)
		{
			return Convert.ToSingle(LittleEndianHelper.ReadInt(aBuf, aOff));
		}

		public static double ReadDouble(byte[] aBuf, int aOff)
		{
			return Convert.ToDouble(LittleEndianHelper.ReadLong(aBuf, aOff));
		}

		public static short ReadByteU(byte[] aBuf, int aOff)
		{
			return (short)(aBuf[aOff] & 0xFF);
		}

		public static int ReadShortU(byte[] aBuf, int aOff)
		{
			return LittleEndianHelper.ReadShort(aBuf, aOff) & 0xFFFF;
		}

		public static long ReadIntU(byte[] aBuf, int aOff)
		{
			return (long)LittleEndianHelper.ReadInt(aBuf, aOff) & -1L;
		}

		public static double ReadFixed32(byte[] aBuff, int aOff)
		{
			int num = LittleEndianHelper.ReadInt(aBuff, aOff);
			bool flag = false;
			if (num < 0)
			{
				num *= -1;
				flag = true;
			}
			double num2 = (double)LittleEndianHelper.URShift(num, 16);
			num &= 0xFFFF;
			num2 += (double)num / 65536.0;
			if (flag)
			{
				num2 *= -1.0;
			}
			return num2;
		}

		public static double ReadFixed32U(byte[] aBuff, int aOff)
		{
			long num = LittleEndianHelper.ReadIntU(aBuff, aOff);
			double num2 = (double)LittleEndianHelper.URShift(num, 16);
			num &= 0xFFFF;
			return num2 + (double)num / 65536.0;
		}

		public static void WriteShort(short aVal, byte[] aBuf, int aOff)
		{
			aBuf[aOff + 1] = (byte)(aVal >> 8);
			aBuf[aOff] = (byte)(aVal & 0xFF);
		}

		public static void WriteShort(ushort aVal, byte[] aBuf, int aOff)
		{
			aBuf[aOff + 1] = (byte)(aVal >> 8);
			aBuf[aOff] = (byte)(aVal & 0xFF);
		}

		public static void WriteInt(int aVal, byte[] aBuf, int aOff)
		{
			for (int i = 0; i < 32; i += 8)
			{
				aBuf[aOff + i / 8] = (byte)(aVal >> i & 0xFF);
			}
		}

		public static void WriteLong(long aVal, byte[] aBuf, int aOff)
		{
			for (int i = 0; i < 64; i += 8)
			{
				aBuf[aOff + i / 8] = (byte)(aVal >> i & 0xFF);
			}
		}

		public static void WriteFloat(float aVal, byte[] aBuf, int aOff)
		{
			LittleEndianHelper.WriteInt(Convert.ToInt32(aVal), aBuf, aOff);
		}

		public static void WriteDouble(double aVal, byte[] aBuf, int aOff)
		{
			LittleEndianHelper.WriteLong(Convert.ToInt64(aVal), aBuf, aOff);
		}

		public static void writeByteU(short aVal, byte[] aBuf, int aOff)
		{
			if (aVal > LittleEndianHelper.UBYTE_MAX)
			{
				throw new IOException(ExcelRenderRes.MaxValueExceeded(LittleEndianHelper.UBYTE_MAX.ToString(CultureInfo.InvariantCulture)));
			}
			aBuf[aOff] = (byte)(aVal & 0xFF);
		}

		public static void WriteShortU(int aVal, byte[] aBuf, int aOff)
		{
			if (aVal > LittleEndianHelper.USHORT_MAX)
			{
				throw new IOException(ExcelRenderRes.MaxValueExceeded(LittleEndianHelper.USHORT_MAX.ToString(CultureInfo.InvariantCulture)));
			}
			LittleEndianHelper.WriteShort((short)(aVal & 0xFFFF), aBuf, aOff);
		}

		public static void WriteIntU(long aVal, byte[] aBuf, int aOff)
		{
			if (aVal > LittleEndianHelper.UINT_MAX)
			{
				throw new IOException(ExcelRenderRes.MaxValueExceeded(LittleEndianHelper.UINT_MAX.ToString(CultureInfo.InvariantCulture)));
			}
			LittleEndianHelper.WriteInt((int)(aVal & -1), aBuf, aOff);
		}

		public static void WriteFixed32(double aVal, byte[] aBuff, int aOff)
		{
			bool flag = false;
			if (aVal < 0.0)
			{
				aVal *= -1.0;
				flag = true;
			}
			double num = Math.Floor(aVal);
			double num2 = aVal - num;
			num2 *= 65536.0;
			int num3 = (int)num << 16;
			num3 += (int)num2;
			if (flag)
			{
				num3 *= -1;
			}
			LittleEndianHelper.WriteInt(num3, aBuff, aOff);
		}

		public static void WriteFixed32U(double aVal, byte[] aBuff, int aOff)
		{
			double num = Math.Floor(aVal);
			double num2 = aVal - num;
			num2 *= 65536.0;
			long num3 = (long)num << 16;
			num3 += (long)num2;
			LittleEndianHelper.WriteIntU(num3, aBuff, aOff);
		}

		public static void WriteShortU(Stream aOut, int aVal)
		{
			for (int i = 0; i < 2; i++)
			{
				aOut.WriteByte((byte)(aVal >> 8 * i & 0xFF));
			}
		}

		public static void WriteIntU(Stream aOut, long aVal)
		{
			for (int i = 0; i < 4; i++)
			{
				aOut.WriteByte((byte)(aVal >> 8 * i & 0xFF));
			}
		}

		public static int URShift(int number, int bits)
		{
			if (number >= 0)
			{
				return number >> bits;
			}
			return (number >> bits) + (2 << ~bits);
		}

		public static int URShift(int number, long bits)
		{
			return LittleEndianHelper.URShift(number, (int)bits);
		}

		public static long URShift(long number, int bits)
		{
			if (number >= 0)
			{
				return number >> bits;
			}
			return (number >> bits) + (2L << ~bits);
		}

		public static long URShift(long number, long bits)
		{
			return LittleEndianHelper.URShift(number, (int)bits);
		}
	}
}
