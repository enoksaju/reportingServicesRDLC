namespace AspNetCore.ReportingServices.ReportProcessing
{
	public struct DataAggregateObjResult
	{
		public bool ErrorOccurred;

		public object Value;

		public bool HasCode;

		public ProcessingErrorCode Code;

		public Severity Severity;

		public string[] Arguments;

		public DataFieldStatus FieldStatus;
	}
}
