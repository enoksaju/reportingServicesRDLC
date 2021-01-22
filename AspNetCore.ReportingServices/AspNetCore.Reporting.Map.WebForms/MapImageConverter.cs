namespace AspNetCore.Reporting.Map.WebForms
{
	public class MapImageConverter : CollectionItemTypeConverter
	{
		public MapImageConverter()
		{
			base.simpleType = typeof(MapImage);
		}
	}
}
