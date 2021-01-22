namespace AspNetCore.ReportingServices.ReportProcessing
{
	public interface IErrorContext
	{
		void Register(ProcessingErrorCode code, Severity severity, params string[] arguments);

		void Register(ProcessingErrorCode code, Severity severity, ObjectType objectType, string objectName, string propertyName, params string[] arguments);
	}
}
