namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class CalculatedValueConverter : CollectionItemTypeConverter
	{
		public CalculatedValueConverter()
		{
			base.simpleType = typeof(CalculatedValue);
		}
	}
}
