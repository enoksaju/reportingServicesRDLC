using System;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	public abstract class MapShapefileExprHost : MapSpatialDataExprHost
	{
		[CLSCompliant(false)]
		protected IList<MapFieldNameExprHost> m_mapFieldNamesHostsRemotable;

		public virtual object SourceExpr
		{
			get
			{
				return null;
			}
		}

		public IList<MapFieldNameExprHost> MapFieldNamesHostsRemotable
		{
			get
			{
				return this.m_mapFieldNamesHostsRemotable;
			}
		}
	}
}
