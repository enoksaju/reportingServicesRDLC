using System;
using System.IO;

namespace AspNetCore.ReportingServices.Rendering.ExcelOpenXmlRenderer.Model
{
	public interface IWorksheetModel : ICloneable
	{
		Streamsheet Interface
		{
			get;
		}

		IWorkbookModel Workbook
		{
			get;
		}

		IPictureShapesModel Pictures
		{
			get;
		}

		IPageSetupModel PageSetup
		{
			get;
		}

		string Name
		{
			get;
			set;
		}

		int Position
		{
			get;
		}

		bool ShowGridlines
		{
			set;
		}

		IColumnModel getColumn(int index);

		AnchorModel createAnchor(int row, int column, double offsetX, double offsetY);

		void SetBackgroundPicture(string uniqueId, string extension, Stream pictureStream);
	}
}
