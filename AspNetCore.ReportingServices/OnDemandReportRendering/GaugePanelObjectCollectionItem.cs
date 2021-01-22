namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class GaugePanelObjectCollectionItem
	{
		protected BaseInstance m_instance;

		public virtual void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}
	}
}
