using System.Collections.Generic;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	public interface IAggregateHolder
	{
		DataScopeInfo DataScopeInfo
		{
			get;
		}

		List<DataAggregateInfo> GetAggregateList();

		List<DataAggregateInfo> GetPostSortAggregateList();

		void ClearIfEmpty();
	}
}
