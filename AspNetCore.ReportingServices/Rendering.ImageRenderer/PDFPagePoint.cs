using System.Drawing;

namespace AspNetCore.ReportingServices.Rendering.ImageRenderer
{
	public sealed class PDFPagePoint
	{
		public int PageObjectId;

		public PointF Point;

		public PDFPagePoint(int pageObjectId, PointF point)
		{
			this.PageObjectId = pageObjectId;
			this.Point = point;
		}
	}
}
