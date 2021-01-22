using System.Drawing;
using System.Drawing.Drawing2D;

namespace AspNetCore.Reporting.Chart.WebForms.Svg
{
	public abstract class SvgParameters
	{
		public enum SvgLineCapStyle
		{
			Butt,
			Round,
			Square
		}

		public enum SvgDashStyle
		{
			Dash,
			DashDot,
			DashDotDot,
			DashDashDot,
			DoubleDash,
			DoubleDashHalfDash,
			DoubleDashHalfDashHalfDash,
			DoubleDashDoubleDashHalfDash,
			HalfDash,
			HalfDashDot,
			HalfDashDotDot,
			HalfDashHalfDashDot,
			Dot,
			Custom,
			Solid
		}

		public enum SvgFillType
		{
			Solid,
			Hatch,
			Image,
			Gradient,
			None
		}

		public enum SvgGradientType
		{
			None,
			LeftRight,
			TopBottom,
			Center,
			DiagonalLeft,
			DiagonalRight,
			HorizontalCenter,
			VerticalCenter
		}

		public enum SvgImageWrapMode
		{
			ClampScaled = 4,
			Tile = 0,
			TileFlipX,
			TileFlipY,
			TileFlipXY,
			ClampUnscaled
		}

		protected abstract Color BrushColor
		{
			get;
		}

		protected abstract Color TextColor
		{
			get;
		}

		protected abstract Color BrushSecondColor
		{
			get;
		}

		protected abstract SvgGradientType GradientType
		{
			get;
		}

		protected abstract SvgFillType FillType
		{
			get;
		}

		protected abstract FillMode FillMode
		{
			get;
			set;
		}

		protected abstract SvgImageWrapMode ImageWrapMode
		{
			get;
		}

		protected abstract Color PenColor
		{
			get;
		}

		protected abstract double PenWidth
		{
			get;
		}

		protected abstract SvgLineCapStyle SvgLineCap
		{
			get;
		}

		protected abstract SvgDashStyle DashStyle
		{
			get;
		}

		protected abstract Matrix Transform
		{
			get;
		}

		protected abstract Font Font
		{
			get;
		}

		protected abstract StringFormat StringFormat
		{
			get;
		}

		public abstract Size PictureSize
		{
			get;
			set;
		}

		protected abstract string GetX(double x);

		protected abstract string GetY(double y);

		protected abstract string GetX(PointF point);

		protected abstract string GetX(RectangleF rectangle);

		protected abstract string GetWidth(RectangleF rectangle);

		protected abstract string GetHeight(RectangleF rectangle);

		protected abstract string GetY(PointF point);

		protected abstract string GetY(RectangleF rectangle);
	}
}
