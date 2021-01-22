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
	public sealed class MapBorderSkin : MapStyleContainer, IPersistable
	{
		[NonSerialized]
		private MapBorderSkinExprHost m_exprHost;

		[NonSerialized]
		private static readonly Declaration m_Declaration = MapBorderSkin.GetDeclaration();

		private ExpressionInfo m_mapBorderSkinType;

		public ExpressionInfo MapBorderSkinType
		{
			get
			{
				return this.m_mapBorderSkinType;
			}
			set
			{
				this.m_mapBorderSkinType = value;
			}
		}

		public string OwnerName
		{
			get
			{
				return base.m_map.Name;
			}
		}

		public MapBorderSkinExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		public MapBorderSkin()
		{
		}

		public MapBorderSkin(Map map)
			: base(map)
		{
		}

		public override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapBorderSkinStart();
			base.Initialize(context);
			if (this.m_mapBorderSkinType != null)
			{
				this.m_mapBorderSkinType.Initialize("MapBorderSkinType", context);
				context.ExprHostBuilder.MapBorderSkinMapBorderSkinType(this.m_mapBorderSkinType);
			}
			context.ExprHostBuilder.MapBorderSkinEnd();
		}

		public override object PublishClone(AutomaticSubtotalContext context)
		{
			MapBorderSkin mapBorderSkin = (MapBorderSkin)base.PublishClone(context);
			if (this.m_mapBorderSkinType != null)
			{
				mapBorderSkin.m_mapBorderSkinType = (ExpressionInfo)this.m_mapBorderSkinType.PublishClone(context);
			}
			return mapBorderSkin;
		}

		public void SetExprHost(MapBorderSkinExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			base.SetExprHost(exprHost, reportObjectModel);
		}

		public new static Declaration GetDeclaration()
		{
			List<MemberInfo> list = new List<MemberInfo>();
			list.Add(new MemberInfo(MemberName.MapBorderSkinType, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo));
			return new Declaration(AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapBorderSkin, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapStyleContainer, list);
		}

		public override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapBorderSkin.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName == MemberName.MapBorderSkinType)
				{
					writer.Write(this.m_mapBorderSkinType);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		public override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapBorderSkin.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName == MemberName.MapBorderSkinType)
				{
					this.m_mapBorderSkinType = (ExpressionInfo)reader.ReadRIFObject();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		public override AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapBorderSkin;
		}

		public MapBorderSkinType EvaluateMapBorderSkinType(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.m_map, reportScopeInstance);
			return EnumTranslator.TranslateMapBorderSkinType(context.ReportRuntime.EvaluateMapBorderSkinMapBorderSkinTypeExpression(this, base.m_map.Name), context.ReportRuntime);
		}
	}
}
