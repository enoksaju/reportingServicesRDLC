namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class GaugeImageConverter : CollectionItemTypeConverter
	{
		public GaugeImageConverter()
		{
			base.simpleType = typeof(GaugeImage);
		}
	}
}
