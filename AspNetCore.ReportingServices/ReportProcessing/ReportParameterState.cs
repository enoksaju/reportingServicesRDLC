namespace AspNetCore.ReportingServices.ReportProcessing
{
	public enum ReportParameterState
	{
		HasValidValue,
		InvalidValueProvided,
		DefaultValueInvalid,
		MissingValidValue,
		HasOutstandingDependencies,
		DynamicValuesUnavailable
	}
}
