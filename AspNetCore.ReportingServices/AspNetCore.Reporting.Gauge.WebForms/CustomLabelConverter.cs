namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class CustomLabelConverter : CollectionItemTypeConverter
	{
		public CustomLabelConverter()
		{
			base.simpleType = typeof(CustomLabel);
		}
	}
}
