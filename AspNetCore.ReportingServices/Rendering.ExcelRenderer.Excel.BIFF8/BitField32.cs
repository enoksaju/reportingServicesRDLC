namespace AspNetCore.ReportingServices.Rendering.ExcelRenderer.Excel.BIFF8
{
	public static class BitField32
	{
		public static int GetValue(int aValue, int aMask)
		{
			int num = aValue & aMask;
			while ((aMask & 1) != 1)
			{
				num >>= 1;
				aMask >>= 1;
			}
			return num;
		}

		public static int PutValue(int aDst, int aMask, int aValue)
		{
			int num = aMask;
			while ((num & 1) != 1)
			{
				aValue <<= 1;
				num >>= 1;
			}
			aDst &= ~aMask;
			aDst |= aValue;
			return aDst;
		}

		public static uint GetValue(uint aValue, uint aMask)
		{
			uint num = aValue & aMask;
			while ((aMask & 1) != 1)
			{
				num >>= 1;
				aMask >>= 1;
			}
			return num;
		}

		public static uint PutValue(uint aDst, uint aMask, uint aValue)
		{
			uint num = aMask;
			while ((num & 1) != 1)
			{
				aValue <<= 1;
				num >>= 1;
			}
			aDst &= ~aMask;
			aDst |= aValue;
			return aDst;
		}
	}
}
