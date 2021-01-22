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
	public sealed class MapPolygonTemplate : MapSpatialElementTemplate, IPersistable
	{
		[NonSerialized]
		private static readonly Declaration m_Declaration = MapPolygonTemplate.GetDeclaration();

		private ExpressionInfo m_scaleFactor;

		private ExpressionInfo m_centerPointOffsetX;

		private ExpressionInfo m_centerPointOffsetY;

		private ExpressionInfo m_showLabel;

		private ExpressionInfo m_labelPlacement;

		public ExpressionInfo ScaleFactor
		{
			get
			{
				return this.m_scaleFactor;
			}
			set
			{
				this.m_scaleFactor = value;
			}
		}

		public ExpressionInfo CenterPointOffsetX
		{
			get
			{
				return this.m_centerPointOffsetX;
			}
			set
			{
				this.m_centerPointOffsetX = value;
			}
		}

		public ExpressionInfo CenterPointOffsetY
		{
			get
			{
				return this.m_centerPointOffsetY;
			}
			set
			{
				this.m_centerPointOffsetY = value;
			}
		}

		public ExpressionInfo ShowLabel
		{
			get
			{
				return this.m_showLabel;
			}
			set
			{
				this.m_showLabel = value;
			}
		}

		public ExpressionInfo LabelPlacement
		{
			get
			{
				return this.m_labelPlacement;
			}
			set
			{
				this.m_labelPlacement = value;
			}
		}

		public new MapPolygonTemplateExprHost ExprHost
		{
			get
			{
				return (MapPolygonTemplateExprHost)base.m_exprHost;
			}
		}

		public MapPolygonTemplate()
		{
		}

		public MapPolygonTemplate(MapPolygonLayer mapPolygonLayer, Map map, int id)
			: base(mapPolygonLayer, map, id)
		{
		}

		public override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapPolygonTemplateStart();
			base.Initialize(context);
			if (this.m_scaleFactor != null)
			{
				this.m_scaleFactor.Initialize("ScaleFactor", context);
				context.ExprHostBuilder.MapPolygonTemplateScaleFactor(this.m_scaleFactor);
			}
			if (this.m_centerPointOffsetX != null)
			{
				this.m_centerPointOffsetX.Initialize("CenterPointOffsetX", context);
				context.ExprHostBuilder.MapPolygonTemplateCenterPointOffsetX(this.m_centerPointOffsetX);
			}
			if (this.m_centerPointOffsetY != null)
			{
				this.m_centerPointOffsetY.Initialize("CenterPointOffsetY", context);
				context.ExprHostBuilder.MapPolygonTemplateCenterPointOffsetY(this.m_centerPointOffsetY);
			}
			if (this.m_showLabel != null)
			{
				this.m_showLabel.Initialize("ShowLabel", context);
				context.ExprHostBuilder.MapPolygonTemplateShowLabel(this.m_showLabel);
			}
			if (this.m_labelPlacement != null)
			{
				this.m_labelPlacement.Initialize("LabelPlacement", context);
				context.ExprHostBuilder.MapPolygonTemplateLabelPlacement(this.m_labelPlacement);
			}
			context.ExprHostBuilder.MapPolygonTemplateEnd();
		}

		public override object PublishClone(AutomaticSubtotalContext context)
		{
			MapPolygonTemplate mapPolygonTemplate = (MapPolygonTemplate)base.PublishClone(context);
			if (this.m_scaleFactor != null)
			{
				mapPolygonTemplate.m_scaleFactor = (ExpressionInfo)this.m_scaleFactor.PublishClone(context);
			}
			if (this.m_centerPointOffsetX != null)
			{
				mapPolygonTemplate.m_centerPointOffsetX = (ExpressionInfo)this.m_centerPointOffsetX.PublishClone(context);
			}
			if (this.m_centerPointOffsetY != null)
			{
				mapPolygonTemplate.m_centerPointOffsetY = (ExpressionInfo)this.m_centerPointOffsetY.PublishClone(context);
			}
			if (this.m_showLabel != null)
			{
				mapPolygonTemplate.m_showLabel = (ExpressionInfo)this.m_showLabel.PublishClone(context);
			}
			if (this.m_labelPlacement != null)
			{
				mapPolygonTemplate.m_labelPlacement = (ExpressionInfo)this.m_labelPlacement.PublishClone(context);
			}
			return mapPolygonTemplate;
		}

		public void SetExprHost(MapPolygonTemplateExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
		}

		public new static Declaration GetDeclaration()
		{
			List<MemberInfo> list = new List<MemberInfo>();
			list.Add(new MemberInfo(MemberName.ScaleFactor, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo));
			list.Add(new MemberInfo(MemberName.CenterPointOffsetX, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo));
			list.Add(new MemberInfo(MemberName.CenterPointOffsetY, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo));
			list.Add(new MemberInfo(MemberName.ShowLabel, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo));
			list.Add(new MemberInfo(MemberName.LabelPlacement, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo));
			return new Declaration(AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPolygonTemplate, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSpatialElementTemplate, list);
		}

		public override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapPolygonTemplate.m_Declaration);
			while (writer.NextMember())
			{
				switch (writer.CurrentMember.MemberName)
				{
				case MemberName.ScaleFactor:
					writer.Write(this.m_scaleFactor);
					break;
				case MemberName.CenterPointOffsetX:
					writer.Write(this.m_centerPointOffsetX);
					break;
				case MemberName.CenterPointOffsetY:
					writer.Write(this.m_centerPointOffsetY);
					break;
				case MemberName.ShowLabel:
					writer.Write(this.m_showLabel);
					break;
				case MemberName.LabelPlacement:
					writer.Write(this.m_labelPlacement);
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
			reader.RegisterDeclaration(MapPolygonTemplate.m_Declaration);
			while (reader.NextMember())
			{
				switch (reader.CurrentMember.MemberName)
				{
				case MemberName.ScaleFactor:
					this.m_scaleFactor = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.CenterPointOffsetX:
					this.m_centerPointOffsetX = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.CenterPointOffsetY:
					this.m_centerPointOffsetY = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.ShowLabel:
					this.m_showLabel = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.LabelPlacement:
					this.m_labelPlacement = (ExpressionInfo)reader.ReadRIFObject();
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		public override AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPolygonTemplate;
		}

		public double EvaluateScaleFactor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapPolygonTemplateScaleFactorExpression(this, base.m_map.Name);
		}

		public double EvaluateCenterPointOffsetX(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapPolygonTemplateCenterPointOffsetXExpression(this, base.m_map.Name);
		}

		public double EvaluateCenterPointOffsetY(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapPolygonTemplateCenterPointOffsetYExpression(this, base.m_map.Name);
		}

		public MapAutoBool EvaluateShowLabel(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.InstancePath, reportScopeInstance);
			return EnumTranslator.TranslateMapAutoBool(context.ReportRuntime.EvaluateMapPolygonTemplateShowLabelExpression(this, base.m_map.Name), context.ReportRuntime);
		}

		public MapPolygonLabelPlacement EvaluateLabelPlacement(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.InstancePath, reportScopeInstance);
			return EnumTranslator.TranslateMapPolygonLabelPlacement(context.ReportRuntime.EvaluateMapPolygonTemplateLabelPlacementExpression(this, base.m_map.Name), context.ReportRuntime);
		}
	}
}
