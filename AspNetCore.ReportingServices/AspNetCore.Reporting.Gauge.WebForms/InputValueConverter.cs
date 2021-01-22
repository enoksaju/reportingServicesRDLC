namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class InputValueConverter : CollectionItemTypeConverter
	{
		public InputValueConverter()
		{
			base.simpleType = typeof(InputValue);
		}
	}
}
