using System;

namespace AspNetCore.Reporting
{
	public sealed class InitializeDataSourcesEventArgs : EventArgs
	{
		private ReportDataSourceCollection m_dataSources;

		public ReportDataSourceCollection DataSources
		{
			get
			{
				return this.m_dataSources;
			}
		}

		public InitializeDataSourcesEventArgs(ReportDataSourceCollection dataSources)
		{
			this.m_dataSources = dataSources;
		}
	}
}
