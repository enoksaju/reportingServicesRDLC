using AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using System.Diagnostics;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public class RuntimeDataTablixMemberObjReference : RuntimeMemberObjReference, IReference<RuntimeDataTablixMemberObj>, IReference, IStorable, IPersistable
	{
		public RuntimeDataTablixMemberObjReference()
		{
		}

		public override ObjectType GetObjectType()
		{
			return ObjectType.RuntimeDataTablixMemberObjReference;
		}

		[DebuggerStepThrough]
		public RuntimeDataTablixMemberObj Value()
		{
			return (RuntimeDataTablixMemberObj)base.InternalValue();
		}
	}
}
