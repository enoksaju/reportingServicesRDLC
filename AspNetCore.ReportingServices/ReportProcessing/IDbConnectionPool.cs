using AspNetCore.ReportingServices.DataProcessing;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	public interface IDbConnectionPool
	{
		int ConnectionCount
		{
			get;
		}

		IDbConnection GetConnection(ConnectionKey connectionKey);

		bool PoolConnection(IDbPoolableConnection connection, ConnectionKey connectionKey);

		void CloseConnections();
	}
}
