using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class DrillthroughInfo
	{
		private string m_reportName;

		private DrillthroughParameters m_reportParameters;

		public string ReportName
		{
			get
			{
				return this.m_reportName;
			}
		}

		public DrillthroughParameters ReportParameters
		{
			get
			{
				return this.m_reportParameters;
			}
		}

		public DrillthroughInfo(string reportName, DrillthroughParameters parameters)
		{
			this.m_reportName = reportName;
			this.m_reportParameters = parameters;
		}
	}
}
