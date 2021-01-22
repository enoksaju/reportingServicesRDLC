namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public interface IImage
	{
		Image.SourceType Source
		{
			get;
		}

		ReportStringProperty Value
		{
			get;
		}

		ReportStringProperty MIMEType
		{
			get;
		}
	}
}
