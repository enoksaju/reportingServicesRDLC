using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.ReportPublishing
{
	public abstract class UniqueNameValidator : NameValidator
	{
		public UniqueNameValidator()
			: base(false)
		{
		}

		public abstract bool Validate(Severity severity, ObjectType objectType, string objectName, string propertyNameValue, ErrorContext errorContext);
	}
}
