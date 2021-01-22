namespace AspNetCore.ReportingServices.ReportProcessing
{
	public enum ReportParameterDependencyState
	{
		AllDependenciesSpecified,
		HasOutstandingDependencies,
		MissingUpstreamDataSourcePrompt
	}
}
