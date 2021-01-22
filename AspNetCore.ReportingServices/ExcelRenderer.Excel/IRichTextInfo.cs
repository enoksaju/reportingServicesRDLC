namespace AspNetCore.ReportingServices.Rendering.ExcelRenderer.Excel
{
	public interface IRichTextInfo
	{
		bool CheckForRotatedFarEastChars
		{
			set;
		}

		IFont AppendTextRun(string value);

		IFont AppendTextRun(string value, bool replaceInvalidWhitespace);

		void AppendText(string value);

		void AppendText(string value, bool replaceInvalidWhiteSpace);

		void AppendText(char value);
	}
}
