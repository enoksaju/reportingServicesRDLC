using AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using System.Diagnostics;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public class RuntimeTablixCellReference : RuntimeCellReference, IReference<RuntimeTablixCell>, IReference, IStorable, IPersistable
	{
		public RuntimeTablixCellReference()
		{
		}

		public override ObjectType GetObjectType()
		{
			return ObjectType.RuntimeTablixCellReference;
		}

		[DebuggerStepThrough]
		public new RuntimeTablixCell Value()
		{
			return (RuntimeTablixCell)base.InternalValue();
		}
	}
}
