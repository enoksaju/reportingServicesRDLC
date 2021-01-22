namespace AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing
{
	public enum ProcessingStages
	{
		Grouping = 1,
		SortAndFilter,
		PreparePeerGroupRunningValues,
		RunningValues,
		UserSortFilter,
		UpdateAggregates,
		CreateGroupTree
	}
}
