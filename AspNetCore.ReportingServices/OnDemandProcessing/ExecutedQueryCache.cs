using AspNetCore.ReportingServices.ReportIntermediateFormat;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.OnDemandProcessing
{
	public sealed class ExecutedQueryCache
	{
		private readonly List<ExecutedQuery> m_queries;

		public ExecutedQueryCache()
		{
			this.m_queries = new List<ExecutedQuery>();
		}

		public void Add(ExecutedQuery query)
		{
			int indexInCollection = query.DataSet.IndexInCollection;
			for (int i = this.m_queries.Count - 1; i <= indexInCollection; i++)
			{
				this.m_queries.Add(null);
			}
			this.m_queries[indexInCollection] = query;
		}

		public void Extract(DataSet dataSet, out ExecutedQuery query)
		{
			int indexInCollection = dataSet.IndexInCollection;
			if (indexInCollection >= this.m_queries.Count)
			{
				query = null;
			}
			else
			{
				query = this.m_queries[indexInCollection];
				this.m_queries[indexInCollection] = null;
			}
		}

		public void Close()
		{
			for (int i = 0; i < this.m_queries.Count; i++)
			{
				ExecutedQuery executedQuery = this.m_queries[i];
				if (executedQuery != null)
				{
					executedQuery.Close();
				}
				this.m_queries[i] = null;
			}
		}
	}
}
