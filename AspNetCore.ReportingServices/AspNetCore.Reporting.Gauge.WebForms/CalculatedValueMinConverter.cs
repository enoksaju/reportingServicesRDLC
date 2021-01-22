namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class CalculatedValueMinConverter : CollectionItemTypeConverter
	{
		public CalculatedValueMinConverter()
		{
			base.simpleType = typeof(CalculatedValueMin);
		}
	}
}
