using AspNetCore.ReportingServices.ReportRendering;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class ParagraphFilteredStyle : Style
	{
		public ParagraphFilteredStyle(AspNetCore.ReportingServices.ReportRendering.ReportItem renderReportItem, RenderingContext renderingContext, bool useRenderStyle)
			: base(renderReportItem, renderingContext, useRenderStyle)
		{
		}

		protected override bool IsAvailableStyle(StyleAttributeNames styleName)
		{
			if (styleName != StyleAttributeNames.TextAlign && styleName != StyleAttributeNames.LineHeight)
			{
				return false;
			}
			return true;
		}
	}
}
