namespace AspNetCore.ReportingServices.Rendering.ExcelOpenXmlRenderer.Model
{
	public interface ICharacterRunModel
	{
		CharacterRun Interface
		{
			get;
		}

		int StartIndex
		{
			get;
		}

		int Length
		{
			get;
		}

		void SetFont(Font font);
	}
}
