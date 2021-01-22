namespace AspNetCore.ReportingServices.ReportProcessing
{
	public interface IPageItem
	{
		int StartPage
		{
			get;
			set;
		}

		int EndPage
		{
			get;
			set;
		}
	}
}
