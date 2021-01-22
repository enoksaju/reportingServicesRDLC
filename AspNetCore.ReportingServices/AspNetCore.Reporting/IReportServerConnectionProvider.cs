namespace AspNetCore.Reporting
{
	public interface IReportServerConnectionProvider
	{
		IReportServerConnection Create();
	}
}
