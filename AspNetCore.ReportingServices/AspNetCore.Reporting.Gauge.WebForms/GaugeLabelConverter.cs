namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class GaugeLabelConverter : CollectionItemTypeConverter
	{
		public GaugeLabelConverter()
		{
			base.simpleType = typeof(GaugeLabel);
		}
	}
}
