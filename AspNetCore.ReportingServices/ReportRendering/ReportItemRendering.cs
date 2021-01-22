using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.ReportRendering
{
	public sealed class ReportItemRendering : MemberBase
	{
		public RenderingContext m_renderingContext;

		public AspNetCore.ReportingServices.ReportProcessing.ReportItem m_reportItemDef;

		public ReportItemInstance m_reportItemInstance;

		public ReportItemInstanceInfo m_reportItemInstanceInfo;

		public MatrixHeadingInstance m_headingInstance;

		public ReportItemRendering()
			: base(false)
		{
		}
	}
}
