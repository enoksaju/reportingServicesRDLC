namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class TablixCorner
	{
		private Tablix m_owner;

		private TablixCornerRowCollection m_rowCollection;

		public TablixCornerRowCollection RowCollection
		{
			get
			{
				if (this.m_rowCollection == null)
				{
					this.m_rowCollection = new TablixCornerRowCollection(this.m_owner);
				}
				return this.m_rowCollection;
			}
		}

		public TablixCorner(Tablix owner)
		{
			this.m_owner = owner;
		}

		public void ResetContext()
		{
			if (this.m_rowCollection != null)
			{
				this.m_rowCollection.ResetContext();
			}
		}

		public void SetNewContext()
		{
			if (this.m_rowCollection != null)
			{
				this.m_rowCollection.SetNewContext();
			}
		}
	}
}
