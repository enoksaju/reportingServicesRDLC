using AspNetCore.ReportingServices.Rendering.ExcelOpenXmlRenderer.Parser;

namespace AspNetCore.ReportingServices.Rendering.ExcelOpenXmlRenderer.Models
{
	public interface IOoxmlCtWrapperModel
	{
		OoxmlComplexType OoxmlTag
		{
			get;
		}

		void Cleanup();
	}
}
