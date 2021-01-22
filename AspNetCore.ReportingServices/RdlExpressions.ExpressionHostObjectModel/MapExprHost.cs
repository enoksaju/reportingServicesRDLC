using System;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	public abstract class MapExprHost : ReportItemExprHost
	{
		public MapViewportExprHost MapViewportHost;

		[CLSCompliant(false)]
		protected IList<MapPolygonLayerExprHost> m_mapPolygonLayersHostsRemotable;

		[CLSCompliant(false)]
		protected IList<MapPointLayerExprHost> m_mapPointLayersHostsRemotable;

		[CLSCompliant(false)]
		protected IList<MapLineLayerExprHost> m_mapLineLayersHostsRemotable;

		[CLSCompliant(false)]
		protected IList<MapTileLayerExprHost> m_mapTileLayersHostsRemotable;

		[CLSCompliant(false)]
		protected IList<MapLegendExprHost> m_mapLegendsHostsRemotable;

		[CLSCompliant(false)]
		protected IList<MapTitleExprHost> m_mapTitlesHostsRemotable;

		public MapDistanceScaleExprHost MapDistanceScaleHost;

		public MapColorScaleExprHost MapColorScaleHost;

		public MapBorderSkinExprHost MapBorderSkinHost;

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

		public IList<MapTileLayerExprHost> MapTileLayersHostsRemotable
		{
			get
			{
				return this.m_mapTileLayersHostsRemotable;
			}
		}

		public IList<MapLegendExprHost> MapLegendsHostsRemotable
		{
			get
			{
				return this.m_mapLegendsHostsRemotable;
			}
		}

		public IList<MapTitleExprHost> MapTitlesHostsRemotable
		{
			get
			{
				return this.m_mapTitlesHostsRemotable;
			}
		}

		public virtual object AntiAliasingExpr
		{
			get
			{
				return null;
			}
		}

		public virtual object TextAntiAliasingQualityExpr
		{
			get
			{
				return null;
			}
		}

		public virtual object ShadowIntensityExpr
		{
			get
			{
				return null;
			}
		}

		public virtual object TileLanguageExpr
		{
			get
			{
				return null;
			}
		}
	}
}
