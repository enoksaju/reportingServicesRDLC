namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class MapSpatialElementInstance : BaseInstance
	{
		private MapSpatialElement m_defObject;

		public MapSpatialElementInstance(MapSpatialElement defObject)
			: base(defObject.ReportScope)
		{
			this.m_defObject = defObject;
		}

		protected override void ResetInstanceCache()
		{
		}
	}
}
