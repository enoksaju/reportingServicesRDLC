namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public interface ICustomReportItem
	{
		void GenerateReportItemDefinition(CustomReportItem cri);

		void EvaluateReportItemInstance(CustomReportItem cri);
	}
}
