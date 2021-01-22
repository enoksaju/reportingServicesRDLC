using System.IO;

namespace AspNetCore.ReportingServices.Rendering.ExcelRenderer.Excel.BIFF8
{
	public struct CellRenderingDetails
	{
		private BinaryWriter m_Writer;

		private int m_row;

		private short m_col;

		private ushort m_ixfe;

		public BinaryWriter Output
		{
			get
			{
				return this.m_Writer;
			}
		}

		public int Row
		{
			get
			{
				return this.m_row;
			}
		}

		public short Column
		{
			get
			{
				return this.m_col;
			}
		}

		public ushort Ixfe
		{
			get
			{
				return this.m_ixfe;
			}
		}

		public void Initialize(BinaryWriter writer, int row, short col, ushort ixfe)
		{
			this.m_Writer = writer;
			this.m_row = row;
			this.m_col = col;
			this.m_ixfe = ixfe;
		}
	}
}
