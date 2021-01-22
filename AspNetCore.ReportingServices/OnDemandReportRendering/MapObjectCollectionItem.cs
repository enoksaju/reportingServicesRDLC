namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class MapObjectCollectionItem : IMapObjectCollectionItem
	{
		protected BaseInstance m_instance;

		void IMapObjectCollectionItem.SetNewContext()
		{
			this.SetNewContext();
		}

		public virtual void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}
	}
}
