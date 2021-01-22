using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public interface IOnDemandMemberOwnerInstanceReference : IReference<IOnDemandScopeInstance>, IReference<IOnDemandMemberOwnerInstance>, IReference, IStorable, IPersistable
	{
	}
}
