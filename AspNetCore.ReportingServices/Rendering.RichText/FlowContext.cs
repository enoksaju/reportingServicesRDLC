namespace AspNetCore.ReportingServices.Rendering.RichText
{
	public class FlowContext
	{
		public float Width;

		public float Height;

		public float ContentOffset;

		public bool WordTrim = true;

		public bool LineLimit = true;

		public float OmittedLineHeight;

		public bool AtEndOfTextBox;

		public TextBoxContext Context = new TextBoxContext();

		public TextBoxContext ClipContext;

		public bool Updatable;

		public bool VerticalCanGrow;

		public bool ForcedCharTrim;

		public bool CharTrimLastLine = true;

		public int CharTrimmedRunWidth;

		private FlowContext()
		{
		}

		public FlowContext(float width, float height)
		{
			this.Width = width;
			this.Height = height;
		}

		public FlowContext(float width, float height, int paragraphIndex, int runIndex, int runCharIndex)
		{
			this.Width = width;
			this.Height = height;
			this.Context.ParagraphIndex = paragraphIndex;
			this.Context.TextRunIndex = runIndex;
			this.Context.TextRunCharacterIndex = runCharIndex;
		}

		public FlowContext(float width, float height, bool wordTrim, bool lineLimit)
			: this(width, height)
		{
			this.WordTrim = wordTrim;
			this.LineLimit = lineLimit;
		}

		public FlowContext(float width, float height, TextBoxContext context)
			: this(width, height)
		{
			this.Context = context;
		}

		public FlowContext Clone()
		{
			FlowContext flowContext = (FlowContext)base.MemberwiseClone();
			flowContext.Context = this.Context.Clone();
			if (this.ClipContext != null)
			{
				flowContext.ClipContext = this.ClipContext.Clone();
			}
			return flowContext;
		}

		public void Reset()
		{
			this.Context.Reset();
			this.ClipContext = null;
			this.ContentOffset = 0f;
			this.AtEndOfTextBox = false;
			this.OmittedLineHeight = 0f;
			this.CharTrimmedRunWidth = 0;
			this.ForcedCharTrim = false;
		}
	}
}
