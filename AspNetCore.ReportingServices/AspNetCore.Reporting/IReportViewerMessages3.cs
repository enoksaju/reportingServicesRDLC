namespace AspNetCore.Reporting
{
	public interface IReportViewerMessages3 : IReportViewerMessages2, IReportViewerMessages
	{
		string CancelLinkText
		{
			get;
		}

		string CalendarLoading
		{
			get;
		}

		string TotalPages(int pageCount, PageCountMode pageCountMode);
	}
}
