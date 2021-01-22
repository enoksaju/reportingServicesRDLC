namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public interface IActionInstance
	{
		string HyperlinkText
		{
			get;
		}

		void SetHyperlinkText(string value);
	}
}
