using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public abstract class TablixHeading : ReportHierarchyNode
	{
		protected bool m_subtotal;

		protected bool m_isColumn;

		protected int m_level;

		protected bool m_hasExprHost;

		protected int m_headingSpan = 1;

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

		public new ReportHierarchyNode InnerHierarchy
		{
			get
			{
				throw new InvalidOperationException();
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public bool Subtotal
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

		public int HeadingSpan
		{
			get
			{
				return this.m_headingSpan;
			}
			set
			{
				this.m_headingSpan = value;
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

		public TablixHeading()
		{
		}

		public TablixHeading(int id, DataRegion dataRegionDef)
			: base(id, dataRegionDef)
		{
			this.m_aggregates = new DataAggregateInfoList();
			this.m_postSortAggregates = new DataAggregateInfoList();
			this.m_recursiveAggregates = new DataAggregateInfoList();
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.Subtotal, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.IsColumn, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.Level, Token.Int32));
			memberInfoList.Add(new MemberInfo(MemberName.HasExprHost, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.HeadingSpan, Token.Int32));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportHierarchyNode, memberInfoList);
		}
	}
}
