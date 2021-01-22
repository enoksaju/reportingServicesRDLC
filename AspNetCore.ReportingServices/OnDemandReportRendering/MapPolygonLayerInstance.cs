namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class MapPolygonLayerInstance : MapVectorLayerInstance
	{
		private MapPolygonLayer m_defObject;

		public MapPolygonLayerInstance(MapPolygonLayer defObject)
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
