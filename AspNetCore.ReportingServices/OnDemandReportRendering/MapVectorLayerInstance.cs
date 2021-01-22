namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class MapVectorLayerInstance : MapLayerInstance
	{
		private MapVectorLayer m_defObject;

		public MapVectorLayerInstance(MapVectorLayer defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
		}
	}
}
