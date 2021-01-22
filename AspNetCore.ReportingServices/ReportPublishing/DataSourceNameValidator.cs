using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.ReportPublishing
{
	public sealed class DataSourceNameValidator : NameValidator
	{
		public DataSourceNameValidator()
			: base(false)
		{
		}

		public bool Validate(ObjectType objectType, string objectName, ErrorContext errorContext)
		{
			bool result = true;
			if (string.IsNullOrEmpty(objectName) || objectName.Length > 256)
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidDataSourceNameLength, Severity.Error, objectType, objectName, "Name", "256");
				result = false;
			}
			if (!base.IsUnique(objectName))
			{
				errorContext.Register(ProcessingErrorCode.rsDuplicateDataSourceName, Severity.Error, objectType, objectName, "Name");
				result = false;
			}
			if (!NameValidator.IsCLSCompliant(objectName))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidDataSourceNameNotCLSCompliant, Severity.Error, objectType, objectName, "Name");
				result = false;
			}
			return result;
		}
	}
}
