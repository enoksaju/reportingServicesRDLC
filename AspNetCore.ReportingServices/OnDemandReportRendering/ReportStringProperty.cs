using AspNetCore.ReportingServices.ReportIntermediateFormat;
using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class ReportStringProperty : ReportProperty
	{
		private string m_value;

		public string Value
		{
			get
			{
				return this.m_value;
			}
		}

		public ReportStringProperty()
		{
			this.m_value = null;
		}

		public ReportStringProperty(bool isExpression, string expressionString, string value)
			: this(isExpression, expressionString, value, null)
		{
		}

		public ReportStringProperty(bool isExpression, string expressionString, string value, string defaultValue)
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

		public ReportStringProperty(AspNetCore.ReportingServices.ReportProcessing.ExpressionInfo expression)
			: base(expression != null && expression.IsExpression, (expression == null) ? null : expression.OriginalText)
		{
			if (expression != null && !expression.IsExpression)
			{
				this.m_value = expression.Value;
			}
		}

		public ReportStringProperty(AspNetCore.ReportingServices.ReportProcessing.ExpressionInfo expression, string formulaText)
			: base(expression != null && expression.IsExpression, (expression == null) ? null : ((expression.IsExpression && expression.OriginalText == null) ? formulaText : expression.OriginalText))
		{
			if (expression != null && !expression.IsExpression)
			{
				this.m_value = expression.Value;
			}
		}

		public ReportStringProperty(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
			: base(expression != null && expression.IsExpression, (expression == null) ? null : expression.OriginalText)
		{
			if (expression != null && !expression.IsExpression)
			{
				if (expression.ConstantType != DataType.String)
				{
					this.m_value = expression.OriginalText;
				}
				else
				{
					this.m_value = expression.StringValue;
				}
			}
		}
	}
}
