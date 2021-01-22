namespace AspNetCore.ReportingServices.Rendering.Utilities
{
	public class LittleEndian
	{
		public static void PutInt(byte[] data, int offset, int val)
		{
			LittleEndian.PutNumber(data, offset, val, 4);
		}

		public static void PutInt(byte[] data, int val)
		{
			LittleEndian.PutInt(data, 0, val);
		}

		public static void PutUShort(byte[] data, int offset, ushort val)
		{
			LittleEndian.PutNumber(data, offset, val, 2);
		}

		public static void PutUShort(byte[] data, ushort val)
		{
			LittleEndian.PutUShort(data, 0, val);
		}

		public static void PutShort(byte[] data, int offset, short val)
		{
			LittleEndian.PutNumber(data, offset, val, 2);
		}

		public static void PutShort(byte[] data, short val)
		{
			LittleEndian.PutShort(data, 0, val);
		}

		public static int getInt(byte[] data)
		{
			return LittleEndian.getInt(data, 0);
		}

		public static int getInt(byte[] data, int offset)
		{
			return (int)LittleEndian.getNumber(data, offset, 4);
		}

		public static ushort getUShort(byte[] data, int offset)
		{
			return (ushort)LittleEndian.getNumber(data, offset, 2);
		}

		public static short getShort(byte[] data, int offset)
		{
			return (short)LittleEndian.getNumber(data, offset, 2);
		}

		private static long getNumber(byte[] data, int offset, int size)
		{
			long num = 0L;
			for (int num2 = offset + size - 1; num2 >= offset; num2--)
			{
				num <<= 8;
				num |= (uint)(0xFF & data[num2]);
			}
			return num;
		}

		private static void PutNumber(byte[] data, int offset, long val, int size)
		{
			int num = size + offset;
			long num2 = val;
			for (int i = offset; i < num; i++)
			{
				data[i] = (byte)(num2 & 0xFF);
				num2 >>= 8;
			}
		}
	}
}
