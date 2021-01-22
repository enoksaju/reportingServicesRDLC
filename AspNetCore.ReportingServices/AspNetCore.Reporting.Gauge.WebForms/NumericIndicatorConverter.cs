namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class NumericIndicatorConverter : CollectionItemTypeConverter
	{
		public NumericIndicatorConverter()
		{
			base.simpleType = typeof(NumericIndicator);
		}
	}
}
