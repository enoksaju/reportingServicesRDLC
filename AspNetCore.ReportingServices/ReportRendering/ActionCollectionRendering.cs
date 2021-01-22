using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.ReportRendering
{
	public sealed class ActionCollectionRendering : MemberBase
	{
		public ActionItemList m_actionList;

		public ActionItemInstanceList m_actionInstanceList;

		public RenderingContext m_renderingContext;

		public Action[] m_actions;

		public string m_ownerUniqueName;

		public ActionCollectionRendering()
			: base(false)
		{
		}
	}
}
