namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class MapSpatialDataInstance : BaseInstance
	{
		private MapSpatialData m_defObject;

		public MapSpatialDataInstance(MapSpatialData defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		protected override void ResetInstanceCache()
		{
		}
	}
}
