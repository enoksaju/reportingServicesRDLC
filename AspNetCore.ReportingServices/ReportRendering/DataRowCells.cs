using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.ReportRendering
{
	public sealed class DataRowCells
	{
		private int m_count;

		private DataCell[] m_rowCells;

		public DataCell this[int index]
		{
			get
			{
				if (index >= 0 && index < this.m_count)
				{
					if (this.m_rowCells != null)
					{
						return this.m_rowCells[index];
					}
					return null;
				}
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, index, 0, this.m_count);
			}
			set
			{
				if (index >= 0 && index < this.m_count)
				{
					if (this.m_rowCells == null)
					{
						this.m_rowCells = new DataCell[this.m_count];
					}
					this.m_rowCells[index] = value;
					return;
				}
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, index, 0, this.m_count);
			}
		}

		public DataRowCells(int count)
		{
			this.m_count = count;
		}
	}
}
