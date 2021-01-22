namespace AspNetCore.Reporting.Map.WebForms
{
	public class MapLabelConverter : CollectionItemTypeConverter
	{
		public MapLabelConverter()
		{
			base.simpleType = typeof(MapLabel);
		}
	}
}
