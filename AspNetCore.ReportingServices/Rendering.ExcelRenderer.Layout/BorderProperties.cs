using AspNetCore.ReportingServices.Rendering.ExcelRenderer.Excel;

namespace AspNetCore.ReportingServices.Rendering.ExcelRenderer.Layout
{
	public sealed class BorderProperties
	{
		private IColor m_color;

		private ExcelBorderStyle m_style;

		private ExcelBorderPart m_part;

		private double m_width = -1.0;

		public ExcelBorderPart ExcelBorderPart
		{
			set
			{
				this.m_part = value;
			}
		}

		public IColor Color
		{
			get
			{
				return this.m_color;
			}
			set
			{
				this.m_color = value;
			}
		}

		public ExcelBorderStyle Style
		{
			get
			{
				return this.m_style;
			}
			set
			{
				if (this.m_width != -1.0)
				{
					this.m_style = LayoutConvert.ToBorderLineStyle(value, this.m_width);
				}
				else
				{
					this.m_style = value;
				}
			}
		}

		public double Width
		{
			get
			{
				return this.m_width;
			}
			set
			{
				if (this.m_style != 0)
				{
					this.m_style = LayoutConvert.ToBorderLineStyle(this.m_style, value);
				}
				this.m_width = value;
			}
		}

		public BorderProperties(IColor color, ExcelBorderStyle style, ExcelBorderPart part)
		{
			this.m_color = color;
			this.m_style = style;
			this.m_part = part;
		}

		public BorderProperties(ExcelBorderPart part)
		{
			this.m_part = part;
		}

		public BorderProperties(BorderProperties borderProps, ExcelBorderPart part)
		{
			if (borderProps != null)
			{
				this.m_color = borderProps.Color;
				this.m_style = borderProps.Style;
				this.m_width = borderProps.Width;
			}
			this.m_part = part;
		}

		public void Render(IStyle style)
		{
			if (this.m_style != 0)
			{
				switch (this.m_part)
				{
				case ExcelBorderPart.Left:
					style.BorderLeftStyle = this.m_style;
					style.BorderLeftColor = this.m_color;
					break;
				case ExcelBorderPart.Right:
					style.BorderRightStyle = this.m_style;
					style.BorderRightColor = this.m_color;
					break;
				case ExcelBorderPart.Top:
					style.BorderTopStyle = this.m_style;
					style.BorderTopColor = this.m_color;
					break;
				case ExcelBorderPart.Bottom:
					style.BorderBottomStyle = this.m_style;
					style.BorderBottomColor = this.m_color;
					break;
				case ExcelBorderPart.Outline:
					style.BorderLeftStyle = this.m_style;
					style.BorderLeftColor = this.m_color;
					style.BorderRightStyle = this.m_style;
					style.BorderRightColor = this.m_color;
					style.BorderTopStyle = this.m_style;
					style.BorderTopColor = this.m_color;
					style.BorderBottomStyle = this.m_style;
					style.BorderBottomColor = this.m_color;
					break;
				case ExcelBorderPart.DiagonalDown:
				case ExcelBorderPart.DiagonalUp:
				case ExcelBorderPart.DiagonalBoth:
					style.BorderDiagStyle = this.m_style;
					style.BorderDiagColor = this.m_color;
					style.BorderDiagPart = this.m_part;
					break;
				}
			}
		}

		public override string ToString()
		{
			return this.m_style.ToString();
		}
	}
}
