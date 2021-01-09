namespace AspNetCore.ReportingServices.Rendering.ExcelRenderer.Excel.BIFF8
{
	internal sealed class BookmarkInfo : HyperlinkInfo
	{
		internal override bool IsBookmark
		{
			get
			{
				return true;
			}
		}

		internal BookmarkInfo(string url, string label, int firstRow, int lastRow, int firstCol, int lastCol)
			: base(url, label, firstRow, lastRow, firstCol, lastCol)
		{
		}
	}
}
