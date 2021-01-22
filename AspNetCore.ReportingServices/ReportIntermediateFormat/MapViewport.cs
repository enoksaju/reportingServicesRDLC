using AspNetCore.ReportingServices.OnDemandProcessing;
using AspNetCore.ReportingServices.OnDemandReportRendering;
using AspNetCore.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using AspNetCore.ReportingServices.ReportProcessing;
using AspNetCore.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using AspNetCore.ReportingServices.ReportPublishing;
using System;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	[Serializable]
	public sealed class MapViewport : MapSubItem, IPersistable
	{
		[NonSerialized]
		private static readonly Declaration m_Declaration = MapViewport.GetDeclaration();

		private ExpressionInfo m_mapCoordinateSystem;

		private ExpressionInfo m_mapProjection;

		private ExpressionInfo m_projectionCenterX;

		private ExpressionInfo m_projectionCenterY;

		private MapLimits m_mapLimits;

		private MapView m_mapView;

		private ExpressionInfo m_maximumZoom;

		private ExpressionInfo m_minimumZoom;

		private ExpressionInfo m_contentMargin;

		private MapGridLines m_mapMeridians;

		private MapGridLines m_mapParallels;

		private ExpressionInfo m_gridUnderContent;

		private ExpressionInfo m_simplificationResolution;

		public ExpressionInfo MapCoordinateSystem
		{
			get
			{
				return this.m_mapCoordinateSystem;
			}
			set
			{
				this.m_mapCoordinateSystem = value;
			}
		}

		public ExpressionInfo MapProjection
		{
			get
			{
				return this.m_mapProjection;
			}
			set
			{
				this.m_mapProjection = value;
			}
		}

		public ExpressionInfo ProjectionCenterX
		{
			get
			{
				return this.m_projectionCenterX;
			}
			set
			{
				this.m_projectionCenterX = value;
			}
		}

		public ExpressionInfo ProjectionCenterY
		{
			get
			{
				return this.m_projectionCenterY;
			}
			set
			{
				this.m_projectionCenterY = value;
			}
		}

		public MapLimits MapLimits
		{
			get
			{
				return this.m_mapLimits;
			}
			set
			{
				this.m_mapLimits = value;
			}
		}

		public MapView MapView
		{
			get
			{
				return this.m_mapView;
			}
			set
			{
				this.m_mapView = value;
			}
		}

		public ExpressionInfo MaximumZoom
		{
			get
			{
				return this.m_maximumZoom;
			}
			set
			{
				this.m_maximumZoom = value;
			}
		}

		public ExpressionInfo MinimumZoom
		{
			get
			{
				return this.m_minimumZoom;
			}
			set
			{
				this.m_minimumZoom = value;
			}
		}

		public ExpressionInfo SimplificationResolution
		{
			get
			{
				return this.m_simplificationResolution;
			}
			set
			{
				this.m_simplificationResolution = value;
			}
		}

		public ExpressionInfo ContentMargin
		{
			get
			{
				return this.m_contentMargin;
			}
			set
			{
				this.m_contentMargin = value;
			}
		}

		public MapGridLines MapMeridians
		{
			get
			{
				return this.m_mapMeridians;
			}
			set
			{
				this.m_mapMeridians = value;
			}
		}

		public MapGridLines MapParallels
		{
			get
			{
				return this.m_mapParallels;
			}
			set
			{
				this.m_mapParallels = value;
			}
		}

		public ExpressionInfo GridUnderContent
		{
			get
			{
				return this.m_gridUnderContent;
			}
			set
			{
				this.m_gridUnderContent = value;
			}
		}

		public new MapViewportExprHost ExprHost
		{
			get
			{
				return (MapViewportExprHost)base.m_exprHost;
			}
		}

		public MapViewport()
		{
		}

		public MapViewport(Map map)
			: base(map)
		{
		}

		public override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapViewportStart();
			base.Initialize(context);
			if (this.m_mapCoordinateSystem != null)
			{
				this.m_mapCoordinateSystem.Initialize("MapCoordinateSystem", context);
				context.ExprHostBuilder.MapViewportMapCoordinateSystem(this.m_mapCoordinateSystem);
			}
			if (this.m_mapProjection != null)
			{
				this.m_mapProjection.Initialize("MapProjection", context);
				context.ExprHostBuilder.MapViewportMapProjection(this.m_mapProjection);
			}
			if (this.m_projectionCenterX != null)
			{
				this.m_projectionCenterX.Initialize("ProjectionCenterX", context);
				context.ExprHostBuilder.MapViewportProjectionCenterX(this.m_projectionCenterX);
			}
			if (this.m_projectionCenterY != null)
			{
				this.m_projectionCenterY.Initialize("ProjectionCenterY", context);
				context.ExprHostBuilder.MapViewportProjectionCenterY(this.m_projectionCenterY);
			}
			if (this.m_mapLimits != null)
			{
				this.m_mapLimits.Initialize(context);
			}
			if (this.m_mapView != null)
			{
				this.m_mapView.Initialize(context);
			}
			if (this.m_maximumZoom != null)
			{
				this.m_maximumZoom.Initialize("MaximumZoom", context);
				context.ExprHostBuilder.MapViewportMaximumZoom(this.m_maximumZoom);
			}
			if (this.m_minimumZoom != null)
			{
				this.m_minimumZoom.Initialize("MinimumZoom", context);
				context.ExprHostBuilder.MapViewportMinimumZoom(this.m_minimumZoom);
			}
			if (this.m_contentMargin != null)
			{
				this.m_contentMargin.Initialize("ContentMargin", context);
				context.ExprHostBuilder.MapViewportContentMargin(this.m_contentMargin);
			}
			if (this.m_mapMeridians != null)
			{
				this.m_mapMeridians.Initialize(context, true);
			}
			if (this.m_mapParallels != null)
			{
				this.m_mapParallels.Initialize(context, false);
			}
			if (this.m_gridUnderContent != null)
			{
				this.m_gridUnderContent.Initialize("GridUnderContent", context);
				context.ExprHostBuilder.MapViewportGridUnderContent(this.m_gridUnderContent);
			}
			if (this.m_simplificationResolution != null)
			{
				this.m_simplificationResolution.Initialize("SimplificationResolution", context);
				context.ExprHostBuilder.MapViewportSimplificationResolution(this.m_simplificationResolution);
			}
			context.ExprHostBuilder.MapViewportEnd();
		}

		public override object PublishClone(AutomaticSubtotalContext context)
		{
			MapViewport mapViewport = (MapViewport)base.PublishClone(context);
			if (this.m_mapCoordinateSystem != null)
			{
				mapViewport.m_mapCoordinateSystem = (ExpressionInfo)this.m_mapCoordinateSystem.PublishClone(context);
			}
			if (this.m_mapProjection != null)
			{
				mapViewport.m_mapProjection = (ExpressionInfo)this.m_mapProjection.PublishClone(context);
			}
			if (this.m_projectionCenterX != null)
			{
				mapViewport.m_projectionCenterX = (ExpressionInfo)this.m_projectionCenterX.PublishClone(context);
			}
			if (this.m_projectionCenterY != null)
			{
				mapViewport.m_projectionCenterY = (ExpressionInfo)this.m_projectionCenterY.PublishClone(context);
			}
			if (this.m_mapLimits != null)
			{
				mapViewport.m_mapLimits = (MapLimits)this.m_mapLimits.PublishClone(context);
			}
			if (this.m_mapView != null)
			{
				mapViewport.m_mapView = (MapView)this.m_mapView.PublishClone(context);
			}
			if (this.m_maximumZoom != null)
			{
				mapViewport.m_maximumZoom = (ExpressionInfo)this.m_maximumZoom.PublishClone(context);
			}
			if (this.m_minimumZoom != null)
			{
				mapViewport.m_minimumZoom = (ExpressionInfo)this.m_minimumZoom.PublishClone(context);
			}
			if (this.m_contentMargin != null)
			{
				mapViewport.m_contentMargin = (ExpressionInfo)this.m_contentMargin.PublishClone(context);
			}
			if (this.m_mapMeridians != null)
			{
				mapViewport.m_mapMeridians = (MapGridLines)this.m_mapMeridians.PublishClone(context);
			}
			if (this.m_mapParallels != null)
			{
				mapViewport.m_mapParallels = (MapGridLines)this.m_mapParallels.PublishClone(context);
			}
			if (this.m_gridUnderContent != null)
			{
				mapViewport.m_gridUnderContent = (ExpressionInfo)this.m_gridUnderContent.PublishClone(context);
			}
			if (this.m_simplificationResolution != null)
			{
				mapViewport.m_simplificationResolution = (ExpressionInfo)this.m_simplificationResolution.PublishClone(context);
			}
			return mapViewport;
		}

		public void SetExprHost(MapViewportExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			if (this.m_mapLimits != null && this.ExprHost.MapLimitsHost != null)
			{
				this.m_mapLimits.SetExprHost(this.ExprHost.MapLimitsHost, reportObjectModel);
			}
			if (this.m_mapMeridians != null && this.ExprHost.MapMeridiansHost != null)
			{
				this.m_mapMeridians.SetExprHost(this.ExprHost.MapMeridiansHost, reportObjectModel);
			}
			if (this.m_mapParallels != null && this.ExprHost.MapParallelsHost != null)
			{
				this.m_mapParallels.SetExprHost(this.ExprHost.MapParallelsHost, reportObjectModel);
			}
			if (this.m_mapView != null && this.ExprHost.MapViewHost != null)
			{
				this.m_mapView.SetExprHost(this.ExprHost.MapViewHost, reportObjectModel);
			}
		}

		public new static Declaration GetDeclaration()
		{
			List<MemberInfo> list = new List<MemberInfo>();
			list.Add(new MemberInfo(MemberName.MapCoordinateSystem, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo));
			list.Add(new MemberInfo(MemberName.MapProjection, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo));
			list.Add(new MemberInfo(MemberName.ProjectionCenterX, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo));
			list.Add(new MemberInfo(MemberName.ProjectionCenterY, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo));
			list.Add(new MemberInfo(MemberName.MapLimits, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLimits));
			list.Add(new MemberInfo(MemberName.MapView, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapView));
			list.Add(new MemberInfo(MemberName.MaximumZoom, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo));
			list.Add(new MemberInfo(MemberName.MinimumZoom, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo));
			list.Add(new MemberInfo(MemberName.ContentMargin, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo));
			list.Add(new MemberInfo(MemberName.MapMeridians, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapGridLines));
			list.Add(new MemberInfo(MemberName.MapParallels, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapGridLines));
			list.Add(new MemberInfo(MemberName.GridUnderContent, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo));
			list.Add(new MemberInfo(MemberName.SimplificationResolution, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo));
			return new Declaration(AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapViewport, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSubItem, list);
		}

		public override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapViewport.m_Declaration);
			while (writer.NextMember())
			{
				switch (writer.CurrentMember.MemberName)
				{
				case MemberName.MapCoordinateSystem:
					writer.Write(this.m_mapCoordinateSystem);
					break;
				case MemberName.MapProjection:
					writer.Write(this.m_mapProjection);
					break;
				case MemberName.ProjectionCenterX:
					writer.Write(this.m_projectionCenterX);
					break;
				case MemberName.ProjectionCenterY:
					writer.Write(this.m_projectionCenterY);
					break;
				case MemberName.MapLimits:
					writer.Write(this.m_mapLimits);
					break;
				case MemberName.MapView:
					writer.Write(this.m_mapView);
					break;
				case MemberName.MaximumZoom:
					writer.Write(this.m_maximumZoom);
					break;
				case MemberName.MinimumZoom:
					writer.Write(this.m_minimumZoom);
					break;
				case MemberName.ContentMargin:
					writer.Write(this.m_contentMargin);
					break;
				case MemberName.MapMeridians:
					writer.Write(this.m_mapMeridians);
					break;
				case MemberName.MapParallels:
					writer.Write(this.m_mapParallels);
					break;
				case MemberName.GridUnderContent:
					writer.Write(this.m_gridUnderContent);
					break;
				case MemberName.SimplificationResolution:
					writer.Write(this.m_simplificationResolution);
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		public override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapViewport.m_Declaration);
			while (reader.NextMember())
			{
				switch (reader.CurrentMember.MemberName)
				{
				case MemberName.MapCoordinateSystem:
					this.m_mapCoordinateSystem = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.MapProjection:
					this.m_mapProjection = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.ProjectionCenterX:
					this.m_projectionCenterX = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.ProjectionCenterY:
					this.m_projectionCenterY = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.MapLimits:
					this.m_mapLimits = (MapLimits)reader.ReadRIFObject();
					break;
				case MemberName.MapView:
					this.m_mapView = (MapView)reader.ReadRIFObject();
					break;
				case MemberName.MaximumZoom:
					this.m_maximumZoom = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.MinimumZoom:
					this.m_minimumZoom = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.ContentMargin:
					this.m_contentMargin = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.MapMeridians:
					this.m_mapMeridians = (MapGridLines)reader.ReadRIFObject();
					break;
				case MemberName.MapParallels:
					this.m_mapParallels = (MapGridLines)reader.ReadRIFObject();
					break;
				case MemberName.GridUnderContent:
					this.m_gridUnderContent = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.SimplificationResolution:
					this.m_simplificationResolution = (ExpressionInfo)reader.ReadRIFObject();
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		public override AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapViewport;
		}

		public MapCoordinateSystem EvaluateMapCoordinateSystem(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.m_map, reportScopeInstance);
			return EnumTranslator.TranslateMapCoordinateSystem(context.ReportRuntime.EvaluateMapViewportMapCoordinateSystemExpression(this, base.m_map.Name), context.ReportRuntime);
		}

		public MapProjection EvaluateMapProjection(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.m_map, reportScopeInstance);
			return EnumTranslator.TranslateMapProjection(context.ReportRuntime.EvaluateMapViewportMapProjectionExpression(this, base.m_map.Name), context.ReportRuntime);
		}

		public double EvaluateProjectionCenterX(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapViewportProjectionCenterXExpression(this, base.m_map.Name);
		}

		public double EvaluateProjectionCenterY(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapViewportProjectionCenterYExpression(this, base.m_map.Name);
		}

		public double EvaluateMaximumZoom(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapViewportMaximumZoomExpression(this, base.m_map.Name);
		}

		public double EvaluateMinimumZoom(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapViewportMinimumZoomExpression(this, base.m_map.Name);
		}

		public string EvaluateContentMargin(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapViewportContentMarginExpression(this, base.m_map.Name);
		}

		public bool EvaluateGridUnderContent(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapViewportGridUnderContentExpression(this, base.m_map.Name);
		}

		public double EvaluateSimplificationResolution(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapViewportSimplificationResolutionExpression(this, base.m_map.Name);
		}
	}
}
