using AspNetCore.ReportingServices.ReportProcessing;
using System;

namespace AspNetCore.ReportingServices.RdlExpressions
{
	public struct VariantResult
	{
		public bool ErrorOccurred;

		public DataFieldStatus FieldStatus;

		public string ExceptionMessage;

		public object Value;

		public TypeCode TypeCode;

		public VariantResult(bool errorOccurred, object v)
		{
			this.ErrorOccurred = errorOccurred;
			this.Value = v;
			this.FieldStatus = DataFieldStatus.None;
			this.ExceptionMessage = null;
			this.TypeCode = TypeCode.Empty;
		}
	}
}
