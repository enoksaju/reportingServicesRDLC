using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public interface IOnDemandMemberInstanceReference : IOnDemandMemberOwnerInstanceReference, IReference<IOnDemandScopeInstance>, IReference<IOnDemandMemberOwnerInstance>, IReference<IOnDemandMemberInstance>, IReference, IStorable, IPersistable
	{
	}
}
