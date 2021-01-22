using AspNetCore.ReportingServices.DataExtensions;
using AspNetCore.ReportingServices.DataProcessing;
using AspNetCore.ReportingServices.Diagnostics;
using AspNetCore.ReportingServices.Interfaces;
using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class ExecuteQueriesContext
	{
		private readonly IDbConnection m_connection;

		private readonly IProcessingDataExtensionConnection m_dataExtensionConnection;

		private readonly DataSourceInfo m_dataSourceInfo;

		private readonly CreateAndRegisterStream m_createAndRegisterStream;

		private readonly IJobContext m_jobContext;

		public IDbConnection Connection
		{
			get
			{
				return this.m_connection;
			}
		}

		public CreateAndRegisterStream CreateAndRegisterStream
		{
			get
			{
				return this.m_createAndRegisterStream;
			}
		}

		public IJobContext JobContext
		{
			get
			{
				return this.m_jobContext;
			}
		}

		public ExecuteQueriesContext(IDbConnection connection, IProcessingDataExtensionConnection dataExtensionConnection, DataSourceInfo dataSourceInfo, CreateAndRegisterStream createAndRegisterStream, IJobContext jobContext)
		{
			this.m_connection = connection;
			this.m_dataExtensionConnection = dataExtensionConnection;
			this.m_dataSourceInfo = dataSourceInfo;
			this.m_createAndRegisterStream = createAndRegisterStream;
			this.m_jobContext = jobContext;
		}

		public IDbCommand CreateCommandWrapperForCancel(IDbCommand command)
		{
			return new CommandWrappedForCancel(command, this.m_dataExtensionConnection, null, this.m_dataSourceInfo, null, this.m_connection);
		}
	}
}
