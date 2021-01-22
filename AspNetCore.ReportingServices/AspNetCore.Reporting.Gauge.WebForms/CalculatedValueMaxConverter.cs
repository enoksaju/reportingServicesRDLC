namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class CalculatedValueMaxConverter : CollectionItemTypeConverter
	{
		public CalculatedValueMaxConverter()
		{
			base.simpleType = typeof(CalculatedValueMax);
		}
	}
}
