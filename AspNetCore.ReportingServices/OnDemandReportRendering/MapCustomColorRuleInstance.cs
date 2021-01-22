namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class MapCustomColorRuleInstance : MapColorRuleInstance
	{
		private MapCustomColorRule m_defObject;

		public MapCustomColorRuleInstance(MapCustomColorRule defObject)
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
