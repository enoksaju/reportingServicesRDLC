using AspNetCore.ReportingServices.Rendering.RPLProcessing;

namespace AspNetCore.ReportingServices.Rendering.HtmlRenderer
{
	public interface IReportItemRenderer
	{
		void RenderReportItem(RPLElement reportItem, RPLItemMeasurement measurement, StyleContext styleContext, ref int borderContext, bool renderId, bool treatAsTopLevel);
	}
}
