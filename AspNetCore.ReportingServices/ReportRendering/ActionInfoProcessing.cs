using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.ReportRendering
{
	public sealed class ActionInfoProcessing : MemberBase
	{
		public ActionStyle m_style;

		public ActionCollection m_actionCollection;

		public DataValueInstanceList m_sharedStyles;

		public DataValueInstanceList m_nonSharedStyles;

		public ActionInfoProcessing()
			: base(true)
		{
		}

		public ActionInfoProcessing DeepClone()
		{
			Global.Tracer.Assert(this.m_sharedStyles == null && null == this.m_nonSharedStyles);
			ActionInfoProcessing actionInfoProcessing = new ActionInfoProcessing();
			if (this.m_style != null)
			{
				((StyleBase)this.m_style).ExtractRenderStyles(out actionInfoProcessing.m_sharedStyles, out actionInfoProcessing.m_nonSharedStyles);
			}
			if (this.m_actionCollection != null)
			{
				actionInfoProcessing.m_actionCollection = this.m_actionCollection.DeepClone();
			}
			return actionInfoProcessing;
		}
	}
}
