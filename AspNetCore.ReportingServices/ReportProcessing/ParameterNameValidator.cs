namespace AspNetCore.ReportingServices.ReportProcessing
{
	public sealed class ParameterNameValidator : NameValidator
	{
		public bool Validate(string parameterName, ObjectType objectType, string objectName, ErrorContext errorContext)
		{
			bool result = true;
			if (!NameValidator.IsCLSCompliant(parameterName))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidParameterNameNotCLSCompliant, Severity.Error, objectType, objectName, "Name", parameterName);
				result = false;
			}
			return result;
		}
	}
}
