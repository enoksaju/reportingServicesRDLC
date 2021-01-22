namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class ChartSeriesCollection : ReportElementCollectionBase<ChartSeries>, IDataRegionRowCollection
	{
		protected Chart m_owner;

		protected ChartSeries[] m_chartSeriesCollection;

		public ChartSeriesCollection(Chart owner)
		{
			this.m_owner = owner;
		}

		IDataRegionRow IDataRegionRowCollection.GetIfExists(int seriesIndex)
		{
			if (this.m_chartSeriesCollection != null && seriesIndex >= 0 && seriesIndex < this.Count)
			{
				return this.m_chartSeriesCollection[seriesIndex];
			}
			return null;
		}
	}
}
