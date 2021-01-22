namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class ReportColorProperty : ReportProperty
	{
		private ReportColor m_value;

		public ReportColor Value
		{
			get
			{
				return this.m_value;
			}
		}

		public ReportColorProperty(bool isExpression, string expressionString, ReportColor value, ReportColor defaultValue)
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
	}
}
