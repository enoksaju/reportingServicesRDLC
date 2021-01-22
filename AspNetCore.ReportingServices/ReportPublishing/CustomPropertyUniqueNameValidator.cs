using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.ReportPublishing
{
	public sealed class CustomPropertyUniqueNameValidator : DynamicImageOrCustomUniqueNameValidator
	{
		public CustomPropertyUniqueNameValidator()
		{
		}

		public override bool Validate(Severity severity, ObjectType objectType, string objectName, string propertyNameValue, ErrorContext errorContext)
		{
			Global.Tracer.Assert(false);
			return this.Validate(severity, "", objectType, objectName, propertyNameValue, errorContext);
		}

		public override bool Validate(Severity severity, string propertyName, ObjectType objectType, string objectName, string propertyNameValue, ErrorContext errorContext)
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
