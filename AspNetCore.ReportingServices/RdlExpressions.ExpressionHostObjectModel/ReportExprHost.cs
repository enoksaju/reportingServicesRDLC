using AspNetCore.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using System;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	public abstract class ReportExprHost : ReportItemExprHost
	{
		protected CustomCodeProxyBase m_codeProxyBase;

		public IndexedExprHost VariableValueHosts;

		[CLSCompliant(false)]
		protected IList<AggregateParamExprHost> m_aggregateParamHostsRemotable;

		[CLSCompliant(false)]
		protected IList<LookupExprHost> m_lookupExprHostsRemotable;

		[CLSCompliant(false)]
		protected IList<LookupDestExprHost> m_lookupDestExprHostsRemotable;

		[CLSCompliant(false)]
		protected IList<ReportParamExprHost> m_reportParameterHostsRemotable;

		[CLSCompliant(false)]
		protected IList<DataSourceExprHost> m_dataSourceHostsRemotable;

		[CLSCompliant(false)]
		protected IList<DataSetExprHost> m_dataSetHostsRemotable;

		[CLSCompliant(false)]
		protected IList<StyleExprHost> m_pageSectionHostsRemotable;

		protected StyleExprHost m_pageHost;

		[CLSCompliant(false)]
		protected IList<StyleExprHost> m_pageHostsRemotable;

		[CLSCompliant(false)]
		protected IList<ReportSectionExprHost> m_reportSectionHostsRemotable;

		[CLSCompliant(false)]
		protected IList<ReportItemExprHost> m_lineHostsRemotable;

		[CLSCompliant(false)]
		protected IList<ReportItemExprHost> m_rectangleHostsRemotable;

		[CLSCompliant(false)]
		protected IList<TextBoxExprHost> m_textBoxHostsRemotable;

		[CLSCompliant(false)]
		protected IList<ImageExprHost> m_imageHostsRemotable;

		[CLSCompliant(false)]
		protected IList<SubreportExprHost> m_subreportHostsRemotable;

		[CLSCompliant(false)]
		protected IList<TablixExprHost> m_tablixHostsRemotable;

		[CLSCompliant(false)]
		protected IList<ChartExprHost> m_chartHostsRemotable;

		[CLSCompliant(false)]
		protected IList<GaugePanelExprHost> m_gaugePanelHostsRemotable;

		[CLSCompliant(false)]
		protected IList<MapExprHost> m_mapHostsRemotable;

		[CLSCompliant(false)]
		protected IList<MapDataRegionExprHost> m_mapDataRegionHostsRemotable;

		[CLSCompliant(false)]
		protected IList<CustomReportItemExprHost> m_customReportItemHostsRemotable;

		public virtual object ReportLanguageExpr
		{
			get
			{
				return null;
			}
		}

		public virtual object AutoRefreshExpr
		{
			get
			{
				return null;
			}
		}

		public virtual object InitialPageNameExpr
		{
			get
			{
				return null;
			}
		}

		public IList<AggregateParamExprHost> AggregateParamHostsRemotable
		{
			get
			{
				return this.m_aggregateParamHostsRemotable;
			}
		}

		[CLSCompliant(false)]
		public IList<LookupExprHost> LookupExprHostsRemotable
		{
			get
			{
				return this.m_lookupExprHostsRemotable;
			}
		}

		[CLSCompliant(false)]
		public IList<LookupDestExprHost> LookupDestExprHostsRemotable
		{
			get
			{
				return this.m_lookupDestExprHostsRemotable;
			}
		}

		public IList<ReportParamExprHost> ReportParameterHostsRemotable
		{
			get
			{
				return this.m_reportParameterHostsRemotable;
			}
		}

		public IList<DataSourceExprHost> DataSourceHostsRemotable
		{
			get
			{
				return this.m_dataSourceHostsRemotable;
			}
		}

		public IList<DataSetExprHost> DataSetHostsRemotable
		{
			get
			{
				return this.m_dataSetHostsRemotable;
			}
		}

		public IList<StyleExprHost> PageSectionHostsRemotable
		{
			get
			{
				return this.m_pageSectionHostsRemotable;
			}
		}

		public virtual StyleExprHost PageHost
		{
			get
			{
				return this.m_pageHost;
			}
		}

		[CLSCompliant(false)]
		public IList<StyleExprHost> PageHostsRemotable
		{
			get
			{
				return this.m_pageHostsRemotable;
			}
		}

		[CLSCompliant(false)]
		public IList<ReportSectionExprHost> ReportSectionHostsRemotable
		{
			get
			{
				return this.m_reportSectionHostsRemotable;
			}
		}

		public IList<ReportItemExprHost> LineHostsRemotable
		{
			get
			{
				return this.m_lineHostsRemotable;
			}
		}

		public IList<ReportItemExprHost> RectangleHostsRemotable
		{
			get
			{
				return this.m_rectangleHostsRemotable;
			}
		}

		public IList<TextBoxExprHost> TextBoxHostsRemotable
		{
			get
			{
				return this.m_textBoxHostsRemotable;
			}
		}

		public IList<ImageExprHost> ImageHostsRemotable
		{
			get
			{
				return this.m_imageHostsRemotable;
			}
		}

		public IList<SubreportExprHost> SubreportHostsRemotable
		{
			get
			{
				return this.m_subreportHostsRemotable;
			}
		}

		public IList<TablixExprHost> TablixHostsRemotable
		{
			get
			{
				return this.m_tablixHostsRemotable;
			}
		}

		public IList<ChartExprHost> ChartHostsRemotable
		{
			get
			{
				return this.m_chartHostsRemotable;
			}
		}

		public IList<GaugePanelExprHost> GaugePanelHostsRemotable
		{
			get
			{
				return this.m_gaugePanelHostsRemotable;
			}
		}

		public IList<MapExprHost> MapHostsRemotable
		{
			get
			{
				return this.m_mapHostsRemotable;
			}
		}

		public IList<MapDataRegionExprHost> MapDataRegionHostsRemotable
		{
			get
			{
				return this.m_mapDataRegionHostsRemotable;
			}
		}

		public IList<CustomReportItemExprHost> CustomReportItemHostsRemotable
		{
			get
			{
				return this.m_customReportItemHostsRemotable;
			}
		}

		protected ReportExprHost(object reportObjectModel)
		{
			base.SetReportObjectModel((OnDemandObjectModel)reportObjectModel);
		}

		public void CustomCodeOnInit()
		{
			if (this.m_codeProxyBase != null)
			{
				this.m_codeProxyBase.CallOnInit();
			}
		}
	}
}
