namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public interface IDataRegionRowCollection
	{
		int Count
		{
			get;
		}

		IDataRegionRow GetIfExists(int index);
	}
}
