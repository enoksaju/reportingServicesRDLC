namespace AspNetCore.ReportingServices.ReportProcessing
{
	public interface IAggregateHolder
	{
		DataAggregateInfoList[] GetAggregateLists();

		DataAggregateInfoList[] GetPostSortAggregateLists();

		void ClearIfEmpty();
	}
}
