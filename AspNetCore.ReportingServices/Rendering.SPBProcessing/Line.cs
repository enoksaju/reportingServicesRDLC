using AspNetCore.ReportingServices.OnDemandReportRendering;
using AspNetCore.ReportingServices.Rendering.RPLProcessing;
using System;
using System.IO;

namespace AspNetCore.ReportingServices.Rendering.SPBProcessing
{
	public sealed class Line : PageItem
	{
		public Line(AspNetCore.ReportingServices.OnDemandReportRendering.Line source, PageContext pageContext, bool createForRepeat)
			: base(source)
		{
			if (pageContext != null)
			{
				if (createForRepeat)
				{
					base.m_itemPageSizes = pageContext.GetSharedFromRepeatItemSizesElement(source, false);
				}
				else
				{
					base.m_itemPageSizes = pageContext.GetSharedItemSizesElement(source, false);
				}
			}
			else
			{
				base.m_itemPageSizes = new ItemSizes(source);
			}
		}

		public override bool CalculatePage(RPLWriter rplWriter, PageItemHelper lastPageInfo, PageContext pageContext, PageItem[] siblings, RepeatWithItem[] repeatWithItems, double parentTopInPage, ref double parentPageHeight, Interactivity interactivity)
		{
			base.AdjustOriginFromItemsAbove(siblings, repeatWithItems);
			if (!this.HitsCurrentPage(pageContext, parentTopInPage))
			{
				return false;
			}
			ItemSizes contentSize = null;
			bool flag = base.ResolveItemHiddenState(rplWriter, interactivity, pageContext, false, ref contentSize);
			parentPageHeight = Math.Max(parentPageHeight, base.m_itemPageSizes.Bottom);
			if (rplWriter != null)
			{
				if (base.m_itemRenderSizes == null)
				{
					this.CreateItemRenderSizes(contentSize, pageContext, false);
				}
				if (!flag)
				{
					this.WriteItemToStream(rplWriter, pageContext);
				}
			}
			return true;
		}

		public override void CalculateRepeatWithPage(RPLWriter rplWriter, PageContext pageContext, PageItem[] siblings)
		{
			base.AdjustOriginFromItemsAbove(siblings, null);
			ItemSizes contentSize = null;
			base.ResolveItemHiddenState(rplWriter, null, pageContext, true, ref contentSize);
			if (base.m_itemRenderSizes == null)
			{
				this.CreateItemRenderSizes(contentSize, pageContext, true);
			}
		}

		public override int WriteRepeatWithToPage(RPLWriter rplWriter, PageContext pageContext)
		{
			if (base.ItemState == State.OnPageHidden)
			{
				return 0;
			}
			this.WriteItemToStream(rplWriter, pageContext);
			return 1;
		}

		public void WriteItemToStream(RPLWriter rplWriter, PageContext pageContext)
		{
			BinaryWriter binaryWriter = rplWriter.BinaryWriter;
			if (binaryWriter != null)
			{
				Stream baseStream = binaryWriter.BaseStream;
				long position = baseStream.Position;
				binaryWriter.Write((byte)8);
				this.WriteElementProps(binaryWriter, rplWriter, pageContext, position + 1);
				base.m_offset = baseStream.Position;
				binaryWriter.Write((byte)254);
				binaryWriter.Write(position);
				binaryWriter.Write((byte)255);
			}
			else
			{
				base.m_rplElement = new RPLLine();
				this.WriteElementProps(base.m_rplElement.ElementProps, rplWriter, pageContext);
			}
		}

		public override void WriteCustomSharedItemProps(BinaryWriter spbifWriter, RPLWriter rplWriter, PageContext pageContext)
		{
			if (((AspNetCore.ReportingServices.OnDemandReportRendering.Line)base.m_source).Slant)
			{
				spbifWriter.Write((byte)24);
				spbifWriter.Write(true);
			}
		}

		public override void WriteCustomSharedItemProps(RPLElementPropsDef sharedProps, RPLWriter rplWriter, PageContext pageContext)
		{
			((RPLLinePropsDef)sharedProps).Slant = ((AspNetCore.ReportingServices.OnDemandReportRendering.Line)base.m_source).Slant;
		}

		public override void WritePaginationInfo(BinaryWriter reportPageInfo)
		{
			if (reportPageInfo != null)
			{
				reportPageInfo.Write((byte)8);
				base.WritePaginationInfoProperties(reportPageInfo);
				reportPageInfo.Write((byte)255);
			}
		}

		public override PageItemHelper WritePaginationInfo()
		{
			PageItemHelper pageItemHelper = new PageItemHelper(8);
			base.WritePaginationInfoProperties(pageItemHelper);
			return pageItemHelper;
		}
	}
}
