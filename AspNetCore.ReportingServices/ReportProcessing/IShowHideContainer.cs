namespace AspNetCore.ReportingServices.ReportProcessing
{
	public interface IShowHideContainer
	{
		void BeginProcessContainer(ReportProcessing.ProcessingContext context);

		void EndProcessContainer(ReportProcessing.ProcessingContext context);
	}
}
