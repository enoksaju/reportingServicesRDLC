namespace AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing
{
	public interface IDataCorrelation
	{
		bool NextCorrelatedRow();
	}
}
