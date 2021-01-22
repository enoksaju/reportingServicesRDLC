namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class MapLineLayerInstance : MapVectorLayerInstance
	{
		private MapLineLayer m_defObject;

		public MapLineLayerInstance(MapLineLayer defObject)
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
