namespace AspNetCore.ReportingServices.ReportProcessing
{
	public interface IShowHideReceiver
	{
		void ProcessReceiver(ReportProcessing.ProcessingContext context, int uniqueName);
	}
}
