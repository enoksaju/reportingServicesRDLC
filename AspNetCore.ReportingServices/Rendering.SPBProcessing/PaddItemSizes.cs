using AspNetCore.ReportingServices.OnDemandReportRendering;
using System.IO;

namespace AspNetCore.ReportingServices.Rendering.SPBProcessing
{
	public sealed class PaddItemSizes : ItemSizes
	{
		private double m_paddingRight;

		private double m_paddingBottom;

		public override double PadWidth
		{
			get
			{
				return base.m_width - this.m_paddingRight;
			}
		}

		public override double PadHeight
		{
			get
			{
				return base.m_height - this.m_paddingBottom;
			}
		}

		public override double PaddingRight
		{
			get
			{
				return this.m_paddingRight;
			}
			set
			{
				this.m_paddingRight = value;
			}
		}

		public override double PaddingBottom
		{
			get
			{
				return this.m_paddingBottom;
			}
			set
			{
				this.m_paddingBottom = value;
			}
		}

		public PaddItemSizes()
		{
		}

		public PaddItemSizes(ReportItem reportItem)
			: base(reportItem)
		{
		}

		public PaddItemSizes(PaddItemSizes paddItemSizes)
			: base(paddItemSizes)
		{
			this.m_paddingRight = paddItemSizes.PaddingRight;
			this.m_paddingBottom = paddItemSizes.PaddingBottom;
		}

		public PaddItemSizes(ItemSizes paddItemSizes)
			: base(paddItemSizes)
		{
		}

		public PaddItemSizes(ReportSize width, ReportSize height, string id)
			: base(width, height, id)
		{
		}

		public override ItemSizes GetNewItem()
		{
			PaddItemSizes paddItemSizes = new PaddItemSizes(this);
			paddItemSizes.DeltaY = base.m_deltaY;
			return paddItemSizes;
		}

		public override void Update(ReportItem reportItem)
		{
			this.Clean();
			base.Update(reportItem);
		}

		public override void Update(ItemSizes paddItemSizes, bool returnPaddings)
		{
			this.Clean();
			base.Update(paddItemSizes, returnPaddings);
			if (returnPaddings)
			{
				PaddItemSizes paddItemSizes2 = paddItemSizes as PaddItemSizes;
				if (paddItemSizes2 != null)
				{
					this.m_paddingRight = paddItemSizes.PaddingRight;
					this.m_paddingBottom = paddItemSizes.PaddingBottom;
				}
			}
		}

		public override void Update(ReportSize width, ReportSize height)
		{
			this.Clean();
			base.Update(width, height);
		}

		public override void Clean()
		{
			base.Clean();
			this.m_paddingRight = 0.0;
			this.m_paddingBottom = 0.0;
		}

		public override void SetPaddings(double right, double bottom)
		{
			this.m_paddingRight = right;
			this.m_paddingBottom = bottom;
			if (this.m_paddingRight < 0.0)
			{
				this.m_paddingRight = 0.0;
			}
			if (this.m_paddingBottom < 0.0)
			{
				this.m_paddingBottom = 0.0;
			}
		}

		public override int ReadPaginationInfo(BinaryReader reader, long offsetEndPage)
		{
			if (reader != null && offsetEndPage > 0)
			{
				base.m_deltaX = reader.ReadDouble();
				base.m_deltaY = reader.ReadDouble();
				base.m_top = reader.ReadDouble();
				base.m_left = reader.ReadDouble();
				base.m_height = reader.ReadDouble();
				base.m_width = reader.ReadDouble();
				this.m_paddingBottom = reader.ReadDouble();
				this.m_paddingRight = reader.ReadDouble();
				if (reader.BaseStream.Position > offsetEndPage)
				{
					throw new InvalidDataException(SPBRes.InvalidPaginationStream);
				}
				return 0;
			}
			return -1;
		}

		public override void WritePaginationInfo(BinaryWriter reportPageInfo)
		{
			if (reportPageInfo != null)
			{
				reportPageInfo.Write((byte)2);
				reportPageInfo.Write(base.m_deltaX);
				reportPageInfo.Write(base.m_deltaY);
				reportPageInfo.Write(base.m_top);
				reportPageInfo.Write(base.m_left);
				reportPageInfo.Write(base.m_height);
				reportPageInfo.Write(base.m_width);
				reportPageInfo.Write(this.m_paddingBottom);
				reportPageInfo.Write(this.m_paddingRight);
			}
		}

		public override ItemSizes WritePaginationInfo()
		{
			PaddItemSizes paddItemSizes = new PaddItemSizes(this);
			paddItemSizes.DeltaY = base.m_deltaY;
			paddItemSizes.ID = null;
			return paddItemSizes;
		}
	}
}
