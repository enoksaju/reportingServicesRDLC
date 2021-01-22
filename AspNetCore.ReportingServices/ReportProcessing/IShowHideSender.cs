namespace AspNetCore.ReportingServices.ReportProcessing
{
	public interface IShowHideSender
	{
		void ProcessSender(ReportProcessing.ProcessingContext context, int uniqueName);
	}
}
