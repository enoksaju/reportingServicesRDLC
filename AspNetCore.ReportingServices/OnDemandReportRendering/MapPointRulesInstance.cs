namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class MapPointRulesInstance : BaseInstance
	{
		private MapPointRules m_defObject;

		public MapPointRulesInstance(MapPointRules defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		protected override void ResetInstanceCache()
		{
		}
	}
}
