using AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public class RuntimeMemberObjReference : Reference<RuntimeMemberObj>
	{
		public RuntimeMemberObjReference()
		{
		}

		public override ObjectType GetObjectType()
		{
			return ObjectType.RuntimeMemberObjReference;
		}
	}
}
