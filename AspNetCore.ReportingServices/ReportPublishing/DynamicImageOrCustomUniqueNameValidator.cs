using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.ReportPublishing
{
	public abstract class DynamicImageOrCustomUniqueNameValidator : UniqueNameValidator
	{
		public abstract bool Validate(Severity severity, string propertyName, ObjectType objectType, string objectName, string propertyNameValue, ErrorContext errorContext);
	}
}
