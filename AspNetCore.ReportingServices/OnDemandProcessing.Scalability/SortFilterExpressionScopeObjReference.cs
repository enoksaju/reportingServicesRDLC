using AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using System.Diagnostics;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public class SortFilterExpressionScopeObjReference : Reference<IHierarchyObj>, IReference<RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj>, IReference, IStorable, IPersistable
	{
		public SortFilterExpressionScopeObjReference()
		{
		}

		public override ObjectType GetObjectType()
		{
			return ObjectType.SortFilterExpressionScopeObjReference;
		}

		[DebuggerStepThrough]
		public RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj Value()
		{
			return (RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj)base.InternalValue();
		}
	}
}
