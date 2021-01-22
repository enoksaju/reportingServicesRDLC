namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public enum SubReportErrorCodes
	{
		Success,
		ProcessingError,
		ParametersNotSpecified,
		ExceededMaxRecursionLevel,
		MissingSubReport,
		DataRetrievalFailed,
		DataNotRetrieved
	}
}
