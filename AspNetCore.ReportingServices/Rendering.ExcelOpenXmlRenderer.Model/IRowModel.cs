using System.Collections.Generic;

namespace AspNetCore.ReportingServices.Rendering.ExcelOpenXmlRenderer.Model
{
	public interface IRowModel
	{
		Row RowInterface
		{
			get;
		}

		IWorksheetModel WorksheetModel
		{
			get;
		}

		IDictionary<int, ICellModel> CellsMap
		{
			get;
		}

		int RowNumber
		{
			get;
			set;
		}

		double Height
		{
			set;
		}

		int OutlineLevel
		{
			set;
		}

		bool Hidden
		{
			set;
		}

		bool OutlineCollapsed
		{
			set;
		}

		bool CustomHeight
		{
			set;
		}

		ICellModel getCell(int colNumber);

		void ClearCell(int column);
	}
}
