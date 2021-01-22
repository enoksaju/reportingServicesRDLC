using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.RdlExpressions
{
	public struct StringResult
	{
		public bool ErrorOccurred;

		public DataFieldStatus FieldStatus;

		public string Value;
	}
}
