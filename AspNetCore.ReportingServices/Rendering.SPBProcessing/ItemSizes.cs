using AspNetCore.ReportingServices.OnDemandReportRendering;
using System.IO;

namespace AspNetCore.ReportingServices.Rendering.SPBProcessing
{
	public class ItemSizes
	{
		protected double m_deltaX;

		protected double m_deltaY;

		protected double m_left;

		protected double m_top;

		protected double m_width;

		protected double m_height;

		protected string m_id;

		public double DeltaX
		{
			get
			{
				return this.m_deltaX;
			}
			set
			{
				this.m_deltaX = value;
			}
		}

		public double DeltaY
		{
			get
			{
				return this.m_deltaY;
			}
			set
			{
				this.m_deltaY = value;
			}
		}

		public double Left
		{
			get
			{
				return this.m_left;
			}
			set
			{
				this.m_left = value;
			}
		}

		public double Top
		{
			get
			{
				return this.m_top;
			}
			set
			{
				this.m_top = value;
			}
		}

		public double Bottom
		{
			get
			{
				return this.m_top + this.m_height;
			}
		}

		public double Right
		{
			get
			{
				return this.m_left + this.m_width;
			}
		}

		public double Width
		{
			get
			{
				return this.m_width;
			}
			set
			{
				this.m_width = value;
			}
		}

		public double Height
		{
			get
			{
				return this.m_height;
			}
			set
			{
				this.m_height = value;
			}
		}

		public virtual double PadWidth
		{
			get
			{
				return this.m_width;
			}
		}

		public virtual double PadHeight
		{
			get
			{
				return this.m_height;
			}
		}

		public virtual double PaddingRight
		{
			get
			{
				return 0.0;
			}
			set
			{
			}
		}

		public virtual double PaddingBottom
		{
			get
			{
				return 0.0;
			}
			set
			{
			}
		}

		public string ID
		{
			get
			{
				return this.m_id;
			}
			set
			{
				this.m_id = value;
			}
		}

		public ItemSizes()
		{
		}

		public ItemSizes(ReportSize width, ReportSize height, string id)
		{
			this.m_width = width.ToMillimeters();
			this.m_height = height.ToMillimeters();
			this.m_id = id;
		}

		public ItemSizes(ReportItem reportItem)
		{
			this.m_top = reportItem.Top.ToMillimeters();
			this.m_left = reportItem.Left.ToMillimeters();
			this.m_width = reportItem.Width.ToMillimeters();
			this.m_height = reportItem.Height.ToMillimeters();
			this.m_id = reportItem.ID;
		}

		public ItemSizes(ItemSizes itemSizes)
		{
			this.m_top = itemSizes.Top;
			this.m_left = itemSizes.Left;
			this.m_width = itemSizes.Width;
			this.m_height = itemSizes.Height;
			this.m_deltaX = itemSizes.DeltaX;
			this.m_id = itemSizes.ID;
		}

		public ItemSizes(double top, double left, string id)
		{
			this.m_top = top;
			this.m_left = left;
			this.m_id = id;
		}

		public virtual ItemSizes GetNewItem()
		{
			ItemSizes itemSizes = new ItemSizes(this);
			itemSizes.DeltaY = this.m_deltaY;
			return itemSizes;
		}

		public virtual void Update(ReportSize width, ReportSize height)
		{
			this.Clean();
			this.m_width = width.ToMillimeters();
			this.m_height = height.ToMillimeters();
		}

		public virtual void Update(ReportItem reportItem)
		{
			this.Clean();
			this.m_top = reportItem.Top.ToMillimeters();
			this.m_left = reportItem.Left.ToMillimeters();
			this.m_width = reportItem.Width.ToMillimeters();
			this.m_height = reportItem.Height.ToMillimeters();
		}

		public virtual void Update(ItemSizes itemSizes, bool returnPaddings)
		{
			if (this != itemSizes)
			{
				this.Clean();
				this.m_top = itemSizes.Top;
				this.m_left = itemSizes.Left;
				this.m_width = itemSizes.Width;
				this.m_height = itemSizes.Height;
				this.m_deltaX = itemSizes.DeltaX;
			}
		}

		public virtual void Update(double top, double left)
		{
			this.Clean();
			this.m_top = top;
			this.m_left = left;
		}

		public virtual void Clean()
		{
			this.m_top = 0.0;
			this.m_left = 0.0;
			this.m_width = 0.0;
			this.m_height = 0.0;
			this.m_deltaX = 0.0;
			this.m_deltaY = 0.0;
		}

		public void AdjustHeightTo(double amount)
		{
			this.m_deltaY += amount - this.m_height;
			this.m_height = amount;
		}

		public void AdjustWidthTo(double amount)
		{
			this.m_deltaX += amount - this.m_width;
			this.m_width = amount;
		}

		public void MoveVertical(double delta)
		{
			this.m_top += delta;
			this.m_deltaY += delta;
		}

		public void MoveHorizontal(double delta)
		{
			this.m_left += delta;
			this.m_deltaX += delta;
		}

		public void UpdateSizes(double topDelta, PageItem owner, PageItem[] siblings, RepeatWithItem[] repeatWithItems)
		{
			ReportItem source = owner.Source;
			this.m_left = owner.DefLeftValue;
			this.m_width = owner.SourceWidthInMM;
			this.m_deltaY = 0.0;
			this.m_deltaX = 0.0;
			this.m_top -= topDelta;
			if (this.m_top < 0.0)
			{
				if (owner.ItemState == PageItem.State.TopNextPage || owner.ItemState == PageItem.State.SpanPages)
				{
					this.m_deltaY = 0.0 - this.m_top;
					this.m_top = 0.0;
				}
				else if (owner.ItemState == PageItem.State.Below && !owner.HasItemsAbove(siblings, repeatWithItems))
				{
					this.m_deltaY = 0.0 - this.m_top;
					this.m_top = 0.0;
				}
			}
		}

		public virtual void SetPaddings(double right, double bottom)
		{
		}

		public virtual int ReadPaginationInfo(BinaryReader reader, long offsetEndPage)
		{
			if (reader != null && offsetEndPage > 0)
			{
				this.m_deltaX = reader.ReadDouble();
				this.m_deltaY = reader.ReadDouble();
				this.m_top = reader.ReadDouble();
				this.m_left = reader.ReadDouble();
				this.m_height = reader.ReadDouble();
				this.m_width = reader.ReadDouble();
				if (reader.BaseStream.Position > offsetEndPage)
				{
					throw new InvalidDataException(SPBRes.InvalidPaginationStream);
				}
				return 0;
			}
			return -1;
		}

		public virtual void WritePaginationInfo(BinaryWriter reportPageInfo)
		{
			if (reportPageInfo != null)
			{
				reportPageInfo.Write((byte)1);
				reportPageInfo.Write(this.m_deltaX);
				reportPageInfo.Write(this.m_deltaY);
				reportPageInfo.Write(this.m_top);
				reportPageInfo.Write(this.m_left);
				reportPageInfo.Write(this.m_height);
				reportPageInfo.Write(this.m_width);
			}
		}

		public virtual ItemSizes WritePaginationInfo()
		{
			ItemSizes itemSizes = new ItemSizes(this);
			itemSizes.DeltaY = this.m_deltaY;
			itemSizes.ID = null;
			return itemSizes;
		}
	}
}
