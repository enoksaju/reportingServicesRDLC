using System.Collections;

namespace AspNetCore.Reporting
{
	public sealed class DataSourceCollectionWrapper : IEnumerable
	{
		private readonly ReportDataSourceCollection m_dsCollection;

		public DataSourceCollectionWrapper(ReportDataSourceCollection dsCollection)
		{
			this.m_dsCollection = dsCollection;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			foreach (ReportDataSource item in this.m_dsCollection)
			{
				yield return (object)new DataSourceWrapper(item);
			}
		}
	}
}
