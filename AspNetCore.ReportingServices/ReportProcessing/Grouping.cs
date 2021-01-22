using AspNetCore.ReportingServices.ReportProcessing.ExprHostObjectModel;
using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using AspNetCore.ReportingServices.ReportRendering;
using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class Grouping : IAggregateHolder, ISortFilterScope
	{
		private string m_name;

		private ExpressionInfoList m_groupExpressions;

		private ExpressionInfo m_groupLabel;

		private BoolList m_sortDirections;

		private bool m_pageBreakAtEnd;

		private bool m_pageBreakAtStart;

		private string m_custom;

		private DataAggregateInfoList m_aggregates;

		private bool m_groupAndSort;

		private FilterList m_filters;

		[Reference]
		private ReportItemList m_reportItemsWithHideDuplicates;

		private ExpressionInfoList m_parent;

		private DataAggregateInfoList m_recursiveAggregates;

		private DataAggregateInfoList m_postSortAggregates;

		private string m_dataElementName;

		private string m_dataCollectionName;

		private DataElementOutputTypes m_dataElementOutput;

		private DataValueList m_customProperties;

		private bool m_saveGroupExprValues;

		private ExpressionInfoList m_userSortExpressions;

		private InScopeSortFilterHashtable m_nonDetailSortFiltersInScope;

		private InScopeSortFilterHashtable m_detailSortFiltersInScope;

		[NonSerialized]
		private IntList m_hideDuplicatesReportItemIDs;

		[NonSerialized]
		private GroupingExprHost m_exprHost;

		[NonSerialized]
		private Hashtable m_scopeNames;

		[NonSerialized]
		private bool m_inPivotCell;

		[NonSerialized]
		private int m_recursiveLevel;

		[NonSerialized]
		private int[] m_groupExpressionFieldIndices;

		[NonSerialized]
		private bool m_hasInnerFilters;

		[NonSerialized]
		private VariantList m_currentGroupExprValues;

		[NonSerialized]
		private ReportHierarchyNode m_owner;

		[NonSerialized]
		private VariantList[] m_sortFilterScopeInfo;

		[NonSerialized]
		private int[] m_sortFilterScopeIndex;

		[NonSerialized]
		private bool[] m_needScopeInfoForSortFilterExpression;

		[NonSerialized]
		private bool[] m_sortFilterScopeMatched;

		[NonSerialized]
		private bool[] m_isSortFilterTarget;

		[NonSerialized]
		private bool[] m_isSortFilterExpressionScope;

		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		public ExpressionInfo GroupLabel
		{
			get
			{
				return this.m_groupLabel;
			}
			set
			{
				this.m_groupLabel = value;
			}
		}

		public BoolList SortDirections
		{
			get
			{
				return this.m_sortDirections;
			}
			set
			{
				this.m_sortDirections = value;
			}
		}

		public ExpressionInfoList GroupExpressions
		{
			get
			{
				return this.m_groupExpressions;
			}
			set
			{
				this.m_groupExpressions = value;
			}
		}

		public bool PageBreakAtEnd
		{
			get
			{
				return this.m_pageBreakAtEnd;
			}
			set
			{
				this.m_pageBreakAtEnd = value;
			}
		}

		public bool PageBreakAtStart
		{
			get
			{
				return this.m_pageBreakAtStart;
			}
			set
			{
				this.m_pageBreakAtStart = value;
			}
		}

		public string Custom
		{
			get
			{
				return this.m_custom;
			}
			set
			{
				this.m_custom = value;
			}
		}

		public DataAggregateInfoList Aggregates
		{
			get
			{
				return this.m_aggregates;
			}
			set
			{
				this.m_aggregates = value;
			}
		}

		public bool GroupAndSort
		{
			get
			{
				return this.m_groupAndSort;
			}
			set
			{
				this.m_groupAndSort = value;
			}
		}

		public FilterList Filters
		{
			get
			{
				return this.m_filters;
			}
			set
			{
				this.m_filters = value;
			}
		}

		public bool SimpleGroupExpressions
		{
			get
			{
				if (this.m_groupExpressions != null)
				{
					for (int i = 0; i < this.m_groupExpressions.Count; i++)
					{
						Global.Tracer.Assert(null != this.m_groupExpressions[i]);
						if (ExpressionInfo.Types.Field != this.m_groupExpressions[i].Type)
						{
							return false;
						}
					}
				}
				return true;
			}
		}

		public ReportItemList ReportItemsWithHideDuplicates
		{
			get
			{
				return this.m_reportItemsWithHideDuplicates;
			}
			set
			{
				this.m_reportItemsWithHideDuplicates = value;
			}
		}

		public ExpressionInfoList Parent
		{
			get
			{
				return this.m_parent;
			}
			set
			{
				this.m_parent = value;
			}
		}

		public IndexedExprHost ParentExprHost
		{
			get
			{
				if (this.m_exprHost == null)
				{
					return null;
				}
				return this.m_exprHost.ParentExpressionsHost;
			}
		}

		public DataAggregateInfoList RecursiveAggregates
		{
			get
			{
				return this.m_recursiveAggregates;
			}
			set
			{
				this.m_recursiveAggregates = value;
			}
		}

		public DataAggregateInfoList PostSortAggregates
		{
			get
			{
				return this.m_postSortAggregates;
			}
			set
			{
				this.m_postSortAggregates = value;
			}
		}

		public string DataElementName
		{
			get
			{
				return this.m_dataElementName;
			}
			set
			{
				this.m_dataElementName = value;
			}
		}

		public string DataCollectionName
		{
			get
			{
				return this.m_dataCollectionName;
			}
			set
			{
				this.m_dataCollectionName = value;
			}
		}

		public DataElementOutputTypes DataElementOutput
		{
			get
			{
				return this.m_dataElementOutput;
			}
			set
			{
				this.m_dataElementOutput = value;
			}
		}

		public DataValueList CustomProperties
		{
			get
			{
				return this.m_customProperties;
			}
			set
			{
				this.m_customProperties = value;
			}
		}

		public bool SaveGroupExprValues
		{
			get
			{
				return this.m_saveGroupExprValues;
			}
			set
			{
				this.m_saveGroupExprValues = value;
			}
		}

		public ExpressionInfoList UserSortExpressions
		{
			get
			{
				return this.m_userSortExpressions;
			}
			set
			{
				this.m_userSortExpressions = value;
			}
		}

		public InScopeSortFilterHashtable NonDetailSortFiltersInScope
		{
			get
			{
				return this.m_nonDetailSortFiltersInScope;
			}
			set
			{
				this.m_nonDetailSortFiltersInScope = value;
			}
		}

		public InScopeSortFilterHashtable DetailSortFiltersInScope
		{
			get
			{
				return this.m_detailSortFiltersInScope;
			}
			set
			{
				this.m_detailSortFiltersInScope = value;
			}
		}

		public IntList HideDuplicatesReportItemIDs
		{
			get
			{
				return this.m_hideDuplicatesReportItemIDs;
			}
			set
			{
				this.m_hideDuplicatesReportItemIDs = value;
			}
		}

		public GroupingExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		public Hashtable ScopeNames
		{
			get
			{
				return this.m_scopeNames;
			}
			set
			{
				this.m_scopeNames = value;
			}
		}

		public bool InPivotCell
		{
			get
			{
				return this.m_inPivotCell;
			}
			set
			{
				this.m_inPivotCell = value;
			}
		}

		public int RecursiveLevel
		{
			get
			{
				return this.m_recursiveLevel;
			}
			set
			{
				this.m_recursiveLevel = value;
			}
		}

		public bool HasInnerFilters
		{
			get
			{
				return this.m_hasInnerFilters;
			}
			set
			{
				this.m_hasInnerFilters = value;
			}
		}

		public VariantList CurrentGroupExpressionValues
		{
			get
			{
				return this.m_currentGroupExprValues;
			}
			set
			{
				this.m_currentGroupExprValues = value;
			}
		}

		public ReportHierarchyNode Owner
		{
			get
			{
				return this.m_owner;
			}
			set
			{
				this.m_owner = value;
			}
		}

		public VariantList[] SortFilterScopeInfo
		{
			get
			{
				return this.m_sortFilterScopeInfo;
			}
			set
			{
				this.m_sortFilterScopeInfo = value;
			}
		}

		public int[] SortFilterScopeIndex
		{
			get
			{
				return this.m_sortFilterScopeIndex;
			}
			set
			{
				this.m_sortFilterScopeIndex = value;
			}
		}

		public bool[] NeedScopeInfoForSortFilterExpression
		{
			get
			{
				return this.m_needScopeInfoForSortFilterExpression;
			}
			set
			{
				this.m_needScopeInfoForSortFilterExpression = value;
			}
		}

		public bool[] IsSortFilterTarget
		{
			get
			{
				return this.m_isSortFilterTarget;
			}
			set
			{
				this.m_isSortFilterTarget = value;
			}
		}

		public bool[] IsSortFilterExpressionScope
		{
			get
			{
				return this.m_isSortFilterExpressionScope;
			}
			set
			{
				this.m_isSortFilterExpressionScope = value;
			}
		}

		public bool[] SortFilterScopeMatched
		{
			get
			{
				return this.m_sortFilterScopeMatched;
			}
			set
			{
				this.m_sortFilterScopeMatched = value;
			}
		}

		int ISortFilterScope.ID
		{
			get
			{
				Global.Tracer.Assert(null != this.m_owner);
				return this.m_owner.ID;
			}
		}

		string ISortFilterScope.ScopeName
		{
			get
			{
				return this.m_name;
			}
		}

		bool[] ISortFilterScope.IsSortFilterTarget
		{
			get
			{
				return this.m_isSortFilterTarget;
			}
			set
			{
				this.m_isSortFilterTarget = value;
			}
		}

		bool[] ISortFilterScope.IsSortFilterExpressionScope
		{
			get
			{
				return this.m_isSortFilterExpressionScope;
			}
			set
			{
				this.m_isSortFilterExpressionScope = value;
			}
		}

		ExpressionInfoList ISortFilterScope.UserSortExpressions
		{
			get
			{
				return this.m_userSortExpressions;
			}
			set
			{
				this.m_userSortExpressions = value;
			}
		}

		IndexedExprHost ISortFilterScope.UserSortExpressionsHost
		{
			get
			{
				if (this.m_exprHost == null)
				{
					return null;
				}
				return this.m_exprHost.UserSortExpressionsHost;
			}
		}

		public Grouping(ConstructionPhase phase)
		{
			if (phase == ConstructionPhase.Publishing)
			{
				this.m_groupExpressions = new ExpressionInfoList();
				this.m_aggregates = new DataAggregateInfoList();
				this.m_postSortAggregates = new DataAggregateInfoList();
				this.m_recursiveAggregates = new DataAggregateInfoList();
			}
		}

		public void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.GroupingStart(this.m_name);
			this.DataRendererInitialize(context);
			if (this.m_groupExpressions != null)
			{
				for (int i = 0; i < this.m_groupExpressions.Count; i++)
				{
					ExpressionInfo expressionInfo = this.m_groupExpressions[i];
					expressionInfo.GroupExpressionInitialize(context);
					context.ExprHostBuilder.GroupingExpression(expressionInfo);
				}
			}
			if (this.m_groupLabel != null)
			{
				this.m_groupLabel.Initialize("Label", context);
				context.ExprHostBuilder.GenericLabel(this.m_groupLabel);
			}
			if (this.m_filters != null)
			{
				for (int j = 0; j < this.m_filters.Count; j++)
				{
					this.m_filters[j].Initialize(context);
				}
			}
			if (this.m_parent != null)
			{
				context.ExprHostBuilder.GroupingParentExpressionsStart();
				for (int k = 0; k < this.m_parent.Count; k++)
				{
					ExpressionInfo expressionInfo2 = this.m_parent[k];
					expressionInfo2.GroupExpressionInitialize(context);
					context.ExprHostBuilder.GroupingParentExpression(expressionInfo2);
				}
				context.ExprHostBuilder.GroupingParentExpressionsEnd();
			}
			if (this.m_customProperties != null)
			{
				this.m_customProperties.Initialize(null, true, context);
			}
			if (this.m_userSortExpressions != null)
			{
				context.ExprHostBuilder.UserSortExpressionsStart();
				for (int l = 0; l < this.m_userSortExpressions.Count; l++)
				{
					ExpressionInfo expression = this.m_userSortExpressions[l];
					context.ExprHostBuilder.UserSortExpression(expression);
				}
				context.ExprHostBuilder.UserSortExpressionsEnd();
			}
			context.ExprHostBuilder.GroupingEnd();
		}

		DataAggregateInfoList[] IAggregateHolder.GetAggregateLists()
		{
			return new DataAggregateInfoList[1]
			{
				this.m_aggregates
			};
		}

		DataAggregateInfoList[] IAggregateHolder.GetPostSortAggregateLists()
		{
			return new DataAggregateInfoList[1]
			{
				this.m_postSortAggregates
			};
		}

		void IAggregateHolder.ClearIfEmpty()
		{
			Global.Tracer.Assert(null != this.m_aggregates);
			if (this.m_aggregates.Count == 0)
			{
				this.m_aggregates = null;
			}
			Global.Tracer.Assert(null != this.m_postSortAggregates);
			if (this.m_postSortAggregates.Count == 0)
			{
				this.m_postSortAggregates = null;
			}
			Global.Tracer.Assert(null != this.m_recursiveAggregates);
			if (this.m_recursiveAggregates.Count == 0)
			{
				this.m_recursiveAggregates = null;
			}
		}

		private void DataRendererInitialize(InitializationContext context)
		{
			CLSNameValidator.ValidateDataElementName(ref this.m_dataElementName, this.m_name, context.ObjectType, context.ObjectName, "DataElementName", context.ErrorContext);
			CLSNameValidator.ValidateDataElementName(ref this.m_dataCollectionName, this.m_dataElementName + "_Collection", context.ObjectType, context.ObjectName, "DataCollectionName", context.ErrorContext);
		}

		public void AddReportItemWithHideDuplicates(ReportItem reportItem)
		{
			if (this.m_reportItemsWithHideDuplicates == null)
			{
				this.m_reportItemsWithHideDuplicates = new ReportItemList();
			}
			this.m_reportItemsWithHideDuplicates.Add(reportItem);
		}

		public void SetExprHost(GroupingExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			if (this.m_exprHost.FilterHostsRemotable != null)
			{
				Global.Tracer.Assert(this.m_filters != null);
				int count = this.m_filters.Count;
				for (int i = 0; i < count; i++)
				{
					this.m_filters[i].SetExprHost(this.m_exprHost.FilterHostsRemotable, reportObjectModel);
				}
			}
			if (this.m_exprHost.ParentExpressionsHost != null)
			{
				this.m_exprHost.ParentExpressionsHost.SetReportObjectModel(reportObjectModel);
			}
			if (this.m_exprHost.CustomPropertyHostsRemotable != null)
			{
				Global.Tracer.Assert(null != this.m_customProperties);
				this.m_customProperties.SetExprHost(this.m_exprHost.CustomPropertyHostsRemotable, reportObjectModel);
			}
			if (this.m_exprHost.UserSortExpressionsHost != null)
			{
				this.m_exprHost.UserSortExpressionsHost.SetReportObjectModel(reportObjectModel);
			}
		}

		public bool IsOnPathToSortFilterSource(int index)
		{
			if (this.m_sortFilterScopeInfo != null && this.m_sortFilterScopeIndex != null && -1 != this.m_sortFilterScopeIndex[index])
			{
				return true;
			}
			return false;
		}

		public int[] GetGroupExpressionFieldIndices()
		{
			if (this.m_groupExpressionFieldIndices == null)
			{
				Global.Tracer.Assert(this.m_groupExpressions != null && 0 < this.m_groupExpressions.Count);
				this.m_groupExpressionFieldIndices = new int[this.m_groupExpressions.Count];
				for (int i = 0; i < this.m_groupExpressions.Count; i++)
				{
					this.m_groupExpressionFieldIndices[i] = -2;
					ExpressionInfo expressionInfo = this.m_groupExpressions[i];
					if (expressionInfo.Type == ExpressionInfo.Types.Field)
					{
						this.m_groupExpressionFieldIndices[i] = expressionInfo.IntValue;
					}
					else if (expressionInfo.Type == ExpressionInfo.Types.Constant)
					{
						this.m_groupExpressionFieldIndices[i] = -1;
					}
				}
			}
			return this.m_groupExpressionFieldIndices;
		}

		public static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.Name, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.GroupExpressions, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ExpressionInfoList));
			memberInfoList.Add(new MemberInfo(MemberName.GroupLabel, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ExpressionInfo));
			memberInfoList.Add(new MemberInfo(MemberName.SortDirections, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.BoolList));
			memberInfoList.Add(new MemberInfo(MemberName.PageBreakAtEnd, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.PageBreakAtStart, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.Custom, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.Aggregates, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.DataAggregateInfoList));
			memberInfoList.Add(new MemberInfo(MemberName.GroupAndSort, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.Filters, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.FilterList));
			memberInfoList.Add(new MemberInfo(MemberName.ReportItemsWithHideDuplicates, Token.Reference, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItemList));
			memberInfoList.Add(new MemberInfo(MemberName.Parent, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ExpressionInfoList));
			memberInfoList.Add(new MemberInfo(MemberName.RecursiveAggregates, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.DataAggregateInfoList));
			memberInfoList.Add(new MemberInfo(MemberName.PostSortAggregates, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.DataAggregateInfoList));
			memberInfoList.Add(new MemberInfo(MemberName.DataElementName, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.DataCollectionName, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.DataElementOutput, Token.Enum));
			memberInfoList.Add(new MemberInfo(MemberName.CustomProperties, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.DataValueList));
			memberInfoList.Add(new MemberInfo(MemberName.SaveGroupExprValues, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.UserSortExpressions, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ExpressionInfoList));
			memberInfoList.Add(new MemberInfo(MemberName.NonDetailSortFiltersInScope, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.InScopeSortFilterHashtable));
			memberInfoList.Add(new MemberInfo(MemberName.DetailSortFiltersInScope, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.InScopeSortFilterHashtable));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.None, memberInfoList);
		}
	}
}
