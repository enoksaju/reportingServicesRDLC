namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	internal interface IActionInstance
	{
		string HyperlinkText
		{
			get;
		}

		void SetHyperlinkText(string value);
	}
}
