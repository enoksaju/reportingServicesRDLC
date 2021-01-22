namespace AspNetCore.ReportingServices.Rendering.ExcelOpenXmlRenderer.Model
{
	public interface ICellModel
	{
		Cell Interface
		{
			get;
		}

		string Name
		{
			get;
		}

		Cell.CellValueType ValueType
		{
			get;
		}

		IStyleModel Style
		{
			get;
			set;
		}

		object Value
		{
			get;
			set;
		}

		ICharacterRunModel getCharacters(int startIndex, int length);
	}
}
