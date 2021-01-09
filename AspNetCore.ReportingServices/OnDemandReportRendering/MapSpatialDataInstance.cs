namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	internal abstract class MapSpatialDataInstance : BaseInstance
	{
		private MapSpatialData m_defObject;

		internal MapSpatialDataInstance(MapSpatialData defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		protected override void ResetInstanceCache()
		{
		}
	}
}
