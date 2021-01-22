using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.RdlExpressions
{
	public struct FloatResult
	{
		public bool ErrorOccurred;

		public DataFieldStatus FieldStatus;

		public double Value;
	}
}
