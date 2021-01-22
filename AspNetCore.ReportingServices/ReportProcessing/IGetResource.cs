using AspNetCore.ReportingServices.Diagnostics;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	public interface IGetResource
	{
		void GetResource(ICatalogItemContext reportContext, string path, out byte[] resource, out string mimeType, out bool registerExternalWarning, out bool registerInvalidSizeWarning);
	}
}
