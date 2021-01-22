using AspNetCore.ReportingServices.OnDemandProcessing.Scalability;
using AspNetCore.ReportingServices.ReportIntermediateFormat;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;

namespace AspNetCore.ReportingServices.OnDemandProcessing
{
	public interface IOnDemandMemberOwnerInstance : IOnDemandScopeInstance, IStorable, IPersistable
	{
		IOnDemandMemberInstanceReference GetFirstMemberInstance(ReportHierarchyNode rifMember);
	}
}
