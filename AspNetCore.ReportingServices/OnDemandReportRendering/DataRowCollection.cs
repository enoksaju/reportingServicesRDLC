namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class DataRowCollection : ReportElementCollectionBase<DataRow>, IDataRegionRowCollection
	{
		protected CustomReportItem m_owner;

		protected DataRow[] m_cachedDataRows;

		public DataRowCollection(CustomReportItem owner)
		{
			this.m_owner = owner;
		}

		IDataRegionRow IDataRegionRowCollection.GetIfExists(int rowIndex)
		{
			if (this.m_cachedDataRows != null && rowIndex >= 0 && rowIndex < this.Count)
			{
				return this.m_cachedDataRows[rowIndex];
			}
			return null;
		}
	}
}
