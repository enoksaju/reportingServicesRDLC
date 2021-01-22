namespace AspNetCore.ReportingServices.Rendering.HtmlRenderer
{
	public interface IReportWrapper
	{
		bool HasBookmarks
		{
			get;
		}

		string SortItem
		{
			get;
		}

		string ShowHideToggle
		{
			get;
		}

		string GetStreamUrl(bool useSessionId, string streamName);

		string GetReportUrl(bool addParams);

		byte[] GetImageName(string imageID);
	}
}
