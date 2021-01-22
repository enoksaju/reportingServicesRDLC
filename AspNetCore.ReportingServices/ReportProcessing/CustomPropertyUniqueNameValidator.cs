namespace AspNetCore.ReportingServices.ReportProcessing
{
	public sealed class CustomPropertyUniqueNameValidator : NameValidator
	{
		public CustomPropertyUniqueNameValidator()
		{
		}

		public bool Validate(Severity severity, ObjectType objectType, string objectName, string propertyNameValue, ErrorContext errorContext)
		{
			bool result = true;
			if (propertyNameValue == null || !base.IsUnique(propertyNameValue))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidCustomPropertyName, severity, objectType, objectName, propertyNameValue);
				result = false;
			}
			return result;
		}
	}
}
