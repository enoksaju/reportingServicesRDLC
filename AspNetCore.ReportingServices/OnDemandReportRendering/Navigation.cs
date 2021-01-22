using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class Navigation
	{
		public readonly AspNetCore.ReportingServices.ReportIntermediateFormat.Navigation m_navigation;

		public Navigation(AspNetCore.ReportingServices.ReportIntermediateFormat.BandLayoutOptions bandLayout)
		{
			this.m_navigation = bandLayout.Navigation;
		}
	}
}
