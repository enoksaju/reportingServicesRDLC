using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.ReportRendering
{
	public sealed class TableCellCollection
	{
		private Table m_table;

		private TableCell[] m_cells;

		private ReportItemCollection m_cellReportItems;

		private AspNetCore.ReportingServices.ReportProcessing.TableRow m_rowDef;

		private TableRowInstance m_rowInstance;

		public TableCell this[int index]
		{
			get
			{
				if (index >= 0 && index < this.Count)
				{
					TableCell tableCell = null;
					if (this.m_cells == null || this.m_cells[index] == null)
					{
						tableCell = new TableCell((AspNetCore.ReportingServices.ReportProcessing.Table)this.m_table.ReportItemDef, index, this);
						if (this.m_table.RenderingContext.CacheState)
						{
							if (this.m_cells == null)
							{
								this.m_cells = new TableCell[this.Count];
							}
							this.m_cells[index] = tableCell;
						}
					}
					else
					{
						tableCell = this.m_cells[index];
					}
					return tableCell;
				}
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, index, 0, this.Count);
			}
		}

		public int Count
		{
			get
			{
				return this.m_rowDef.ReportItems.Count;
			}
		}

		public ReportItemCollection ReportItems
		{
			get
			{
				if (this.m_cellReportItems == null)
				{
					this.m_cellReportItems = new ReportItemCollection(this.m_rowDef.ReportItems, (this.m_rowInstance == null) ? null : this.m_rowInstance.TableRowReportItemColInstance, this.m_table.RenderingContext, null);
				}
				return this.m_cellReportItems;
			}
		}

		public IntList ColSpans
		{
			get
			{
				return this.m_rowDef.ColSpans;
			}
		}

		public AspNetCore.ReportingServices.ReportProcessing.TableRow RowDef
		{
			get
			{
				return this.m_rowDef;
			}
		}

		public RenderingContext RenderingContext
		{
			get
			{
				return this.m_table.RenderingContext;
			}
		}

		public TableCellCollection(Table table, AspNetCore.ReportingServices.ReportProcessing.TableRow rowDef, TableRowInstance rowInstance)
		{
			this.m_table = table;
			this.m_rowDef = rowDef;
			this.m_rowInstance = rowInstance;
		}
	}
}
