namespace AspNetCore.ReportingServices.Rendering.WordRenderer
{
	public class BorderContext
	{
		private const int TopBit = 1;

		private const int LeftBit = 2;

		private const int BottomBit = 4;

		private const int RightBit = 8;

		public static readonly BorderContext EmptyBorder = new BorderContext(0);

		public static readonly BorderContext TopBorder = new BorderContext(1);

		public static readonly BorderContext LeftBorder = new BorderContext(2);

		public static readonly BorderContext RightBorder = new BorderContext(8);

		public static readonly BorderContext BottomBorder = new BorderContext(4);

		public static readonly BorderContext TopLeftBorder = new BorderContext(3);

		public static readonly BorderContext TopRightBorder = new BorderContext(9);

		public static readonly BorderContext BottomLeftBorder = new BorderContext(6);

		public static readonly BorderContext BottomRightBorder = new BorderContext(12);

		private int m_borderContext;

		public bool Top
		{
			get
			{
				return (this.m_borderContext & 1) > 0;
			}
			set
			{
				if (value)
				{
					this.m_borderContext |= 1;
				}
				else
				{
					this.m_borderContext &= -2;
				}
			}
		}

		public bool Left
		{
			get
			{
				return (this.m_borderContext & 2) > 0;
			}
			set
			{
				if (value)
				{
					this.m_borderContext |= 2;
				}
				else
				{
					this.m_borderContext &= -3;
				}
			}
		}

		public bool Bottom
		{
			get
			{
				return (this.m_borderContext & 4) > 0;
			}
			set
			{
				if (value)
				{
					this.m_borderContext |= 4;
				}
				else
				{
					this.m_borderContext &= -5;
				}
			}
		}

		public bool Right
		{
			get
			{
				return (this.m_borderContext & 8) > 0;
			}
			set
			{
				if (value)
				{
					this.m_borderContext |= 8;
				}
				else
				{
					this.m_borderContext &= -9;
				}
			}
		}

		public BorderContext()
		{
			this.m_borderContext = 0;
		}

		public BorderContext(BorderContext borderContext)
		{
			this.m_borderContext = borderContext.m_borderContext;
		}

		public BorderContext(int borderContext)
		{
			this.m_borderContext = borderContext;
		}

		public void Reset()
		{
			this.m_borderContext = 0;
		}
	}
}
