namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public static class GaugeMapperFactory
	{
		public static IGaugeMapper CreateGaugeMapperInstance(GaugePanel gaugePanel, string defaultFontFamily)
		{
			return new GaugeMapper(gaugePanel, defaultFontFamily);
		}
	}
}
