using System;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	public abstract class DataSetExprHost : ReportObjectModelProxy
	{
		[CLSCompliant(false)]
		protected IList<CalcFieldExprHost> m_fieldHostsRemotable;

		[CLSCompliant(false)]
		protected IList<JoinConditionExprHost> m_joinConditionExprHostsRemotable;

		public IndexedExprHost QueryParametersHost;

		[CLSCompliant(false)]
		protected IList<FilterExprHost> m_filterHostsRemotable;

		public IndexedExprHost UserSortExpressionsHost;

		public IList<CalcFieldExprHost> FieldHostsRemotable
		{
			get
			{
				return this.m_fieldHostsRemotable;
			}
		}

		public IList<JoinConditionExprHost> JoinConditionExprHostsRemotable
		{
			get
			{
				return this.m_joinConditionExprHostsRemotable;
			}
		}

		public virtual object QueryCommandTextExpr
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
	}
}
