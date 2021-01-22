namespace AspNetCore.ReportingServices.Rendering.ExcelRenderer.Excel.BIFF8
{
	public class HyperlinkInfo : AreaInfo
	{
		private string m_url;

		private string m_label;

		public string URL
		{
			get
			{
				return this.m_url;
			}
			set
			{
				this.m_url = value;
			}
		}

		public string Label
		{
			get
			{
				return this.m_label;
			}
		}

		public virtual bool IsBookmark
		{
			get
			{
				return false;
			}
		}

		public HyperlinkInfo(string url, string label, int firstRow, int lastRow, int firstCol, int lastCol)
			: base(firstRow, lastRow, firstCol, lastCol)
		{
			this.m_url = url;
			if (label == null)
			{
				label = string.Empty;
			}
			this.m_label = label;
		}

		public override string ToString()
		{
			return "URL: " + this.URL + ", Bookmark: " + this.IsBookmark;
		}
	}
}
