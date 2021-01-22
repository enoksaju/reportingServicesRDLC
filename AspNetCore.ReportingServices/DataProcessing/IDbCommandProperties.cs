namespace AspNetCore.ReportingServices.DataProcessing
{
	public interface IDbCommandProperties
	{
		bool SetRequestMemoryLimit(int limit);

		bool SetRequestIDAndCurrentActivityID();
	}
}
