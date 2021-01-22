using System.Globalization;

namespace AspNetCore.ReportingServices.Rendering.RichText
{
	public class TextBoxContext
	{
		public int ParagraphIndex;

		public int TextRunIndex;

		public int TextRunCharacterIndex;

		public TextBoxContext()
		{
		}

		public void IncrementParagraph()
		{
			this.ParagraphIndex++;
			this.TextRunIndex = 0;
			this.TextRunCharacterIndex = 0;
		}

		public TextBoxContext Clone()
		{
			TextBoxContext textBoxContext = new TextBoxContext();
			textBoxContext.ParagraphIndex = this.ParagraphIndex;
			textBoxContext.TextRunIndex = this.TextRunIndex;
			textBoxContext.TextRunCharacterIndex = this.TextRunCharacterIndex;
			return textBoxContext;
		}

		public void Reset()
		{
			this.ParagraphIndex = 0;
			this.TextRunIndex = 0;
			this.TextRunCharacterIndex = 0;
		}

		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "P:{0} TR:{1} NCI:{2}", this.ParagraphIndex, this.TextRunIndex, this.TextRunCharacterIndex);
		}
	}
}
