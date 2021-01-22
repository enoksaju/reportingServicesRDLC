namespace AspNetCore.ReportingServices.DataExtensions
{
	public interface IServiceEndpoint
	{
		string Host
		{
			get;
		}

		int Port
		{
			get;
		}
	}
}
