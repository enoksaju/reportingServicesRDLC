using AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using System.Diagnostics;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public class RuntimeCriObjReference : RuntimeChartCriObjReference, IReference<RuntimeCriObj>, IReference, IStorable, IPersistable
	{
		public RuntimeCriObjReference()
		{
		}

		public override ObjectType GetObjectType()
		{
			return ObjectType.RuntimeCriObjReference;
		}

		[DebuggerStepThrough]
		public new RuntimeCriObj Value()
		{
			return (RuntimeCriObj)base.InternalValue();
		}
	}
}
