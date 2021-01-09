namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	internal interface ICustomReportItem
	{
		void GenerateReportItemDefinition(CustomReportItem cri);

		void EvaluateReportItemInstance(CustomReportItem cri);
	}
}
