using AspNetCore.ReportingServices.OnDemandProcessing.Scalability;
using AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing;
using AspNetCore.ReportingServices.ReportIntermediateFormat;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;

namespace AspNetCore.ReportingServices.OnDemandProcessing
{
	public interface IOnDemandScopeInstance : IStorable, IPersistable
	{
		bool IsNoRows
		{
			get;
		}

		bool IsMostRecentlyCreatedScopeInstance
		{
			get;
		}

		bool HasUnProcessedServerAggregate
		{
			get;
		}

		void SetupEnvironment();

		IOnDemandMemberOwnerInstanceReference GetDataRegionInstance(DataRegion rifDataRegion);

		IReference<IDataCorrelation> GetIdcReceiver(IRIFReportDataScope scope);
	}
}
