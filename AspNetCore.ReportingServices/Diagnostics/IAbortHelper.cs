namespace AspNetCore.ReportingServices.Diagnostics
{
	public interface IAbortHelper
	{
		bool Abort(ProcessingStatus status);
	}
}
