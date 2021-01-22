using AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using System.Diagnostics;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public class RuntimeCellReference : IScopeReference, IReference<RuntimeCell>, IReference<IOnDemandScopeInstance>, IReference, IStorable, IPersistable
	{
		public RuntimeCellReference()
		{
		}

		public override ObjectType GetObjectType()
		{
			return ObjectType.RuntimeCellReference;
		}

		[DebuggerStepThrough]
		public new RuntimeCell Value()
		{
			return (RuntimeCell)base.InternalValue();
		}

		[DebuggerStepThrough]
		IOnDemandScopeInstance IReference<IOnDemandScopeInstance>.Value()
		{
			return (IOnDemandScopeInstance)base.InternalValue();
		}
	}
}
