using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class PageBreak
	{
		private ReportBoolProperty m_resetPageNumber;

		private ReportBoolProperty m_disabled;

		private RenderingContext m_renderingContext;

		private IReportScope m_reportScope;

		private IPageBreakOwner m_pageBreakOwner;

		private AspNetCore.ReportingServices.ReportIntermediateFormat.PageBreak m_pageBreakDef;

		private PageBreakLocation m_pageBreaklocation;

		private bool m_isOldSnapshotOrStaticMember;

		private PageBreakInstance m_pageBreakInstance;

		public PageBreakLocation BreakLocation
		{
			get
			{
				if (this.m_isOldSnapshotOrStaticMember)
				{
					return this.m_pageBreaklocation;
				}
				return this.m_pageBreakDef.BreakLocation;
			}
		}

		public ReportBoolProperty Disabled
		{
			get
			{
				if (this.m_disabled == null)
				{
					if (this.m_isOldSnapshotOrStaticMember)
					{
						this.m_disabled = new ReportBoolProperty();
					}
					else
					{
						this.m_disabled = new ReportBoolProperty(this.m_pageBreakDef.Disabled);
					}
				}
				return this.m_disabled;
			}
		}

		public ReportBoolProperty ResetPageNumber
		{
			get
			{
				if (this.m_resetPageNumber == null)
				{
					if (this.m_isOldSnapshotOrStaticMember)
					{
						this.m_resetPageNumber = new ReportBoolProperty();
					}
					else
					{
						this.m_resetPageNumber = new ReportBoolProperty(this.m_pageBreakDef.ResetPageNumber);
					}
				}
				return this.m_resetPageNumber;
			}
		}

		public PageBreakInstance Instance
		{
			get
			{
				if (this.m_renderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_pageBreakInstance == null)
				{
					this.m_pageBreakInstance = new PageBreakInstance(this.m_reportScope, this);
				}
				return this.m_pageBreakInstance;
			}
		}

		public bool IsOldSnapshot
		{
			get
			{
				return this.m_isOldSnapshotOrStaticMember;
			}
		}

		public IReportScope ReportScope
		{
			get
			{
				return this.m_reportScope;
			}
		}

		public RenderingContext RenderingContext
		{
			get
			{
				return this.m_renderingContext;
			}
		}

		public IPageBreakOwner PageBreakOwner
		{
			get
			{
				return this.m_pageBreakOwner;
			}
		}

		public AspNetCore.ReportingServices.ReportIntermediateFormat.PageBreak PageBreakDef
		{
			get
			{
				return this.m_pageBreakDef;
			}
		}

		public bool HasEnabledInstance
		{
			get
			{
				PageBreakInstance instance = this.Instance;
				if (instance != null)
				{
					return !instance.Disabled;
				}
				return false;
			}
		}

		public PageBreak(RenderingContext renderingContext, IReportScope reportScope, IPageBreakOwner pageBreakOwner)
		{
			this.m_renderingContext = renderingContext;
			this.m_reportScope = reportScope;
			this.m_pageBreakOwner = pageBreakOwner;
			this.m_pageBreakDef = this.m_pageBreakOwner.PageBreak;
			if (this.m_pageBreakDef == null)
			{
				this.m_pageBreakDef = new AspNetCore.ReportingServices.ReportIntermediateFormat.PageBreak();
			}
			this.m_isOldSnapshotOrStaticMember = false;
		}

		public PageBreak(RenderingContext renderingContext, IReportScope reportScope, PageBreakLocation pageBreaklocation)
		{
			this.m_renderingContext = renderingContext;
			this.m_reportScope = reportScope;
			this.m_pageBreaklocation = pageBreaklocation;
			this.m_isOldSnapshotOrStaticMember = true;
		}

		public void SetNewContext()
		{
			if (this.m_pageBreakInstance != null)
			{
				this.m_pageBreakInstance.SetNewContext();
			}
		}
	}
}
