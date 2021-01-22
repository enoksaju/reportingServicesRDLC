using System.Drawing;

namespace AspNetCore.ReportingServices.Rendering.ImageRenderer
{
	public sealed class FillPolygonOp : Operation
	{
		public Color Color;

		public PointF[] Polygon;

		public FillPolygonOp(Color color, PointF[] polygon)
		{
			this.Color = color;
			this.Polygon = polygon;
		}

		public override void Perform(WriterBase writer)
		{
			writer.FillPolygon(this.Color, this.Polygon);
		}
	}
}
