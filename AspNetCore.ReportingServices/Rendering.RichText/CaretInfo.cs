using System.Drawing;

namespace AspNetCore.ReportingServices.Rendering.RichText
{
	public class CaretInfo
	{
		private Point m_position = Point.Empty;

		private bool m_isFirstLine;

		private bool m_isLastLine;

		private int m_ascent;

		private int m_descent;

		private int m_height;

		private int m_lineHeight;

		private int m_lineYOffset;

		public Point Position
		{
			get
			{
				return this.m_position;
			}
			set
			{
				this.m_position = value;
			}
		}

		public bool IsFirstLine
		{
			get
			{
				return this.m_isFirstLine;
			}
			set
			{
				this.m_isFirstLine = value;
			}
		}

		public bool IsLastLine
		{
			get
			{
				return this.m_isLastLine;
			}
			set
			{
				this.m_isLastLine = value;
			}
		}

		public int Ascent
		{
			get
			{
				return this.m_ascent;
			}
			set
			{
				this.m_ascent = value;
			}
		}

		public int Descent
		{
			get
			{
				return this.m_descent;
			}
			set
			{
				this.m_descent = value;
			}
		}

		public int LineHeight
		{
			get
			{
				return this.m_lineHeight;
			}
			set
			{
				this.m_lineHeight = value;
			}
		}

		public int LineYOffset
		{
			get
			{
				return this.m_lineYOffset;
			}
			set
			{
				this.m_lineYOffset = value;
			}
		}

		public int Height
		{
			get
			{
				return this.m_height;
			}
			set
			{
				this.m_height = value;
			}
		}

		public CaretInfo()
		{
		}
	}
}
