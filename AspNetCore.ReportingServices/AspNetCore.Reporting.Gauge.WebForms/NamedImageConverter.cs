namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class NamedImageConverter : CollectionItemTypeConverter
	{
		public NamedImageConverter()
		{
			base.simpleType = typeof(NamedImage);
		}
	}
}
