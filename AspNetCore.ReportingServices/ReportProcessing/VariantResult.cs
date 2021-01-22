namespace AspNetCore.ReportingServices.ReportProcessing
{
	public struct VariantResult
	{
		public bool ErrorOccurred;

		public DataFieldStatus FieldStatus;

		public string ExceptionMessage;

		public object Value;

		public VariantResult(bool errorOccurred, object v)
		{
			this.ErrorOccurred = errorOccurred;
			this.Value = v;
			this.FieldStatus = DataFieldStatus.None;
			this.ExceptionMessage = null;
		}
	}
}
