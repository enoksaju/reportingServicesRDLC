namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class TablixRow : ReportElementCollectionBase<TablixCell>, IDataRegionRow
	{
		protected Tablix m_owner;

		protected int m_rowIndex;

		public abstract ReportSize Height
		{
			get;
		}

		public TablixRow(Tablix owner, int rowIndex)
		{
			this.m_owner = owner;
			this.m_rowIndex = rowIndex;
		}

		IDataRegionCell IDataRegionRow.GetIfExists(int index)
		{
			return this.GetIfExists(index);
		}

		public virtual IDataRegionCell GetIfExists(int index)
		{
			if (index >= 0 && index < this.Count)
			{
				return ((ReportElementCollectionBase<TablixCell>)this)[index];
			}
			return null;
		}
	}
}
