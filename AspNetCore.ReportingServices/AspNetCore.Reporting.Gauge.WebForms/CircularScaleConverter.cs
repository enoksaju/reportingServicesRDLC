namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class CircularScaleConverter : CollectionItemTypeConverter
	{
		public CircularScaleConverter()
		{
			base.simpleType = typeof(CircularScale);
		}
	}
}
