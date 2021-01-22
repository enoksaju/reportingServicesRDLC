using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.RdlExpressions
{
	public struct BinaryResult
	{
		public bool ErrorOccurred;

		public DataFieldStatus FieldStatus;

		public byte[] Value;
	}
}
