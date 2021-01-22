namespace AspNetCore.ReportingServices.ReportRendering
{
	public interface IImage
	{
		byte[] ImageData
		{
			get;
		}

		string MIMEType
		{
			get;
		}

		string StreamName
		{
			get;
		}
	}
}
