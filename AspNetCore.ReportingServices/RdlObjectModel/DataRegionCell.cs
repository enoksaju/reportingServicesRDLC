namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public abstract class DataRegionCell : ReportObject
	{
		public DataRegionCell()
		{
		}

		public DataRegionCell(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
