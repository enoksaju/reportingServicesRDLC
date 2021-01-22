using System.IO.Packaging;

namespace AspNetCore.ReportingServices.Rendering.ExcelOpenXmlRenderer.Model
{
	public interface IStreambookModel : IWorkbookModel
	{
		Package ZipPackage
		{
			get;
		}

		void Save();
	}
}
