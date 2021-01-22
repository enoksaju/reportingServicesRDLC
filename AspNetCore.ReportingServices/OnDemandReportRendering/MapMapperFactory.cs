namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public static class MapMapperFactory
	{
		public static IMapMapper CreateMapMapperInstance(Map map, string defaultFontFamily)
		{
			return new MapMapper(map, defaultFontFamily);
		}
	}
}
