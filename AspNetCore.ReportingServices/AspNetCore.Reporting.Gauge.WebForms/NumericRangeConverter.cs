namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class NumericRangeConverter : CollectionItemTypeConverter
	{
		public NumericRangeConverter()
		{
			base.simpleType = typeof(NumericRange);
		}
	}
}
