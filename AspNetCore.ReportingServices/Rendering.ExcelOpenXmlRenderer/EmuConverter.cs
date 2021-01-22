namespace AspNetCore.ReportingServices.Rendering.ExcelOpenXmlRenderer
{
	public static class EmuConverter
	{
		private const uint EmusPerPoint = 12700u;

		public static long PointsToEmus(long points)
		{
			return points * 12700;
		}
	}
}
