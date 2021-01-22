namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class MapPolygonRulesInstance : BaseInstance
	{
		private MapPolygonRules m_defObject;

		public MapPolygonRulesInstance(MapPolygonRules defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		protected override void ResetInstanceCache()
		{
		}
	}
}
