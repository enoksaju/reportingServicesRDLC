namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public static class ChartMapperFactory
	{
		public static IChartMapper CreateChartMapperInstance(Chart chart, string defaultFontFamily)
		{
			return new ChartMapper(chart, defaultFontFamily);
		}
	}
}
