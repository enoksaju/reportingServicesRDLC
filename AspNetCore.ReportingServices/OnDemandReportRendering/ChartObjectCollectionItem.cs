namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class ChartObjectCollectionItem<T> where T : BaseInstance
	{
		protected T m_instance;

		public virtual void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}
	}
}
