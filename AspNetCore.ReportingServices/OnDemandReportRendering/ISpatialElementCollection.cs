namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public interface ISpatialElementCollection
	{
		int Count
		{
			get;
		}

		MapSpatialElement GetItem(int index);
	}
}
