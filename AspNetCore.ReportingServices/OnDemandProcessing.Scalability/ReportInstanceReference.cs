using AspNetCore.ReportingServices.ReportIntermediateFormat;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public class ReportInstanceReference : ScopeInstanceReference, IReference<ReportInstance>, IReference, IStorable, IPersistable
	{
		public ReportInstanceReference()
		{
		}

		public override ObjectType GetObjectType()
		{
			return ObjectType.ReportInstanceReference;
		}

		public new ReportInstance Value()
		{
			return (ReportInstance)base.InternalValue();
		}
	}
}
