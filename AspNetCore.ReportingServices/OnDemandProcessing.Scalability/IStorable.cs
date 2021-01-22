using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public interface IStorable : IPersistable
	{
		int Size
		{
			get;
		}
	}
}
