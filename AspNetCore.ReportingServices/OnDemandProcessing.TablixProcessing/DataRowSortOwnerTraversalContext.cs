namespace AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing
{
	public class DataRowSortOwnerTraversalContext : ITraversalContext
	{
		private IDataRowSortOwner m_sortOwner;

		public IDataRowSortOwner SortOwner
		{
			get
			{
				return this.m_sortOwner;
			}
		}

		public DataRowSortOwnerTraversalContext(IDataRowSortOwner sortOwner)
		{
			this.m_sortOwner = sortOwner;
		}
	}
}
