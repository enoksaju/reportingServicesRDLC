using System.Data;

namespace AspNetCore.ReportingServices.DataProcessing
{
	public interface IDbConnectionWrapper
	{
		System.Data.IDbConnection Connection
		{
			get;
		}
	}
}
