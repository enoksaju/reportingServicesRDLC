namespace AspNetCore.ReportingServices.ReportProcessing
{
	public enum DataFieldStatus
	{
		None,
		Overflow,
		UnSupportedDataType,
		IsMissing = 4,
		IsError = 8
	}
}
