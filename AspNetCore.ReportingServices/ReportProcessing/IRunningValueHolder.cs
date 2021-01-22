namespace AspNetCore.ReportingServices.ReportProcessing
{
	public interface IRunningValueHolder
	{
		RunningValueInfoList GetRunningValueList();

		void ClearIfEmpty();
	}
}
