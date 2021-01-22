using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public abstract class PivotHeading : ReportHierarchyNode
	{
		protected Visibility m_visibility;

		protected Subtotal m_subtotal;

		protected int m_level;

		protected bool m_isColumn;

		protected bool m_hasExprHost;

		protected int m_subtotalSpan;

		private IntList m_IDs;

		[NonSerialized]
		protected int m_numberOfStatics;

		[NonSerialized]
		protected DataAggregateInfoList m_aggregates;

		[NonSerialized]
		protected DataAggregateInfoList m_postSortAggregates;

		[NonSerialized]
		protected DataAggregateInfoList m_recursiveAggregates;

		[NonSerialized]
		protected AggregatesImpl m_outermostSTCellRVCol;

		[NonSerialized]
		protected AggregatesImpl m_cellRVCol;

		[NonSerialized]
		protected AggregatesImpl[] m_outermostSTCellScopedRVCollections;

		[NonSerialized]
		protected AggregatesImpl[] m_cellScopedRVCollections;

		[NonSerialized]
		protected Hashtable[] m_cellScopeNames;

		public PivotHeading SubHeading
		{
			get
			{
				return (PivotHeading)base.m_innerHierarchy;
			}
			set
			{
				base.m_innerHierarchy = value;
			}
		}

		public Visibility Visibility
		{
			get
			{
				return this.m_visibility;
			}
			set
			{
				this.m_visibility = value;
			}
		}

		public Subtotal Subtotal
		{
			get
			{
				return this.m_subtotal;
			}
			set
			{
				this.m_subtotal = value;
			}
		}

		public int Level
		{
			get
			{
				return this.m_level;
			}
			set
			{
				this.m_level = value;
			}
		}

		public bool IsColumn
		{
			get
			{
				return this.m_isColumn;
			}
			set
			{
				this.m_isColumn = value;
			}
		}

		public bool HasExprHost
		{
			get
			{
				return this.m_hasExprHost;
			}
			set
			{
				this.m_hasExprHost = value;
			}
		}

		public int SubtotalSpan
		{
			get
			{
				return this.m_subtotalSpan;
			}
			set
			{
				this.m_subtotalSpan = value;
			}
		}

		public IntList IDs
		{
			get
			{
				return this.m_IDs;
			}
			set
			{
				this.m_IDs = value;
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

		public int NumberOfStatics
		{
			get
			{
				return this.m_numberOfStatics;
			}
			set
			{
				this.m_numberOfStatics = value;
			}
		}

		public AggregatesImpl OutermostSTCellRVCol
		{
			get
			{
				return this.m_outermostSTCellRVCol;
			}
			set
			{
				this.m_outermostSTCellRVCol = value;
			}
		}

		public AggregatesImpl CellRVCol
		{
			get
			{
				return this.m_cellRVCol;
			}
			set
			{
				this.m_cellRVCol = value;
			}
		}

		public AggregatesImpl[] OutermostSTCellScopedRVCollections
		{
			get
			{
				return this.m_outermostSTCellScopedRVCollections;
			}
			set
			{
				this.m_outermostSTCellScopedRVCollections = value;
			}
		}

		public AggregatesImpl[] CellScopedRVCollections
		{
			get
			{
				return this.m_cellScopedRVCollections;
			}
			set
			{
				this.m_cellScopedRVCollections = value;
			}
		}

		public Hashtable[] CellScopeNames
		{
			get
			{
				return this.m_cellScopeNames;
			}
			set
			{
				this.m_cellScopeNames = value;
			}
		}

		public PivotHeading()
		{
		}

		public PivotHeading(int id, DataRegion matrixDef)
			: base(id, matrixDef)
		{
			this.m_aggregates = new DataAggregateInfoList();
			this.m_postSortAggregates = new DataAggregateInfoList();
			this.m_recursiveAggregates = new DataAggregateInfoList();
		}

		public void CopySubHeadingAggregates()
		{
			if (this.SubHeading != null)
			{
				this.SubHeading.CopySubHeadingAggregates();
				Pivot.CopyAggregates(this.SubHeading.Aggregates, this.m_aggregates);
				Pivot.CopyAggregates(this.SubHeading.PostSortAggregates, this.m_postSortAggregates);
				Pivot.CopyAggregates(this.SubHeading.RecursiveAggregates, this.m_aggregates);
			}
		}

		public void TransferHeadingAggregates()
		{
			if (this.SubHeading != null)
			{
				this.SubHeading.TransferHeadingAggregates();
			}
			if (base.m_grouping != null)
			{
				for (int i = 0; i < this.m_aggregates.Count; i++)
				{
					base.m_grouping.Aggregates.Add(this.m_aggregates[i]);
				}
			}
			this.m_aggregates = null;
			if (base.m_grouping != null)
			{
				for (int j = 0; j < this.m_postSortAggregates.Count; j++)
				{
					base.m_grouping.PostSortAggregates.Add(this.m_postSortAggregates[j]);
				}
			}
			this.m_postSortAggregates = null;
			if (base.m_grouping != null)
			{
				for (int k = 0; k < this.m_recursiveAggregates.Count; k++)
				{
					base.m_grouping.RecursiveAggregates.Add(this.m_recursiveAggregates[k]);
				}
			}
			this.m_recursiveAggregates = null;
		}

		public PivotHeading GetInnerStaticHeading()
		{
			PivotHeading pivotHeading = null;
			Pivot pivot = (Pivot)base.m_dataRegionDef;
			pivotHeading = ((!this.m_isColumn) ? pivot.PivotStaticRows : pivot.PivotStaticColumns);
			if (pivotHeading != null && pivotHeading.Level > this.m_level)
			{
				return pivotHeading;
			}
			return null;
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.Visibility, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.Visibility));
			memberInfoList.Add(new MemberInfo(MemberName.Subtotal, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.Subtotal));
			memberInfoList.Add(new MemberInfo(MemberName.Level, Token.Int32));
			memberInfoList.Add(new MemberInfo(MemberName.IsColumn, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.HasExprHost, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.SubtotalSpan, Token.Int32));
			memberInfoList.Add(new MemberInfo(MemberName.IDs, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.IntList));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportHierarchyNode, memberInfoList);
		}
	}
}
