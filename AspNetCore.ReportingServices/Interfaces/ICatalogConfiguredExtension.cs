using System.Collections.Generic;

namespace AspNetCore.ReportingServices.Interfaces
{
	public interface ICatalogConfiguredExtension
	{
		void SetCatalogConfiguration(IDictionary<string, string> configuration);

		IEnumerable<string> EnumerateRequiredProperties();
	}
}
