namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class VisibilityInstance : BaseInstance
	{
		protected bool m_cachedStartHidden;

		protected bool m_startHiddenValue;

		protected bool m_cachedCurrentlyHidden;

		protected bool m_currentlyHiddenValue;

		public abstract bool CurrentlyHidden
		{
			get;
		}

		public abstract bool StartHidden
		{
			get;
		}

		public VisibilityInstance(IReportScope reportScope)
			: base(reportScope)
		{
		}

		protected override void ResetInstanceCache()
		{
			this.m_cachedStartHidden = false;
			this.m_cachedCurrentlyHidden = false;
		}
	}
}
