using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	public interface IShowHideSender
	{
		void ProcessSender(AspNetCore.ReportingServices.ReportProcessing.ReportProcessing.ProcessingContext context, int uniqueName);
	}
}
