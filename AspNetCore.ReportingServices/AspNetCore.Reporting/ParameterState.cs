namespace AspNetCore.Reporting
{
	public enum ParameterState
	{
		HasValidValue,
		MissingValidValue,
		HasOutstandingDependencies,
		DynamicValuesUnavailable
	}
}
