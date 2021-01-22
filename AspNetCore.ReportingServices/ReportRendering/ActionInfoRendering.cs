using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.ReportRendering
{
	public sealed class ActionInfoRendering : MemberBase
	{
		public AspNetCore.ReportingServices.ReportProcessing.Action m_actionInfoDef;

		public ActionInstance m_actionInfoInstance;

		public RenderingContext m_renderingContext;

		public ActionStyle m_style;

		public ActionCollection m_actionCollection;

		public string m_ownerUniqueName;

		public ActionInfoRendering()
			: base(false)
		{
		}
	}
}
