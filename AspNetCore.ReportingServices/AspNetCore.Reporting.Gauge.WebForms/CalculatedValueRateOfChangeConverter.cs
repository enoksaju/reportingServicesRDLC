namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class CalculatedValueRateOfChangeConverter : CollectionItemTypeConverter
	{
		public CalculatedValueRateOfChangeConverter()
		{
			base.simpleType = typeof(CalculatedValueRateOfChange);
		}
	}
}
