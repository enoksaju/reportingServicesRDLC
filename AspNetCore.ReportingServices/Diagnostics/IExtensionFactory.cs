using AspNetCore.ReportingServices.Interfaces;

namespace AspNetCore.ReportingServices.Diagnostics
{
	public interface IExtensionFactory
	{
		bool IsRegisteredCustomReportItemExtension(string extensionType);

		object GetNewCustomReportItemProcessingInstanceClass(string reportItemName);

		IExtension GetNewRendererExtensionClass(string format);
	}
}
