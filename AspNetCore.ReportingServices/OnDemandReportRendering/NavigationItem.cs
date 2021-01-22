using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class NavigationItem
	{
		private readonly AspNetCore.ReportingServices.ReportIntermediateFormat.NavigationItem m_navigationItem;

		public string ReportItemReference
		{
			get
			{
				return this.m_navigationItem.ReportItemReference;
			}
		}

		public ReportItem ReportItem
		{
			get
			{
				return null;
			}
		}

		public NavigationItem(AspNetCore.ReportingServices.ReportIntermediateFormat.NavigationItem navigationItem)
		{
			this.m_navigationItem = navigationItem;
		}
	}
}
