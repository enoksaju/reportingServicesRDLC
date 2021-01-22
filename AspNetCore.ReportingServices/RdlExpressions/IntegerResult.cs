using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.RdlExpressions
{
	public struct IntegerResult
	{
		public bool ErrorOccurred;

		public DataFieldStatus FieldStatus;

		public int Value;
	}
}
