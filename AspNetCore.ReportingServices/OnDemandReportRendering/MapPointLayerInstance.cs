namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class MapPointLayerInstance : MapVectorLayerInstance
	{
		private MapPointLayer m_defObject;

		public MapPointLayerInstance(MapPointLayer defObject)
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
