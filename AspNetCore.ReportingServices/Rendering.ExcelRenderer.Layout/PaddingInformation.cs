namespace AspNetCore.ReportingServices.Rendering.ExcelRenderer.Layout
{
	public class PaddingInformation
	{
		private int m_paddingLeft;

		private int m_paddingRight;

		private int m_paddingTop;

		private int m_paddingBottom;

		public int PaddingLeft
		{
			get
			{
				return this.m_paddingLeft;
			}
		}

		public int PaddingRight
		{
			get
			{
				return this.m_paddingRight;
			}
		}

		public int PaddingTop
		{
			get
			{
				return this.m_paddingTop;
			}
		}

		public int PaddingBottom
		{
			get
			{
				return this.m_paddingBottom;
			}
		}

		public PaddingInformation(int paddingLeft, int paddingRight, int paddingTop, int paddingBottom)
		{
			this.m_paddingLeft = paddingLeft;
			this.m_paddingRight = paddingRight;
			this.m_paddingTop = paddingTop;
			this.m_paddingBottom = paddingBottom;
		}
	}
}
