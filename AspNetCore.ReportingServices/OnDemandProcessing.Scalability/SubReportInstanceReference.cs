using AspNetCore.ReportingServices.ReportIntermediateFormat;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public class SubReportInstanceReference : ScopeInstanceReference, IReference<SubReportInstance>, IReference, IStorable, IPersistable
	{
		public SubReportInstanceReference()
		{
		}

		public override ObjectType GetObjectType()
		{
			return ObjectType.SubReportInstanceReference;
		}

		public new SubReportInstance Value()
		{
			return (SubReportInstance)base.InternalValue();
		}
	}
}
