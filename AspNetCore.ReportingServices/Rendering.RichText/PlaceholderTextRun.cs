using System.Drawing;

namespace AspNetCore.ReportingServices.Rendering.RichText
{
	public sealed class PlaceholderTextRun : HighlightTextRun
	{
		private Color m_placeholderBorderColor = Color.Empty;

		private bool m_allowColorInversion = true;

		public override bool IsPlaceholderTextRun
		{
			get
			{
				return true;
			}
		}

		public Color PlaceholderBorderColor
		{
			get
			{
				return this.m_placeholderBorderColor;
			}
			set
			{
				this.m_placeholderBorderColor = value;
			}
		}

		public override bool AllowColorInversion
		{
			get
			{
				return this.m_allowColorInversion;
			}
			set
			{
				this.m_allowColorInversion = value;
			}
		}

		public PlaceholderTextRun(string text, ITextRunProps props)
			: base(text, props)
		{
		}

		public PlaceholderTextRun(string text, TextRun textRun)
			: base(text, textRun.TextRunProperties)
		{
		}

		public PlaceholderTextRun(string text, PlaceholderTextRun textRun)
			: base(text, textRun)
		{
			this.m_placeholderBorderColor = textRun.PlaceholderBorderColor;
		}

		public PlaceholderTextRun(string text, PlaceholderTextRun textRun, SCRIPT_LOGATTR[] scriptLogAttr)
			: base(text, textRun, scriptLogAttr)
		{
			this.m_placeholderBorderColor = textRun.PlaceholderBorderColor;
		}

		public override TextRun Split(string text, SCRIPT_LOGATTR[] scriptLogAttr)
		{
			return new PlaceholderTextRun(text, this, scriptLogAttr);
		}

		public override TextRun GetSubRun(int startIndex, int length)
		{
			if (length == base.m_text.Length)
			{
				return this;
			}
			if (startIndex > 0)
			{
				base.m_textRunProps.AddSplitIndex(startIndex);
			}
			PlaceholderTextRun placeholderTextRun = new PlaceholderTextRun(base.m_text.Substring(startIndex, length), this);
			placeholderTextRun.CharacterIndexInOriginal = startIndex;
			return placeholderTextRun;
		}
	}
}
