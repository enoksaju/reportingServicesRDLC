using System.Drawing;

namespace AspNetCore.ReportingServices.Rendering.ExcelRenderer.Excel.BIFF8
{
	public sealed class BIFF8Color : IColor
	{
		private Color m_color;

		private int m_paletteIndex;

		public int PaletteIndex
		{
			get
			{
				return this.m_paletteIndex;
			}
		}

		public byte Red
		{
			get
			{
				return this.m_color.R;
			}
		}

		public byte Green
		{
			get
			{
				return this.m_color.G;
			}
		}

		public byte Blue
		{
			get
			{
				return this.m_color.B;
			}
		}

		public BIFF8Color()
		{
		}

		public BIFF8Color(Color color, int paletteIndex)
		{
			this.m_color = color;
			this.m_paletteIndex = paletteIndex;
		}

		public override int GetHashCode()
		{
			return this.m_color.GetHashCode();
		}

		public override bool Equals(object val)
		{
			if (val is BIFF8Color)
			{
				return this.m_color.Equals(((BIFF8Color)val).m_color);
			}
			return this.m_color.Equals(val);
		}
	}
}
