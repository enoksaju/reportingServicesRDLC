using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.RdlExpressions
{
	public struct ParameterValueResult
	{
		public bool ErrorOccurred;

		public object Value;

		public DataType Type;
	}
}
