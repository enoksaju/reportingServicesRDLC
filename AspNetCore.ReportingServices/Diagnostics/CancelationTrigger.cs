namespace AspNetCore.ReportingServices.Diagnostics
{
	public enum CancelationTrigger
	{
		None,
		AfterDsqParsing,
		AfterDataSourceResolution,
		DsqtAfterValidation,
		DsqtAfterQueryGeneration,
		DsqtAfterDsdGeneration,
		ReportProcessing
	}
}
