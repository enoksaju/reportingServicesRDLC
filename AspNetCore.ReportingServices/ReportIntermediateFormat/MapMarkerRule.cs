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
	public sealed class MapMarkerRule : MapAppearanceRule, IPersistable
	{
		[NonSerialized]
		private static readonly Declaration m_Declaration = MapMarkerRule.GetDeclaration();

		private List<MapMarker> m_mapMarkers;

		public List<MapMarker> MapMarkers
		{
			get
			{
				return this.m_mapMarkers;
			}
			set
			{
				this.m_mapMarkers = value;
			}
		}

		public new MapMarkerRuleExprHost ExprHost
		{
			get
			{
				return (MapMarkerRuleExprHost)base.m_exprHost;
			}
		}

		public MapMarkerRule()
		{
		}

		public MapMarkerRule(MapVectorLayer mapVectorLayer, Map map)
			: base(mapVectorLayer, map)
		{
		}

		public override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapMarkerRuleStart();
			base.Initialize(context);
			if (this.m_mapMarkers != null)
			{
				for (int i = 0; i < this.m_mapMarkers.Count; i++)
				{
					this.m_mapMarkers[i].Initialize(context, i);
				}
			}
			context.ExprHostBuilder.MapMarkerRuleEnd();
		}

		public override void InitializeMapMember(InitializationContext context)
		{
			context.ExprHostBuilder.MapMarkerRuleStart();
			base.InitializeMapMember(context);
			context.ExprHostBuilder.MapMarkerRuleEnd();
		}

		public override object PublishClone(AutomaticSubtotalContext context)
		{
			MapMarkerRule mapMarkerRule = (MapMarkerRule)base.PublishClone(context);
			if (this.m_mapMarkers != null)
			{
				mapMarkerRule.m_mapMarkers = new List<MapMarker>(this.m_mapMarkers.Count);
				{
					foreach (MapMarker mapMarker in this.m_mapMarkers)
					{
						mapMarkerRule.m_mapMarkers.Add((MapMarker)mapMarker.PublishClone(context));
					}
					return mapMarkerRule;
				}
			}
			return mapMarkerRule;
		}

		public override void SetExprHost(MapAppearanceRuleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			IList<MapMarkerExprHost> mapMarkersHostsRemotable = this.ExprHost.MapMarkersHostsRemotable;
			if (this.m_mapMarkers != null && mapMarkersHostsRemotable != null)
			{
				for (int i = 0; i < this.m_mapMarkers.Count; i++)
				{
					MapMarker mapMarker = this.m_mapMarkers[i];
					if (mapMarker != null && mapMarker.ExpressionHostID > -1)
					{
						mapMarker.SetExprHost(mapMarkersHostsRemotable[mapMarker.ExpressionHostID], reportObjectModel);
					}
				}
			}
		}

		public new static Declaration GetDeclaration()
		{
			List<MemberInfo> list = new List<MemberInfo>();
			list.Add(new MemberInfo(MemberName.MapMarkers, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapMarker));
			return new Declaration(AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapMarkerRule, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapAppearanceRule, list);
		}

		public override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapMarkerRule.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName == MemberName.MapMarkers)
				{
					writer.Write(this.m_mapMarkers);
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
			reader.RegisterDeclaration(MapMarkerRule.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName == MemberName.MapMarkers)
				{
					this.m_mapMarkers = reader.ReadGenericListOfRIFObjects<MapMarker>();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		public override AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapMarkerRule;
		}
	}
}
