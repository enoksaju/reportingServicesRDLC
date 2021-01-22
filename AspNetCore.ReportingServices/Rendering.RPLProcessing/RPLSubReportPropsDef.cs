namespace AspNetCore.ReportingServices.Rendering.RPLProcessing
{
	public sealed class RPLSubReportPropsDef : RPLItemPropsDef
	{
		private string m_reportName;

		public string ReportName
		{
			get
			{
				return this.m_reportName;
			}
			set
			{
				this.m_reportName = value;
			}
		}

		public RPLSubReportPropsDef()
		{
		}
	}
}
