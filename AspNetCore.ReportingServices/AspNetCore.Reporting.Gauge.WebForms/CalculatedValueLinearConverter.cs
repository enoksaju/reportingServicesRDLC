namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class CalculatedValueLinearConverter : CollectionItemTypeConverter
	{
		public CalculatedValueLinearConverter()
		{
			base.simpleType = typeof(CalculatedValueLinear);
		}
	}
}
