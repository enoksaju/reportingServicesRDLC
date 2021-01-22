namespace AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing
{
	public interface IDataRowHolder
	{
		void ReadRows(DataActions action, ITraversalContext context);

		void UpdateAggregates(AggregateUpdateContext context);

		void SetupEnvironment();
	}
}
