using System;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public interface IPageBreakItem
	{
		[Obsolete("Use PageBreak.BreakLocation instead.")]
		PageBreakLocation PageBreakLocation
		{
			get;
		}

		PageBreak PageBreak
		{
			get;
		}
	}
}
