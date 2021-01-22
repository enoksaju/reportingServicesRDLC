namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class CalculatedValueAverageConverter : CollectionItemTypeConverter
	{
		public CalculatedValueAverageConverter()
		{
			base.simpleType = typeof(CalculatedValueAverage);
		}
	}
}
