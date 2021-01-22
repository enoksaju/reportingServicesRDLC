using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.ReportPublishing
{
	public sealed class ScopeNameValidator : NameValidator
	{
		public ScopeNameValidator()
			: base(false)
		{
		}

		public bool Validate(bool isGrouping, string scopeName, ObjectType objectType, string objectName, ErrorContext errorContext)
		{
			return this.Validate(isGrouping, scopeName, objectType, objectName, errorContext, true);
		}

		public bool Validate(bool isGrouping, string scopeName, ObjectType objectType, string objectName, ErrorContext errorContext, bool enforceCLSCompliance)
		{
			bool result = true;
			if (!NameValidator.IsCLSCompliant(scopeName) && enforceCLSCompliance)
			{
				if (isGrouping)
				{
					errorContext.Register(ProcessingErrorCode.rsInvalidGroupingNameNotCLSCompliant, Severity.Error, objectType, objectName, "Name", scopeName);
				}
				else
				{
					errorContext.Register(ProcessingErrorCode.rsInvalidNameNotCLSCompliant, Severity.Error, objectType, objectName, "Name");
				}
				result = false;
			}
			if (!base.IsUnique(scopeName))
			{
				errorContext.Register(ProcessingErrorCode.rsDuplicateScopeName, Severity.Error, objectType, objectName, "Name", scopeName);
				result = false;
			}
			return result;
		}
	}
}
