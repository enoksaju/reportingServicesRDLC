using AspNetCore.ReportingServices.OnDemandReportRendering;

namespace AspNetCore.ReportingServices.Rendering.HPBProcessing
{
	public sealed class SelectiveRenderingCannotFindReportItemException : ReportRenderingException
	{
		public SelectiveRenderingCannotFindReportItemException(string name)
			: base(HPBRes.ReportItemCannotBeFound(name), false)
		{
		}
	}
}
