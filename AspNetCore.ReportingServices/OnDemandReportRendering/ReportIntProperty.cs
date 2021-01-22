using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class ReportIntProperty : ReportProperty
	{
		private int m_value;

		public int Value
		{
			get
			{
				return this.m_value;
			}
		}

		public ReportIntProperty(int value)
		{
			this.m_value = value;
		}

		public ReportIntProperty(bool isExpression, string expressionString, int value, int defaultValue)
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

		public ReportIntProperty(ExpressionInfo expression)
			: base(expression != null && expression.IsExpression, (expression == null) ? null : expression.OriginalText)
		{
			if (expression != null && !expression.IsExpression)
			{
				this.m_value = expression.IntValue;
			}
		}
	}
}
