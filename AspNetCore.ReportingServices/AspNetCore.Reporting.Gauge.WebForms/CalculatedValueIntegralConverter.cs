namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class CalculatedValueIntegralConverter : CollectionItemTypeConverter
	{
		public CalculatedValueIntegralConverter()
		{
			base.simpleType = typeof(CalculatedValueIntegral);
		}
	}
}
