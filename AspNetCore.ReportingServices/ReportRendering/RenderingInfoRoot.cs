using AspNetCore.ReportingServices.ReportProcessing;
using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportRendering
{
	[Serializable]
	public sealed class RenderingInfoRoot
	{
		private Hashtable m_renderingInfo;

		private Hashtable m_sharedRenderingInfo;

		private Hashtable m_pageSectionRenderingInfo;

		private PaginationInfo m_paginationInfo;

		public Hashtable RenderingInfo
		{
			get
			{
				return this.m_renderingInfo;
			}
		}

		public Hashtable SharedRenderingInfo
		{
			get
			{
				return this.m_sharedRenderingInfo;
			}
		}

		public Hashtable PageSectionRenderingInfo
		{
			get
			{
				return this.m_pageSectionRenderingInfo;
			}
		}

		public PaginationInfo PaginationInfo
		{
			get
			{
				if (this.m_paginationInfo == null)
				{
					this.m_paginationInfo = new PaginationInfo();
				}
				return this.m_paginationInfo;
			}
			set
			{
				this.m_paginationInfo = value;
			}
		}

		public RenderingInfoRoot()
		{
			this.m_renderingInfo = new Hashtable();
			this.m_sharedRenderingInfo = new Hashtable();
			this.m_pageSectionRenderingInfo = new Hashtable();
			this.m_paginationInfo = null;
		}
	}
}
