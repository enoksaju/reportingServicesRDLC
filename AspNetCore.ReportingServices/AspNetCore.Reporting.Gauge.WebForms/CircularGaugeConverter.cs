namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class CircularGaugeConverter : CollectionItemTypeConverter
	{
		public CircularGaugeConverter()
		{
			base.simpleType = typeof(CircularGauge);
		}
	}
}
