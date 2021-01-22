using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.ReportRendering
{
	public sealed class MatrixRowCells
	{
		private int m_count;

		private MatrixCell[] m_matrixRowCells;

		public MatrixCell this[int index]
		{
			get
			{
				if (index >= 0 && index < this.m_count)
				{
					if (this.m_matrixRowCells != null)
					{
						return this.m_matrixRowCells[index];
					}
					return null;
				}
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, index, 0, this.m_count);
			}
			set
			{
				if (index >= 0 && index < this.m_count)
				{
					if (this.m_matrixRowCells == null)
					{
						this.m_matrixRowCells = new MatrixCell[this.m_count];
					}
					this.m_matrixRowCells[index] = value;
					return;
				}
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, index, 0, this.m_count);
			}
		}

		public MatrixRowCells(int count)
		{
			this.m_count = count;
		}
	}
}
