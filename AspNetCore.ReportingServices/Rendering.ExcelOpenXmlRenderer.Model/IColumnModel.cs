namespace AspNetCore.ReportingServices.Rendering.ExcelOpenXmlRenderer.Model
{
	public interface IColumnModel
	{
		ColumnProperties Interface
		{
			get;
		}

		double Width
		{
			set;
		}

		bool Hidden
		{
			set;
		}

		int OutlineLevel
		{
			set;
		}

		bool OutlineCollapsed
		{
			set;
		}
	}
}
