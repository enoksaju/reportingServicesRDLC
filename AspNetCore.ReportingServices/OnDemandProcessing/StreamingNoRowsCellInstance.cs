using AspNetCore.ReportingServices.ReportIntermediateFormat;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;

namespace AspNetCore.ReportingServices.OnDemandProcessing
{
	[PersistedWithinRequestOnly]
	[SkipStaticValidation]
	public class StreamingNoRowsCellInstance : StreamingNoRowsScopeInstanceBase
	{
		public StreamingNoRowsCellInstance(OnDemandProcessingContext odpContext, IRIFReportDataScope cell)
			: base(odpContext, cell)
		{
		}

		public override ObjectType GetObjectType()
		{
			return ObjectType.StreamingNoRowsCellInstance;
		}
	}
}
