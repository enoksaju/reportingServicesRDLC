using AspNetCore.ReportingServices.Rendering.RPLProcessing;
using System.Drawing;

namespace AspNetCore.ReportingServices.Rendering.ImageRenderer
{
	public sealed class DrawLineOp : Operation
	{
		public float Width;

		public RPLFormat.BorderStyles Style;

		public Color Color;

		public float X1;

		public float Y1;

		public float X2;

		public float Y2;

		public DrawLineOp(Color color, float width, RPLFormat.BorderStyles style, float x1, float y1, float x2, float y2)
		{
			this.Color = color;
			this.Width = width;
			this.Style = style;
			this.X1 = x1;
			this.Y1 = y1;
			this.X2 = x2;
			this.Y2 = y2;
		}

		public override void Perform(WriterBase writer)
		{
			writer.DrawLine(this.Color, this.Width, this.Style, this.X1, this.Y1, this.X2, this.Y2);
		}
	}
}
