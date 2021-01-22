using AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using System.Diagnostics;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public class RuntimeGaugePanelObjReference : RuntimeChartCriObjReference, IReference<RuntimeGaugePanelObj>, IReference, IStorable, IPersistable
	{
		public RuntimeGaugePanelObjReference()
		{
		}

		public override ObjectType GetObjectType()
		{
			return ObjectType.RuntimeGaugePanelObjReference;
		}

		[DebuggerStepThrough]
		public new RuntimeGaugePanelObj Value()
		{
			return (RuntimeGaugePanelObj)base.InternalValue();
		}
	}
}
