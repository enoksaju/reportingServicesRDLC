namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class LinearScaleConverter : CollectionItemTypeConverter
	{
		public LinearScaleConverter()
		{
			base.simpleType = typeof(LinearScale);
		}
	}
}
