namespace AspNetCore.ReportingServices.ReportProcessing
{
	internal interface IShowHideReceiver
	{
		void ProcessReceiver(ReportProcessing.ProcessingContext context, int uniqueName);
	}
}
