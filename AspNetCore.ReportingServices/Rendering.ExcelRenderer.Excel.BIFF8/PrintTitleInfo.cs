namespace AspNetCore.ReportingServices.Rendering.ExcelRenderer.Excel.BIFF8
{
	public class PrintTitleInfo
	{
		private ushort m_externSheetIndex;

		private ushort m_currentSheetIndex;

		private ushort m_firstRow;

		private ushort m_lastRow;

		public ushort ExternSheetIndex
		{
			get
			{
				return this.m_externSheetIndex;
			}
		}

		public ushort CurrentSheetIndex
		{
			get
			{
				return this.m_currentSheetIndex;
			}
		}

		public ushort FirstRow
		{
			get
			{
				return this.m_firstRow;
			}
		}

		public ushort LastRow
		{
			get
			{
				return this.m_lastRow;
			}
		}

		public PrintTitleInfo(ushort externSheetIndex, ushort currentSheetIndex, ushort firstRow, ushort lastRow)
		{
			this.m_externSheetIndex = externSheetIndex;
			this.m_currentSheetIndex = currentSheetIndex;
			this.m_firstRow = firstRow;
			this.m_lastRow = lastRow;
		}
	}
}
