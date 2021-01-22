namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public interface IImageInstance
	{
		byte[] ImageData
		{
			get;
		}

		string StreamName
		{
			get;
		}

		string MIMEType
		{
			get;
		}
	}
}
