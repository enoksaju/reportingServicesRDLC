using AspNetCore.ReportingServices.Rendering.RPLProcessing;
using System;

namespace AspNetCore.ReportingServices.Rendering.HtmlRenderer
{
	public sealed class PageTableCell
	{
		[NonSerialized]
		private RoundedFloat m_x = new RoundedFloat(0f);

		[NonSerialized]
		private RoundedFloat m_y = new RoundedFloat(0f);

		private RoundedFloat m_dx = new RoundedFloat(0f);

		private RoundedFloat m_dy = new RoundedFloat(0f);

		private bool m_consumedByEmptyWhiteSpace;

		private int m_rowSpan = 1;

		private int m_colSpan = 1;

		private bool m_keepRightBorder;

		private bool m_keepBottomBorder;

		private bool m_fInUse;

		private bool m_fEaten;

		private bool m_vertMerge;

		private bool m_horzMerge;

		private bool m_firstHorzMerge;

		private bool m_firstVertMerge;

		[NonSerialized]
		private int m_usedCell = -1;

		private RPLItemMeasurement m_measurement;

		private RPLLine m_borderLeft;

		private RPLLine m_borderTop;

		private RPLLine m_borderRight;

		private RPLLine m_borderBottom;

		public bool VertMerge
		{
			get
			{
				return this.m_vertMerge;
			}
			set
			{
				this.m_vertMerge = value;
			}
		}

		public bool HorzMerge
		{
			get
			{
				return this.m_horzMerge;
			}
			set
			{
				this.m_horzMerge = value;
			}
		}

		public bool FirstHorzMerge
		{
			get
			{
				return this.m_firstHorzMerge;
			}
			set
			{
				this.m_firstHorzMerge = value;
			}
		}

		public bool FirstVertMerge
		{
			get
			{
				return this.m_firstVertMerge;
			}
			set
			{
				this.m_firstVertMerge = value;
			}
		}

		public bool KeepRightBorder
		{
			get
			{
				return this.m_keepRightBorder;
			}
			set
			{
				this.m_keepRightBorder = value;
			}
		}

		public bool KeepBottomBorder
		{
			get
			{
				return this.m_keepBottomBorder;
			}
			set
			{
				this.m_keepBottomBorder = value;
			}
		}

		public bool ConsumedByEmptyWhiteSpace
		{
			get
			{
				return this.m_consumedByEmptyWhiteSpace;
			}
			set
			{
				this.m_consumedByEmptyWhiteSpace = value;
			}
		}

		public bool HasBorder
		{
			get
			{
				if (this.m_borderLeft == null && this.m_borderTop == null && this.m_borderRight == null)
				{
					return this.m_borderBottom != null;
				}
				return true;
			}
		}

		public RPLLine BorderLeft
		{
			get
			{
				return this.m_borderLeft;
			}
			set
			{
				this.m_borderLeft = value;
			}
		}

		public RPLLine BorderRight
		{
			get
			{
				return this.m_borderRight;
			}
			set
			{
				this.m_borderRight = value;
			}
		}

		public RPLLine BorderBottom
		{
			get
			{
				return this.m_borderBottom;
			}
			set
			{
				this.m_borderBottom = value;
			}
		}

		public RPLLine BorderTop
		{
			get
			{
				return this.m_borderTop;
			}
			set
			{
				this.m_borderTop = value;
			}
		}

		public int UsedCell
		{
			get
			{
				return this.m_usedCell;
			}
			set
			{
				this.m_usedCell = value;
			}
		}

		public RoundedFloat XValue
		{
			get
			{
				return this.m_x;
			}
		}

		public RoundedFloat DXValue
		{
			get
			{
				return this.m_dx;
			}
		}

		public RoundedFloat YValue
		{
			get
			{
				return this.m_y;
			}
		}

		public RoundedFloat DYValue
		{
			get
			{
				return this.m_dy;
			}
		}

		public int RowSpan
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

		public int ColSpan
		{
			get
			{
				return this.m_colSpan;
			}
			set
			{
				this.m_colSpan = value;
			}
		}

		public bool InUse
		{
			get
			{
				return this.m_fInUse;
			}
			set
			{
				this.m_fInUse = value;
			}
		}

		public bool Eaten
		{
			get
			{
				return this.m_fEaten;
			}
			set
			{
				this.m_fEaten = value;
			}
		}

		public RPLItemMeasurement Measurement
		{
			get
			{
				return this.m_measurement;
			}
		}

		public bool NeedsRowHeight
		{
			get
			{
				if (!this.Eaten && this.InUse && this.RowSpan == 1)
				{
					RPLItemMeasurement measurement = this.Measurement;
					if (measurement != null)
					{
						RPLElement element = measurement.Element;
						if (!(element is RPLTablix) && !(element is RPLSubReport) && !(element is RPLRectangle))
						{
							goto IL_0044;
						}
						return false;
					}
				}
				goto IL_0044;
				IL_0044:
				return true;
			}
		}

		public PageTableCell(float x, float y, float dx, float dy)
		{
			this.m_x.Value = x;
			this.m_y.Value = y;
			this.m_dx.Value = dx;
			this.m_dy.Value = dy;
		}

		public void MarkCellEaten(int index)
		{
			this.m_fEaten = true;
			this.m_usedCell = index;
		}

		public void MarkCellUsed(RPLItemMeasurement measurement, int colSpan, int rowSpan, int index)
		{
			this.m_colSpan = colSpan;
			this.m_rowSpan = rowSpan;
			this.m_usedCell = index;
			this.m_measurement = measurement;
			this.m_fInUse = true;
		}
	}
}
