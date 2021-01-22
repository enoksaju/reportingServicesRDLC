using AspNetCore.ReportingServices.ReportIntermediateFormat;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public class ScopeInstanceReference : Reference<ScopeInstance>
	{
		public ScopeInstanceReference()
		{
		}

		public override ObjectType GetObjectType()
		{
			return ObjectType.ScopeInstanceReference;
		}

		public ScopeInstance Value()
		{
			return (ScopeInstance)base.InternalValue();
		}
	}
}
