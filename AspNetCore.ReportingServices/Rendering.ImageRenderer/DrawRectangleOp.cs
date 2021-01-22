using AspNetCore.ReportingServices.Rendering.RPLProcessing;
using System.Drawing;

namespace AspNetCore.ReportingServices.Rendering.ImageRenderer
{
	public sealed class DrawRectangleOp : Operation
	{
		public float Width;

		public RPLFormat.BorderStyles Style;

		public Color Color;

		public RectangleF Rectangle;

		public DrawRectangleOp(Color color, float width, RPLFormat.BorderStyles style, RectangleF rectangle)
		{
			this.Color = color;
			this.Width = width;
			this.Style = style;
			this.Rectangle = rectangle;
		}

		public override void Perform(WriterBase writer)
		{
			writer.DrawRectangle(this.Color, this.Width, this.Style, this.Rectangle);
		}
	}
}
