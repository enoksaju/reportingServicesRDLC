using AspNetCore.ReportingServices.Rendering.RPLProcessing;
using System.Drawing;

namespace AspNetCore.ReportingServices.Rendering.ImageRenderer
{
	public sealed class HardPageBreakShared
	{
		private HardPageBreakShared()
		{
		}

		public static RectangleF CalculateColumnBounds(RPLReportSection reportSection, RPLPageLayout pageLayout, int columnNumber, float top, float columnHeight)
		{
			float num = pageLayout.PageWidth - pageLayout.MarginLeft - pageLayout.MarginRight;
			int num2 = reportSection.ColumnCount;
			if (num2 == 0)
			{
				num2 = 1;
			}
			float num3 = (float)(reportSection.ColumnCount - 1) * reportSection.ColumnSpacing;
			float num4 = (num - num3) / (float)num2;
			return new RectangleF(pageLayout.MarginLeft + (float)columnNumber * (reportSection.ColumnSpacing + num4), top, num4, columnHeight);
		}

		public static RectangleF CalculateHeaderBounds(RPLReportSection reportSection, RPLPageLayout pageLayout, float top, float width)
		{
			if (reportSection.Header == null)
			{
				return RectangleF.Empty;
			}
			return new RectangleF(pageLayout.MarginLeft, top, width, reportSection.Header.Height);
		}

		public static RectangleF CalculateFooterBounds(RPLReportSection reportSection, RPLPageLayout pageLayout, float top, float width)
		{
			if (reportSection.Footer == null)
			{
				return RectangleF.Empty;
			}
			return new RectangleF(pageLayout.MarginLeft, top, width, reportSection.Footer.Height);
		}
	}
}
