namespace AspNetCore.ReportingServices.Rendering.ExcelRenderer.Excel.BIFF8
{
	public struct BIFF8Format
	{
		private int m_ifmt;

		private string m_string;

		public int Index
		{
			get
			{
				return this.m_ifmt;
			}
			set
			{
				this.m_ifmt = value;
			}
		}

		public string String
		{
			get
			{
				return this.m_string;
			}
			set
			{
				this.m_string = value;
			}
		}

		public BIFF8Format(string builtInFormat, int ifmt)
		{
			this.m_ifmt = ifmt;
			this.m_string = builtInFormat;
		}
	}
}
