using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.ReportPublishing
{
	internal abstract class UniqueNameValidator : NameValidator
	{
		internal UniqueNameValidator()
			: base(false)
		{
		}

		internal abstract bool Validate(Severity severity, ObjectType objectType, string objectName, string propertyNameValue, ErrorContext errorContext);
	}
}
