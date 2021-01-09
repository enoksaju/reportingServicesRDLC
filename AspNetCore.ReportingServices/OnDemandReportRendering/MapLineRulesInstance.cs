namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	internal sealed class MapLineRulesInstance : BaseInstance
	{
		private MapLineRules m_defObject;

		internal MapLineRulesInstance(MapLineRules defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		protected override void ResetInstanceCache()
		{
		}
	}
}
