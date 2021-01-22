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
	public sealed class MapPolygonRules : IPersistable
	{
		[NonSerialized]
		private MapPolygonRulesExprHost m_exprHost;

		[NonSerialized]
		private MapPolygonRulesExprHost m_exprHostMapMember;

		[Reference]
		private Map m_map;

		[NonSerialized]
		private static readonly Declaration m_Declaration = MapPolygonRules.GetDeclaration();

		private MapColorRule m_mapColorRule;

		public MapColorRule MapColorRule
		{
			get
			{
				return this.m_mapColorRule;
			}
			set
			{
				this.m_mapColorRule = value;
			}
		}

		public string OwnerName
		{
			get
			{
				return this.m_map.Name;
			}
		}

		public MapPolygonRulesExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		public MapPolygonRules()
		{
		}

		public MapPolygonRules(Map map)
		{
			this.m_map = map;
		}

		public void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapPolygonRulesStart();
			if (this.m_mapColorRule != null)
			{
				this.m_mapColorRule.Initialize(context);
			}
			context.ExprHostBuilder.MapPolygonRulesEnd();
		}

		public void InitializeMapMember(InitializationContext context)
		{
			context.ExprHostBuilder.MapPolygonRulesStart();
			if (this.m_mapColorRule != null)
			{
				this.m_mapColorRule.InitializeMapMember(context);
			}
			context.ExprHostBuilder.MapPolygonRulesEnd();
		}

		public object PublishClone(AutomaticSubtotalContext context)
		{
			MapPolygonRules mapPolygonRules = (MapPolygonRules)base.MemberwiseClone();
			mapPolygonRules.m_map = context.CurrentMapClone;
			if (this.m_mapColorRule != null)
			{
				mapPolygonRules.m_mapColorRule = (MapColorRule)this.m_mapColorRule.PublishClone(context);
			}
			return mapPolygonRules;
		}

		public void SetExprHost(MapPolygonRulesExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			if (this.m_mapColorRule != null && this.ExprHost.MapColorRuleHost != null)
			{
				this.m_mapColorRule.SetExprHost(this.ExprHost.MapColorRuleHost, reportObjectModel);
			}
		}

		public void SetExprHostMapMember(MapPolygonRulesExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHostMapMember = exprHost;
			this.m_exprHostMapMember.SetReportObjectModel(reportObjectModel);
			if (this.m_mapColorRule != null && this.m_exprHostMapMember.MapColorRuleHost != null)
			{
				this.m_mapColorRule.SetExprHostMapMember(this.m_exprHostMapMember.MapColorRuleHost, reportObjectModel);
			}
		}

		public static Declaration GetDeclaration()
		{
			List<MemberInfo> list = new List<MemberInfo>();
			list.Add(new MemberInfo(MemberName.MapColorRule, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapColorRule));
			list.Add(new MemberInfo(MemberName.Map, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Map, Token.Reference));
			return new Declaration(AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPolygonRules, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, list);
		}

		public void Serialize(IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(MapPolygonRules.m_Declaration);
			while (writer.NextMember())
			{
				switch (writer.CurrentMember.MemberName)
				{
				case MemberName.Map:
					writer.WriteReference(this.m_map);
					break;
				case MemberName.MapColorRule:
					writer.Write(this.m_mapColorRule);
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		public void Deserialize(IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(MapPolygonRules.m_Declaration);
			while (reader.NextMember())
			{
				switch (reader.CurrentMember.MemberName)
				{
				case MemberName.Map:
					this.m_map = reader.ReadReference<Map>(this);
					break;
				case MemberName.MapColorRule:
					this.m_mapColorRule = (MapColorRule)reader.ReadRIFObject();
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		public void ResolveReferences(Dictionary<AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			List<MemberReference> list = default(List<MemberReference>);
			if (memberReferencesCollection.TryGetValue(MapPolygonRules.m_Declaration.ObjectType, out list))
			{
				foreach (MemberReference item in list)
				{
					MemberName memberName = item.MemberName;
					if (memberName == MemberName.Map)
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(item.RefID));
						this.m_map = (Map)referenceableItems[item.RefID];
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}
		}

		public AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPolygonRules;
		}
	}
}
