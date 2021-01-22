namespace AspNetCore.ReportingServices.Rendering.ExcelOpenXmlRenderer.Model
{
	public interface IWorkbookModel
	{
		IWorksheetsModel Worksheets
		{
			get;
		}

		IPaletteModel Palette
		{
			get;
		}

		IWorksheetModel getWorksheet(int sheetOffset);

		IStyleModel createGlobalStyle();
	}
}
