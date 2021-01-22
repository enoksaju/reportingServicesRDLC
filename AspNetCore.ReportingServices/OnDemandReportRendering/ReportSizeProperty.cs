using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class ReportSizeProperty : ReportProperty
	{
		private ReportSize m_value;

		public ReportSize Value
		{
			get
			{
				return this.m_value;
			}
		}

		public ReportSizeProperty(bool isExpression, string expressionString, ReportSize value)
			: this(isExpression, expressionString, value, null)
		{
		}

		public ReportSizeProperty(bool isExpression, string expressionString, ReportSize value, ReportSize defaultValue)
			: base(isExpression, expressionString)
		{
			if (!isExpression)
			{
				this.m_value = value;
			}
			else
			{
				this.m_value = defaultValue;
			}
		}

		public ReportSizeProperty(ExpressionInfo expressionInfo)
			: base(expressionInfo != null && expressionInfo.IsExpression, (expressionInfo == null) ? null : expressionInfo.OriginalText)
		{
			if (expressionInfo != null && !expressionInfo.IsExpression)
			{
				this.m_value = new ReportSize(expressionInfo.StringValue);
			}
		}

		public ReportSizeProperty(ExpressionInfo expressionInfo, bool allowNegative)
			: base(expressionInfo != null && expressionInfo.IsExpression, (expressionInfo == null) ? null : expressionInfo.OriginalText)
		{
			if (expressionInfo != null && !expressionInfo.IsExpression)
			{
				this.m_value = new ReportSize(expressionInfo.StringValue, true, allowNegative);
			}
		}

		public ReportSizeProperty(ExpressionInfo expressionInfo, ReportSize defaultValue)
			: base(expressionInfo != null && expressionInfo.IsExpression, (expressionInfo == null) ? defaultValue.ToString() : expressionInfo.OriginalText)
		{
			if (expressionInfo != null && !expressionInfo.IsExpression)
			{
				this.m_value = new ReportSize(expressionInfo.StringValue);
			}
			else
			{
				this.m_value = defaultValue;
			}
		}
	}
}
