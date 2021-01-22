namespace AspNetCore.ReportingServices.ReportProcessing
{
	public struct BinaryResult
	{
		public bool ErrorOccurred;

		public DataFieldStatus FieldStatus;

		public byte[] Value;
	}
}
