using System;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	public abstract class MapMemberExprHost : MemberNodeExprHost<MapMemberExprHost>
	{
		[CLSCompliant(false)]
		protected IList<MapPolygonLayerExprHost> m_mapPolygonLayersHostsRemotable;

		[CLSCompliant(false)]
		protected IList<MapPointLayerExprHost> m_mapPointLayersHostsRemotable;

		[CLSCompliant(false)]
		protected IList<MapLineLayerExprHost> m_mapLineLayersHostsRemotable;

		public IList<MapPolygonLayerExprHost> MapPolygonLayersHostsRemotable
		{
			get
			{
				return this.m_mapPolygonLayersHostsRemotable;
			}
		}

		public IList<MapPointLayerExprHost> MapPointLayersHostsRemotable
		{
			get
			{
				return this.m_mapPointLayersHostsRemotable;
			}
		}

		public IList<MapLineLayerExprHost> MapLineLayersHostsRemotable
		{
			get
			{
				return this.m_mapLineLayersHostsRemotable;
			}
		}
	}
}
