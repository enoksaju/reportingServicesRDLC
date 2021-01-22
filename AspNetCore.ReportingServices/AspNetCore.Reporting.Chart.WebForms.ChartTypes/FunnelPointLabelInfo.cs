using System.Drawing;

namespace AspNetCore.Reporting.Chart.WebForms.ChartTypes
{
	public class FunnelPointLabelInfo
	{
		public DataPoint Point;

		public int PointIndex;

		public string Text = string.Empty;

		public SizeF Size = SizeF.Empty;

		public RectangleF Position = RectangleF.Empty;

		public FunnelLabelStyle Style = FunnelLabelStyle.OutsideInColumn;

		public StringFormat Format = new StringFormat();

		public FunnelLabelVerticalAlignment VerticalAlignment;

		public FunnelLabelPlacement OutsidePlacement;

		public PointF CalloutPoint1 = PointF.Empty;

		public PointF CalloutPoint2 = PointF.Empty;
	}
}
