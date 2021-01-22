using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.ReportRendering
{
	public sealed class ActionRendering : MemberBase
	{
		public ActionItem m_actionDef;

		public ReportUrl m_actionURL;

		public ActionItemInstance m_actionInstance;

		public RenderingContext m_renderingContext;

		public string m_drillthroughId;

		public ActionRendering()
			: base(false)
		{
		}
	}
}
