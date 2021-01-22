using System;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	public abstract class DataRegionExprHost<TMemberType, TCellType> : ReportItemExprHost where TMemberType : MemberNodeExprHost<TMemberType> where TCellType : CellExprHost
	{
		[CLSCompliant(false)]
		public IList<FilterExprHost> m_filterHostsRemotable;

		public SortExprHost m_sortHost;

		[CLSCompliant(false)]
        public IList<IMemberNode> m_memberTreeHostsRemotable;

		[CLSCompliant(false)]
		public IList<TCellType> m_cellHostsRemotable;

		public IndexedExprHost UserSortExpressionsHost;

		[CLSCompliant(false)]
		public IList<JoinConditionExprHost> m_joinConditionExprHostsRemotable;

		public virtual object NoRowsExpr
		{
			get
			{
				return null;
			}
		}

		public IList<FilterExprHost> FilterHostsRemotable
		{
			get
			{
				return this.m_filterHostsRemotable;
			}
		}

		public SortExprHost SortHost
		{
			get
			{
				return this.m_sortHost;
			}
		}

		public IList<IMemberNode> MemberTreeHostsRemotable
		{
			get
			{
				return this.m_memberTreeHostsRemotable;
			}
		}

		public IList<TCellType> CellHostsRemotable
		{
			get
			{
				return this.m_cellHostsRemotable;
			}
		}

		public IList<JoinConditionExprHost> JoinConditionExprHostsRemotable
		{
			get
			{
				return this.m_joinConditionExprHostsRemotable;
			}
		}
	}
}
