namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class ReportUrlProperty : ReportProperty
	{
		private ReportUrl m_reportUrl;

		public ReportUrl Value
		{
			get
			{
				return this.m_reportUrl;
			}
		}

		public ReportUrlProperty(bool isExpression, string expressionString, ReportUrl reportUrl)
			: base(isExpression, expressionString)
		{
			if (!isExpression)
			{
				this.m_reportUrl = reportUrl;
			}
		}
	}
}
