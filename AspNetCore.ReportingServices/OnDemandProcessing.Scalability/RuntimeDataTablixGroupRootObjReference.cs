using AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using System.Diagnostics;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public class RuntimeDataTablixGroupRootObjReference : RuntimeGroupRootObjReference, IReference<RuntimeDataTablixGroupRootObj>, IReference, IStorable, IPersistable
	{
		public RuntimeDataTablixGroupRootObjReference()
		{
		}

		public override ObjectType GetObjectType()
		{
			return ObjectType.RuntimeDataTablixGroupRootObjReference;
		}

		[DebuggerStepThrough]
		public new RuntimeDataTablixGroupRootObj Value()
		{
			return (RuntimeDataTablixGroupRootObj)base.InternalValue();
		}
	}
}
