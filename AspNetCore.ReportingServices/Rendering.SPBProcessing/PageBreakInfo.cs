using AspNetCore.ReportingServices.Diagnostics.Utilities;
using AspNetCore.ReportingServices.OnDemandReportRendering;

namespace AspNetCore.ReportingServices.Rendering.SPBProcessing
{
	public class PageBreakInfo
	{
		private PageBreakLocation m_breakLocation;

		private bool m_disabled;

		private bool m_resetPageNumber;

		private string m_reportItemName;

		public PageBreakLocation BreakLocation
		{
			get
			{
				return this.m_breakLocation;
			}
		}

		public bool Disabled
		{
			get
			{
				return this.m_disabled;
			}
		}

		public bool ResetPageNumber
		{
			get
			{
				return this.m_resetPageNumber;
			}
		}

		public string ReportItemName
		{
			get
			{
				return this.m_reportItemName;
			}
		}

		public PageBreakInfo(PageBreak pageBreak, string reportItemName)
		{
			if (pageBreak != null)
			{
				this.m_breakLocation = pageBreak.BreakLocation;
				this.m_disabled = pageBreak.Instance.Disabled;
				this.m_resetPageNumber = pageBreak.Instance.ResetPageNumber;
				if (RenderingDiagnostics.Enabled)
				{
					this.m_reportItemName = reportItemName;
				}
			}
		}
	}
}
