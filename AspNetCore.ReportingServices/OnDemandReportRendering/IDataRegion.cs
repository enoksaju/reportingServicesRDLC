namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public interface IDataRegion
	{
		bool HasDataCells
		{
			get;
		}

		IDataRegionRowCollection RowCollection
		{
			get;
		}
	}
}
