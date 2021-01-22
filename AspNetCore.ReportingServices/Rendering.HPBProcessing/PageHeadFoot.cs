using AspNetCore.ReportingServices.OnDemandReportRendering;
using AspNetCore.ReportingServices.Rendering.RPLProcessing;
using System.IO;

namespace AspNetCore.ReportingServices.Rendering.HPBProcessing
{
	public class PageHeadFoot : PageItemContainer
	{
		private new PageSection m_source;

		private bool m_isHeader;

		public override string SourceUniqueName
		{
			get
			{
				return this.m_source.Instance.UniqueName;
			}
		}

		public override string SourceID
		{
			get
			{
				return this.m_source.ID;
			}
		}

		public override Style SharedStyle
		{
			get
			{
				return this.m_source.Style;
			}
		}

		public override StyleInstance NonSharedStyle
		{
			get
			{
				return this.m_source.Instance.Style;
			}
		}

		public override double OriginalLeft
		{
			get
			{
				return 0.0;
			}
		}

		public override double OriginalWidth
		{
			get
			{
				return base.m_itemPageSizes.Width;
			}
		}

		public override byte RPLFormatType
		{
			get
			{
				if (this.m_isHeader)
				{
					return 4;
				}
				return 5;
			}
		}

		public PageHeadFoot(PageSection source, double width, bool aIsHeader)
			: base(null)
		{
			base.m_itemPageSizes = new FixedItemSizes(width, source.Height.ToMillimeters());
			this.m_source = source;
			this.m_isHeader = aIsHeader;
		}

		protected override void CreateChildren(PageContext pageContext)
		{
			base.CreateChildren(this.m_source.ReportItemCollection, pageContext);
		}

		public override bool HitsCurrentPage(double pageLeft, double pageTop, double pageRight, double pageBottom)
		{
			return true;
		}

		public override RPLElement CreateRPLElement()
		{
			return new RPLHeaderFooter();
		}

		public override RPLElement CreateRPLElement(RPLElementProps props, PageContext pageContext)
		{
			RPLElement rPLElement = this.CreateRPLElement();
			base.WriteElementProps(rPLElement.ElementProps, pageContext);
			return rPLElement;
		}

		public override void WriteItemSharedStyleProps(BinaryWriter spbifWriter, Style style, PageContext pageContext)
		{
			base.WriteStyleProp(style, spbifWriter, StyleAttributeNames.BackgroundColor, 34);
			base.WriteBackgroundImage(style, true, spbifWriter, pageContext);
		}

		public override void WriteItemSharedStyleProps(RPLStyleProps rplStyleProps, Style style, PageContext pageContext)
		{
			PageItem.WriteStyleProp(style, rplStyleProps, StyleAttributeNames.BackgroundColor, 34);
			base.WriteBackgroundImage(style, true, rplStyleProps, pageContext);
		}

		public override void WriteItemNonSharedStyleProps(BinaryWriter spbifWriter, Style styleDef, StyleInstance style, StyleAttributeNames styleAtt, PageContext pageContext)
		{
			switch (styleAtt)
			{
			case StyleAttributeNames.BackgroundColor:
				base.WriteStyleProp(styleDef, style, spbifWriter, StyleAttributeNames.BackgroundColor, 34);
				break;
			case StyleAttributeNames.BackgroundImage:
				base.WriteBackgroundImage(styleDef, false, spbifWriter, pageContext);
				break;
			}
		}

		public override void WriteItemNonSharedStyleProps(RPLStyleProps rplStyleProps, Style styleDef, StyleInstance style, StyleAttributeNames styleAtt, PageContext pageContext)
		{
			switch (styleAtt)
			{
			case StyleAttributeNames.BackgroundColor:
				base.WriteStyleProp(styleDef, style, rplStyleProps, StyleAttributeNames.BackgroundColor, 34);
				break;
			case StyleAttributeNames.BackgroundImage:
				base.WriteBackgroundImage(styleDef, false, rplStyleProps, pageContext);
				break;
			}
		}
	}
}
