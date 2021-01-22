using AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using System.Diagnostics;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public class RuntimeChartCriCellReference : RuntimeCellReference, IReference<RuntimeChartCriCell>, IReference, IStorable, IPersistable
	{
		public RuntimeChartCriCellReference()
		{
		}

		public override ObjectType GetObjectType()
		{
			return ObjectType.RuntimeChartCriCellReference;
		}

		[DebuggerStepThrough]
		public new RuntimeChartCriCell Value()
		{
			return (RuntimeChartCriCell)base.InternalValue();
		}
	}
}
