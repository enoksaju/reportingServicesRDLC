using System.Collections.Generic;

namespace AspNetCore.ReportingServices.Interfaces
{
	public interface IReportDefinitionCustomizationExtension : IExtension
	{
		bool ProcessReportDefinition(byte[] reportDefinition, IReportContext reportContext, IUserContext userContext, out byte[] reportDefinitionProcessed, out IEnumerable<RdceCustomizableElementId> customizedElementIds);
	}
}
