namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class CircularRangeConverter : CollectionItemTypeConverter
	{
		public CircularRangeConverter()
		{
			base.simpleType = typeof(CircularRange);
		}
	}
}
