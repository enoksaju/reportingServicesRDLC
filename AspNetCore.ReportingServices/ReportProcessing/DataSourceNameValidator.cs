namespace AspNetCore.ReportingServices.ReportProcessing
{
	public sealed class DataSourceNameValidator : NameValidator
	{
		public bool Validate(ObjectType objectType, string objectName, ErrorContext errorContext)
		{
			bool result = true;
			if (!base.IsUnique(objectName))
			{
				errorContext.Register(ProcessingErrorCode.rsDuplicateDataSourceName, Severity.Error, objectType, objectName, "Name");
				result = false;
			}
			return result;
		}
	}
}
