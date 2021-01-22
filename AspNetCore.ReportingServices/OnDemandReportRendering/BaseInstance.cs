namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class BaseInstance
	{
		public IReportScope m_reportScope;

		public virtual IReportScopeInstance ReportScopeInstance
		{
			get
			{
				return this.m_reportScope.ReportScopeInstance;
			}
		}

		public BaseInstance(IReportScope reportScope)
		{
			this.m_reportScope = reportScope;
		}

		public virtual void SetNewContext()
		{
			this.ResetInstanceCache();
		}

		protected abstract void ResetInstanceCache();
	}
}
