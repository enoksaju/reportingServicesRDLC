namespace AspNetCore.ReportingServices.ReportProcessing
{
	internal interface IShowHideSender
	{
		void ProcessSender(ReportProcessing.ProcessingContext context, int uniqueName);
	}
}
