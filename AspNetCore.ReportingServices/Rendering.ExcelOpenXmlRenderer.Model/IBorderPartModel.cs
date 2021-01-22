using AspNetCore.ReportingServices.Rendering.ExcelOpenXmlRenderer.Parser.spreadsheetml.x2006.main;

namespace AspNetCore.ReportingServices.Rendering.ExcelOpenXmlRenderer.Model
{
	public interface IBorderPartModel
	{
		ColorModel Color
		{
			set;
		}

		ST_BorderStyle Style
		{
			set;
		}

		bool HasBeenModified
		{
			get;
		}
	}
}
