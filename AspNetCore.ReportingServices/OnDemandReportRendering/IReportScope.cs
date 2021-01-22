using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public interface IReportScope
	{
		IReportScopeInstance ReportScopeInstance
		{
			get;
		}

		IRIFReportScope RIFReportScope
		{
			get;
		}
	}
}
