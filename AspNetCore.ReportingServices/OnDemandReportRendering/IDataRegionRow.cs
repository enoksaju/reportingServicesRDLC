namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public interface IDataRegionRow
	{
		int Count
		{
			get;
		}

		IDataRegionCell GetIfExists(int index);
	}
}
