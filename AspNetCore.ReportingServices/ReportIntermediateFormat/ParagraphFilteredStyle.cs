namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	public sealed class ParagraphFilteredStyle : Style
	{
		public ParagraphFilteredStyle(Style style)
			: base(ConstructionPhase.Deserializing)
		{
			base.m_styleAttributes = style.StyleAttributes;
			base.m_expressionList = style.ExpressionList;
		}

		public override bool GetAttributeInfo(string styleAttributeName, out AttributeInfo styleAttribute)
		{
			switch (styleAttributeName)
			{
			case "TextAlign":
			case "LineHeight":
				return base.GetAttributeInfo(styleAttributeName, out styleAttribute);
			default:
				styleAttribute = null;
				return false;
			}
		}
	}
}
