namespace AspNetCore.Reporting.Map.WebForms
{
	public class NamedImageConverter : CollectionItemTypeConverter
	{
		public NamedImageConverter()
		{
			base.simpleType = typeof(NamedImage);
		}
	}
}
