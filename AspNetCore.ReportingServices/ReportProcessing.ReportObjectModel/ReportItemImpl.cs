namespace AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel
{
	public abstract class ReportItemImpl : ReportItem
	{
		public AspNetCore.ReportingServices.ReportProcessing.ReportItem m_item;

		public ReportRuntime m_reportRT;

		public IErrorContext m_iErrorContext;

		public ReportProcessing.IScope m_scope;

		public string Name
		{
			get
			{
				return this.m_item.Name;
			}
		}

		public ReportProcessing.IScope Scope
		{
			set
			{
				this.m_scope = value;
			}
		}

		public ReportItemImpl(AspNetCore.ReportingServices.ReportProcessing.ReportItem itemDef, ReportRuntime reportRT, IErrorContext iErrorContext)
		{
			Global.Tracer.Assert(null != itemDef, "(null != itemDef)");
			Global.Tracer.Assert(null != reportRT, "(null != reportRT)");
			Global.Tracer.Assert(null != iErrorContext, "(null != iErrorContext)");
			this.m_item = itemDef;
			this.m_reportRT = reportRT;
			this.m_iErrorContext = iErrorContext;
		}
	}
}
