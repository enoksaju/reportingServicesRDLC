using AspNetCore.ReportingServices.DataProcessing;

namespace AspNetCore.ReportingServices.DataExtensions
{
	public interface ITokenDataExtension2 : ITokenDataExtension
	{
		bool UseTokenAuthentication
		{
			get;
		}
	}
}
