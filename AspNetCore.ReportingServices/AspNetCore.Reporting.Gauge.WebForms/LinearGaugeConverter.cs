namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class LinearGaugeConverter : CollectionItemTypeConverter
	{
		public LinearGaugeConverter()
		{
			base.simpleType = typeof(LinearGauge);
		}
	}
}
