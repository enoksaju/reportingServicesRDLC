using AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using System.Diagnostics;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public class RuntimeChartObjReference : RuntimeChartCriObjReference, IReference<RuntimeChartObj>, IReference, IStorable, IPersistable
	{
		public RuntimeChartObjReference()
		{
		}

		public override ObjectType GetObjectType()
		{
			return ObjectType.RuntimeChartObjReference;
		}

		[DebuggerStepThrough]
		public new RuntimeChartObj Value()
		{
			return (RuntimeChartObj)base.InternalValue();
		}
	}
}
