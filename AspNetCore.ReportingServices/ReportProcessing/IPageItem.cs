namespace AspNetCore.ReportingServices.ReportProcessing
{
	internal interface IPageItem
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
