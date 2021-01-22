using AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using System.Diagnostics;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public class RuntimeGroupLeafObjReference : RuntimeGroupObjReference, IReference<RuntimeGroupLeafObj>, IReference, IStorable, IPersistable
	{
		public RuntimeGroupLeafObjReference()
		{
		}

		public override ObjectType GetObjectType()
		{
			return ObjectType.RuntimeGroupLeafObjReference;
		}

		[DebuggerStepThrough]
		public new RuntimeGroupLeafObj Value()
		{
			return (RuntimeGroupLeafObj)base.InternalValue();
		}
	}
}
