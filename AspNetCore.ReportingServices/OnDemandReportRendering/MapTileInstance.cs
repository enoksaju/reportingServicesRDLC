namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class MapTileInstance : BaseInstance
	{
		private MapTile m_defObject;

		public MapTileInstance(MapTile defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		protected override void ResetInstanceCache()
		{
		}
	}
}
