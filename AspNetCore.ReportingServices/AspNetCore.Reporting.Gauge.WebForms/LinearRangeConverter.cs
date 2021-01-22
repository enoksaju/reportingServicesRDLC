namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class LinearRangeConverter : CollectionItemTypeConverter
	{
		public LinearRangeConverter()
		{
			base.simpleType = typeof(LinearRange);
		}
	}
}
