namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class GaugeImageInstance : GaugePanelItemInstance
	{
		public GaugeImageInstance(GaugeImage defObject)
			: base(defObject)
		{
		}

		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
		}
	}
}
