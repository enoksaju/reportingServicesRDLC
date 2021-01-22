using AspNetCore.ReportingServices.Diagnostics;
using AspNetCore.ReportingServices.ReportProcessing.ExprHostObjectModel;
using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using AspNetCore.ReportingServices.ReportRendering;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class SubReport : ReportItem, IPageBreakItem
	{
		public enum Status
		{
			NotRetrieved,
			Retrieved,
			RetrieveFailed,
			PreFetched
		}

		public const uint MaxSubReportLevel = 20u;

		private string m_reportPath;

		private ParameterValueList m_parameters;

		private ExpressionInfo m_noRows;

		private bool m_mergeTransactions;

		[Reference]
		private GroupingList m_containingScopes;

		private bool m_isMatrixCellScope;

		private Status m_status;

		private string m_reportName;

		private string m_description;

		private Report m_report;

		private string m_stringUri;

		private ParameterInfoCollection m_parametersFromCatalog;

		private ScopeLookupTable m_dataSetUniqueNameMap;

		[NonSerialized]
		private string m_subReportScope;

		[NonSerialized]
		private bool m_isDetailScope;

		[NonSerialized]
		private PageBreakStates m_pagebreakState;

		[NonSerialized]
		private SubreportExprHost m_exprHost;

		[NonSerialized]
		private SubReportList m_detailScopeSubReports;

		[NonSerialized]
		private bool m_saveDataSetUniqueName;

		[NonSerialized]
		private Uri m_uri;

		[NonSerialized]
		private ICatalogItemContext m_reportContext;

		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.Subreport;
			}
		}

		public string ReportPath
		{
			get
			{
				return this.m_reportPath;
			}
			set
			{
				this.m_reportPath = value;
			}
		}

		public ParameterValueList Parameters
		{
			get
			{
				return this.m_parameters;
			}
			set
			{
				this.m_parameters = value;
			}
		}

		public ExpressionInfo NoRows
		{
			get
			{
				return this.m_noRows;
			}
			set
			{
				this.m_noRows = value;
			}
		}

		public bool MergeTransactions
		{
			get
			{
				return this.m_mergeTransactions;
			}
			set
			{
				this.m_mergeTransactions = value;
			}
		}

		public GroupingList ContainingScopes
		{
			get
			{
				return this.m_containingScopes;
			}
			set
			{
				this.m_containingScopes = value;
			}
		}

		public Status RetrievalStatus
		{
			get
			{
				return this.m_status;
			}
			set
			{
				this.m_status = value;
			}
		}

		public string ReportName
		{
			get
			{
				return this.m_reportName;
			}
			set
			{
				this.m_reportName = value;
			}
		}

		public string Description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value;
			}
		}

		public Report Report
		{
			get
			{
				return this.m_report;
			}
			set
			{
				this.m_report = value;
			}
		}

		public string StringUri
		{
			get
			{
				return this.m_stringUri;
			}
			set
			{
				this.m_stringUri = value;
			}
		}

		public ICatalogItemContext ReportContext
		{
			get
			{
				return this.m_reportContext;
			}
			set
			{
				this.m_reportContext = value;
			}
		}

		public ParameterInfoCollection ParametersFromCatalog
		{
			get
			{
				return this.m_parametersFromCatalog;
			}
			set
			{
				this.m_parametersFromCatalog = value;
			}
		}

		public Uri Uri
		{
			get
			{
				if ((Uri)null == this.m_uri)
				{
					this.m_uri = new Uri(this.m_stringUri);
				}
				return this.m_uri;
			}
		}

		public SubreportExprHost SubReportExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		public string SubReportScope
		{
			get
			{
				return this.m_subReportScope;
			}
			set
			{
				this.m_subReportScope = value;
			}
		}

		public bool IsMatrixCellScope
		{
			get
			{
				return this.m_isMatrixCellScope;
			}
			set
			{
				this.m_isMatrixCellScope = value;
			}
		}

		public bool IsDetailScope
		{
			get
			{
				return this.m_isDetailScope;
			}
			set
			{
				this.m_isDetailScope = value;
			}
		}

		public SubReportList DetailScopeSubReports
		{
			get
			{
				return this.m_detailScopeSubReports;
			}
			set
			{
				this.m_detailScopeSubReports = value;
			}
		}

		public ScopeLookupTable DataSetUniqueNameMap
		{
			get
			{
				return this.m_dataSetUniqueNameMap;
			}
			set
			{
				this.m_dataSetUniqueNameMap = value;
			}
		}

		public bool SaveDataSetUniqueName
		{
			get
			{
				return this.m_saveDataSetUniqueName;
			}
		}

		public SubReport(ReportItem parent)
			: base(parent)
		{
		}

		public SubReport(int id, ReportItem parent)
			: base(id, parent)
		{
			this.m_parameters = new ParameterValueList();
		}

		public override bool Initialize(InitializationContext context)
		{
			context.ObjectType = this.ObjectType;
			context.ObjectName = base.m_name;
			this.m_subReportScope = context.GetCurrentScope();
			if ((LocationFlags)0 < (context.Location & LocationFlags.InMatrixCellTopLevelItem))
			{
				this.m_isMatrixCellScope = true;
			}
			if ((LocationFlags)0 < (context.Location & LocationFlags.InDetail))
			{
				this.m_isDetailScope = true;
				context.SetDataSetDetailUserSortFilter();
			}
			context.ExprHostBuilder.SubreportStart(base.m_name);
			base.Initialize(context);
			if (base.m_visibility != null)
			{
				base.m_visibility.Initialize(context, false, false);
			}
			if (this.m_parameters != null)
			{
				for (int i = 0; i < this.m_parameters.Count; i++)
				{
					ParameterValue parameterValue = this.m_parameters[i];
					context.ExprHostBuilder.SubreportParameterStart();
					parameterValue.Initialize(context, false);
					parameterValue.ExprHostID = context.ExprHostBuilder.SubreportParameterEnd();
				}
			}
			if (this.m_noRows != null)
			{
				this.m_noRows.Initialize("NoRows", context);
				context.ExprHostBuilder.GenericNoRows(this.m_noRows);
			}
			base.ExprHostID = context.ExprHostBuilder.SubreportEnd();
			return false;
		}

		public override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null);
				this.m_exprHost = reportExprHost.SubreportHostsRemotable[base.ExprHostID];
				base.ReportItemSetExprHost(this.m_exprHost, reportObjectModel);
				if (this.m_exprHost.ParameterHostsRemotable != null)
				{
					Global.Tracer.Assert(this.m_parameters != null);
					for (int num = this.m_parameters.Count - 1; num >= 0; num--)
					{
						this.m_parameters[num].SetExprHost(this.m_exprHost.ParameterHostsRemotable, reportObjectModel);
					}
				}
			}
		}

		bool IPageBreakItem.IgnorePageBreaks()
		{
			if (this.m_pagebreakState == PageBreakStates.Unknown)
			{
				if (SharedHiddenState.Never != Visibility.GetSharedHidden(base.m_visibility))
				{
					this.m_pagebreakState = PageBreakStates.CanIgnore;
				}
				else
				{
					this.m_pagebreakState = PageBreakStates.CannotIgnore;
				}
			}
			if (PageBreakStates.CanIgnore == this.m_pagebreakState)
			{
				return true;
			}
			return false;
		}

		bool IPageBreakItem.HasPageBreaks(bool atStart)
		{
			return false;
		}

		public void UpdateSubReportScopes(UserSortFilterContext context)
		{
			if (this.m_containingScopes != null && 0 < this.m_containingScopes.Count && this.m_containingScopes.LastEntry == null)
			{
				if (context.DetailScopeSubReports != null)
				{
					this.m_detailScopeSubReports = context.DetailScopeSubReports.Clone();
				}
				else
				{
					this.m_detailScopeSubReports = new SubReportList();
				}
				this.m_detailScopeSubReports.Add(this);
			}
			else
			{
				this.m_detailScopeSubReports = context.DetailScopeSubReports;
			}
			if (context.ContainingScopes != null)
			{
				if (this.m_containingScopes != null && 0 < this.m_containingScopes.Count)
				{
					this.m_containingScopes.InsertRange(0, context.ContainingScopes);
				}
				else
				{
					this.m_containingScopes = context.ContainingScopes;
				}
			}
		}

		public void AddDataSetUniqueName(VariantList[] scopeValues, int subReportUniqueName)
		{
			if (this.m_dataSetUniqueNameMap == null)
			{
				this.m_dataSetUniqueNameMap = new ScopeLookupTable();
				this.m_saveDataSetUniqueName = true;
			}
			this.m_dataSetUniqueNameMap.Add(this.m_containingScopes, scopeValues, subReportUniqueName);
		}

		public int GetDataSetUniqueName(VariantList[] scopeValues)
		{
			Global.Tracer.Assert(null != this.m_dataSetUniqueNameMap);
			return this.m_dataSetUniqueNameMap.Lookup(this.m_containingScopes, scopeValues);
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.ReportPath, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.Parameters, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ParameterValueList));
			memberInfoList.Add(new MemberInfo(MemberName.NoRows, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ExpressionInfo));
			memberInfoList.Add(new MemberInfo(MemberName.MergeTransactions, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.ContainingScopes, Token.Reference, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.GroupingList));
			memberInfoList.Add(new MemberInfo(MemberName.IsMatrixCellScope, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.DataSetUniqueNameMap, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ScopeLookupTable));
			memberInfoList.Add(new MemberInfo(MemberName.Status, Token.Enum));
			memberInfoList.Add(new MemberInfo(MemberName.ReportName, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.Description, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.Report, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.Report));
			memberInfoList.Add(new MemberInfo(MemberName.StringUri, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.ParametersFromCatalog, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ParameterInfoCollection));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItem, memberInfoList);
		}
	}
}
