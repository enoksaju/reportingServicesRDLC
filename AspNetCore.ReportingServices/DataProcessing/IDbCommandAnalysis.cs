namespace AspNetCore.ReportingServices.DataProcessing
{
	public interface IDbCommandAnalysis
	{
		IDataParameterCollection GetParameters();
	}
}
