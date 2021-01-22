namespace AspNetCore.ReportingServices.Rendering.ExcelRenderer.Excel.BIFF8
{
	public sealed class BookmarkInfo : HyperlinkInfo
	{
		public override bool IsBookmark
		{
			get
			{
				return true;
			}
		}

		public BookmarkInfo(string url, string label, int firstRow, int lastRow, int firstCol, int lastCol)
			: base(url, label, firstRow, lastRow, firstCol, lastCol)
		{
		}
	}
}
