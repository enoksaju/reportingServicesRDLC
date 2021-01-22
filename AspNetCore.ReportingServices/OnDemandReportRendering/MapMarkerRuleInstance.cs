namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class MapMarkerRuleInstance : MapAppearanceRuleInstance
	{
		private MapMarkerRule m_defObject;

		public MapMarkerRuleInstance(MapMarkerRule defObject)
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
