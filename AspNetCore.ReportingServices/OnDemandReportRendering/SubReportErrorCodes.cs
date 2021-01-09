namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	internal enum SubReportErrorCodes
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
