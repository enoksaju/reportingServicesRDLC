using System.Text;

namespace AspNetCore.ReportingServices.Rendering.ExcelOpenXmlRenderer
{
	public static class CellPair
	{
		public static string Name(int row, int col)
		{
			return CellPair.ConvertToLetter(col) + (row + 1);
		}

		public static string ConvertToLetter(int col)
		{
			int num;
			for (num = 26; num <= col; num *= 26)
			{
				col -= num;
			}
			StringBuilder stringBuilder = new StringBuilder();
			while (num > 1)
			{
				num /= 26;
				int num2 = col / num;
				stringBuilder.Append((char)(ushort)(num2 + 65));
				col -= num2 * num;
			}
			return stringBuilder.ToString();
		}
	}
}
