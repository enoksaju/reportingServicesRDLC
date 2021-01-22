namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class MapLineRulesInstance : BaseInstance
	{
		private MapLineRules m_defObject;

		public MapLineRulesInstance(MapLineRules defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		protected override void ResetInstanceCache()
		{
		}
	}
}
