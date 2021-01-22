using System.Collections;
using System.Data;

namespace AspNetCore.ReportingServices.Extensions
{
	public interface ICatalogQuery
	{
		void ExecuteNonQuery(string query, Hashtable parameters, CommandType type);

		IDataReader ExecuteReader(string query, Hashtable parameters, CommandType type);

		void Commit();
	}
}
