namespace AspNetCore.ReportingServices.Rendering.RPLProcessing
{
	public class RPLTablixCornerCell : RPLTablixCell
	{
		protected int m_rowSpan = 1;

		public override int RowSpan
		{
			get
			{
				return this.m_rowSpan;
			}
			set
			{
				this.m_rowSpan = value;
			}
		}

		public RPLTablixCornerCell()
		{
		}

		public RPLTablixCornerCell(RPLItem element, byte elementState, int rowSpan, int colSpan)
			: base(element, elementState)
		{
			this.m_rowSpan = rowSpan;
			base.m_colSpan = colSpan;
		}
	}
}
