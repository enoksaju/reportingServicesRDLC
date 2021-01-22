using AspNetCore.ReportingServices.OnDemandProcessing.Scalability;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.OnDemandProcessing
{
	public interface IOnDemandMemberInstance : IOnDemandMemberOwnerInstance, IOnDemandScopeInstance, IStorable, IPersistable
	{
		List<object> GroupExprValues
		{
			get;
		}

		IOnDemandMemberInstanceReference GetNextMemberInstance();

		IOnDemandScopeInstance GetCellInstance(IOnDemandMemberInstanceReference outerGroupInstanceRef, out IReference<IOnDemandScopeInstance> cellRef);
	}
}
