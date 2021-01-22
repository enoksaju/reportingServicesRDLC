using AspNetCore.ReportingServices.ReportProcessing.ExprHostObjectModel;
using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using AspNetCore.ReportingServices.ReportRendering;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class List : DataRegion
	{
		private ReportHierarchyNode m_hierarchyDef;

		private ReportItemCollection m_reportItems;

		private bool m_fillPage;

		private string m_dataInstanceName;

		private DataElementOutputTypes m_dataInstanceElementOutput;

		private bool m_isListMostInner;

		[NonSerialized]
		private ListExprHost m_exprHost;

		[NonSerialized]
		private int m_ContentStartPage = -1;

		[NonSerialized]
		private int m_keepWithChildFirstPage = -1;

		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.List;
			}
		}

		public Grouping Grouping
		{
			get
			{
				return this.m_hierarchyDef.Grouping;
			}
			set
			{
				this.m_hierarchyDef.Grouping = value;
			}
		}

		public Sorting Sorting
		{
			get
			{
				return this.m_hierarchyDef.Sorting;
			}
			set
			{
				this.m_hierarchyDef.Sorting = value;
			}
		}

		public ReportHierarchyNode HierarchyDef
		{
			get
			{
				return this.m_hierarchyDef;
			}
			set
			{
				this.m_hierarchyDef = value;
			}
		}

		public ReportItemCollection ReportItems
		{
			get
			{
				return this.m_reportItems;
			}
			set
			{
				this.m_reportItems = value;
			}
		}

		public bool FillPage
		{
			get
			{
				return this.m_fillPage;
			}
			set
			{
				this.m_fillPage = value;
			}
		}

		public int ListContentID
		{
			get
			{
				return this.m_hierarchyDef.ID;
			}
		}

		public string DataInstanceName
		{
			get
			{
				return this.m_dataInstanceName;
			}
			set
			{
				this.m_dataInstanceName = value;
			}
		}

		public DataElementOutputTypes DataInstanceElementOutput
		{
			get
			{
				return this.m_dataInstanceElementOutput;
			}
			set
			{
				this.m_dataInstanceElementOutput = value;
			}
		}

		public bool IsListMostInner
		{
			get
			{
				return this.m_isListMostInner;
			}
			set
			{
				this.m_isListMostInner = value;
			}
		}

		public bool PropagatedPageBreakAtStart
		{
			get
			{
				if (this.Grouping == null)
				{
					return false;
				}
				return this.Grouping.PageBreakAtStart;
			}
		}

		public bool PropagatedPageBreakAtEnd
		{
			get
			{
				if (this.Grouping == null)
				{
					return false;
				}
				return this.Grouping.PageBreakAtEnd;
			}
		}

		public ListExprHost ListExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		public int ContentStartPage
		{
			get
			{
				return this.m_ContentStartPage;
			}
			set
			{
				this.m_ContentStartPage = value;
			}
		}

		public int KeepWithChildFirstPage
		{
			get
			{
				return this.m_keepWithChildFirstPage;
			}
			set
			{
				this.m_keepWithChildFirstPage = value;
			}
		}

		protected override DataRegionExprHost DataRegionExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		public List(ReportItem parent)
			: base(parent)
		{
		}

		public List(int id, int idForListContent, int idForReportItems, ReportItem parent)
			: base(id, parent)
		{
			this.m_hierarchyDef = new ReportHierarchyNode(idForListContent, this);
			this.m_reportItems = new ReportItemCollection(idForReportItems, true);
		}

		public override void CalculateSizes(double width, double height, InitializationContext context, bool overwrite)
		{
			base.CalculateSizes(width, height, context, overwrite);
			this.m_reportItems.CalculateSizes(context, false);
		}

		public override bool Initialize(InitializationContext context)
		{
			context.ObjectType = this.ObjectType;
			context.ObjectName = base.m_name;
			context.RegisterDataRegion(this);
			this.InternalInitialize(context);
			context.UnRegisterDataRegion(this);
			return false;
		}

		private void InternalInitialize(InitializationContext context)
		{
			context.Location = (context.Location | LocationFlags.InDataSet | LocationFlags.InDataRegion);
			context.ExprHostBuilder.ListStart(base.m_name);
			base.Initialize(context);
			context.Location &= ~LocationFlags.InMatrixCellTopLevelItem;
			if (this.Grouping != null)
			{
				context.Location |= LocationFlags.InGrouping;
			}
			else
			{
				context.Location |= LocationFlags.InDetail;
				context.DetailObjectType = ObjectType.List;
			}
			if (this.Grouping != null)
			{
				context.RegisterGroupingScope(this.Grouping.Name, this.Grouping.SimpleGroupExpressions, this.Grouping.Aggregates, this.Grouping.PostSortAggregates, this.Grouping.RecursiveAggregates, this.Grouping);
			}
			Global.Tracer.Assert(null != this.m_hierarchyDef);
			this.m_hierarchyDef.Initialize(context);
			context.RegisterRunningValues(this.m_reportItems.RunningValues);
			context.RegisterReportItems(this.m_reportItems);
			if (base.m_visibility != null)
			{
				base.m_visibility.Initialize(context, true, false);
			}
			this.m_reportItems.Initialize(context, false);
			if (base.m_visibility != null)
			{
				base.m_visibility.UnRegisterReceiver(context);
			}
			context.UnRegisterReportItems(this.m_reportItems);
			context.UnRegisterRunningValues(this.m_reportItems.RunningValues);
			if (this.Grouping != null)
			{
				context.UnRegisterGroupingScope(this.Grouping.Name);
			}
			base.ExprHostID = context.ExprHostBuilder.ListEnd();
		}

		protected override void DataRendererInitialize(InitializationContext context)
		{
			base.DataRendererInitialize(context);
			CLSNameValidator.ValidateDataElementName(ref this.m_dataInstanceName, "Item", context.ObjectType, context.ObjectName, "DataInstanceName", context.ErrorContext);
		}

		public override void RegisterReceiver(InitializationContext context)
		{
			context.RegisterReportItems(this.m_reportItems);
			if (base.m_visibility != null)
			{
				base.m_visibility.RegisterReceiver(context, true);
			}
			this.m_reportItems.RegisterReceiver(context);
			if (base.m_visibility != null)
			{
				base.m_visibility.UnRegisterReceiver(context);
			}
			context.UnRegisterReportItems(this.m_reportItems);
		}

		public override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null);
				this.m_exprHost = reportExprHost.ListHostsRemotable[base.ExprHostID];
				base.DataRegionSetExprHost(this.m_exprHost, reportObjectModel);
				if (this.m_exprHost.GroupingHost == null && this.m_exprHost.SortingHost == null)
				{
					return;
				}
				Global.Tracer.Assert(this.m_hierarchyDef != null);
				this.m_hierarchyDef.ReportHierarchyNodeSetExprHost(this.m_exprHost.GroupingHost, this.m_exprHost.SortingHost, reportObjectModel);
			}
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.HierarchyDef, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportHierarchyNode));
			memberInfoList.Add(new MemberInfo(MemberName.ReportItems, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItemCollection));
			memberInfoList.Add(new MemberInfo(MemberName.FillPage, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.DataInstanceName, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.DataInstanceElementOutput, Token.Enum));
			memberInfoList.Add(new MemberInfo(MemberName.IsListMostInner, Token.Boolean));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.DataRegion, memberInfoList);
		}
	}
}
