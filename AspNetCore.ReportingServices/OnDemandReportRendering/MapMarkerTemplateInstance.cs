namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class MapMarkerTemplateInstance : MapPointTemplateInstance
	{
		private MapMarkerTemplate m_defObject;

		public MapMarkerTemplateInstance(MapMarkerTemplate defObject)
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
