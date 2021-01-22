namespace AspNetCore.ReportingServices.Rendering.HtmlRenderer
{
	public class PaddingSharedInfo
	{
		private double m_padH;

		private double m_padV;

		private int m_paddingContext;

		public double PadH
		{
			get
			{
				return this.m_padH;
			}
		}

		public double PadV
		{
			get
			{
				return this.m_padV;
			}
		}

		public int PaddingContext
		{
			get
			{
				return this.m_paddingContext;
			}
		}

		public PaddingSharedInfo(int paddingContext, double padH, double padV)
		{
			this.m_padH = padH;
			this.m_padV = padV;
			this.m_paddingContext = paddingContext;
		}
	}
}
