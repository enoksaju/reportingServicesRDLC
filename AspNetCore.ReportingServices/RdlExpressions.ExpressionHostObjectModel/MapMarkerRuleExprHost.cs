using System;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	public abstract class MapMarkerRuleExprHost : MapAppearanceRuleExprHost
	{
		[CLSCompliant(false)]
		protected IList<MapMarkerExprHost> m_mapMarkersHostsRemotable;

		public IList<MapMarkerExprHost> MapMarkersHostsRemotable
		{
			get
			{
				return this.m_mapMarkersHostsRemotable;
			}
		}
	}
}
