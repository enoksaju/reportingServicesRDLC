namespace AspNetCore.ReportingServices.Rendering.ExcelRenderer.Excel
{
	public interface IFont
	{
		int Bold
		{
			set;
		}

		bool Italic
		{
			set;
		}

		bool Strikethrough
		{
			set;
		}

		ScriptStyle ScriptStyle
		{
			set;
		}

		IColor Color
		{
			set;
		}

		Underline Underline
		{
			set;
		}

		string Name
		{
			set;
		}

		double Size
		{
			set;
		}
	}
}
