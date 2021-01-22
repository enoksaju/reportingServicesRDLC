using AspNetCore.ReportingServices.ReportProcessing;
using System;

namespace AspNetCore.ReportingServices.ReportRendering
{
	[Serializable]
	public abstract class Page
	{
		private PageSectionInstance m_pageHeaderInstance;

		private PageSectionInstance m_pageFooterInstance;

		[NonSerialized]
		private PageSection m_pageHeader;

		[NonSerialized]
		private PageSection m_pageFooter;

		public PageSection PageSectionHeader
		{
			get
			{
				return this.m_pageHeader;
			}
			set
			{
				this.m_pageHeader = value;
			}
		}

		public PageSection PageSectionFooter
		{
			get
			{
				return this.m_pageFooter;
			}
			set
			{
				this.m_pageFooter = value;
			}
		}

		public PageSectionInstance HeaderInstance
		{
			get
			{
				return this.m_pageHeaderInstance;
			}
		}

		public PageSectionInstance FooterInstance
		{
			get
			{
				return this.m_pageFooterInstance;
			}
		}

		public PageSection Header
		{
			get
			{
				return this.m_pageHeader;
			}
			set
			{
				this.m_pageHeader = value;
				if (value != null)
				{
					this.m_pageHeaderInstance = (PageSectionInstance)value.ReportItemInstance;
				}
			}
		}

		public PageSection Footer
		{
			get
			{
				return this.m_pageFooter;
			}
			set
			{
				this.m_pageFooter = value;
				if (value != null)
				{
					this.m_pageFooterInstance = (PageSectionInstance)value.ReportItemInstance;
				}
			}
		}

		protected Page(PageSection pageHeader, PageSection pageFooter)
		{
			this.Header = pageHeader;
			this.Footer = pageFooter;
		}
	}
}
