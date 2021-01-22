using AspNetCore.ReportingServices.OnDemandProcessing;
using AspNetCore.ReportingServices.OnDemandReportRendering;
using AspNetCore.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using AspNetCore.ReportingServices.ReportProcessing;
using AspNetCore.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using AspNetCore.ReportingServices.ReportPublishing;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	[Serializable]
	public sealed class MapFieldName : IPersistable
	{
		private int m_exprHostID = -1;

		[NonSerialized]
		private MapFieldNameExprHost m_exprHost;

		[Reference]
		private Map m_map;

		[NonSerialized]
		private static readonly Declaration m_Declaration = MapFieldName.GetDeclaration();

		private ExpressionInfo m_name;

		public ExpressionInfo Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		public string OwnerName
		{
			get
			{
				return this.m_map.Name;
			}
		}

		public MapFieldNameExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		public int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		public MapFieldName()
		{
		}

		public MapFieldName(Map map)
		{
			this.m_map = map;
		}

		public void Initialize(InitializationContext context, int index)
		{
			context.ExprHostBuilder.MapFieldNameStart(index.ToString(CultureInfo.InvariantCulture.NumberFormat));
			if (this.m_name != null)
			{
				this.m_name.Initialize("Name", context);
				context.ExprHostBuilder.MapFieldNameName(this.m_name);
			}
			this.m_exprHostID = context.ExprHostBuilder.MapFieldNameEnd();
		}

		public object PublishClone(AutomaticSubtotalContext context)
		{
			MapFieldName mapFieldName = (MapFieldName)base.MemberwiseClone();
			mapFieldName.m_map = context.CurrentMapClone;
			if (this.m_name != null)
			{
				mapFieldName.m_name = (ExpressionInfo)this.m_name.PublishClone(context);
			}
			return mapFieldName;
		}

		public void SetExprHost(MapFieldNameExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
		}

		public static Declaration GetDeclaration()
		{
			List<MemberInfo> list = new List<MemberInfo>();
			list.Add(new MemberInfo(MemberName.Name, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo));
			list.Add(new MemberInfo(MemberName.Map, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Map, Token.Reference));
			list.Add(new MemberInfo(MemberName.ExprHostID, Token.Int32));
			return new Declaration(AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapFieldName, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, list);
		}

		public void Serialize(IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(MapFieldName.m_Declaration);
			while (writer.NextMember())
			{
				switch (writer.CurrentMember.MemberName)
				{
				case MemberName.Map:
					writer.WriteReference(this.m_map);
					break;
				case MemberName.Name:
					writer.Write(this.m_name);
					break;
				case MemberName.ExprHostID:
					writer.Write(this.m_exprHostID);
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		public void Deserialize(IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(MapFieldName.m_Declaration);
			while (reader.NextMember())
			{
				switch (reader.CurrentMember.MemberName)
				{
				case MemberName.Map:
					this.m_map = reader.ReadReference<Map>(this);
					break;
				case MemberName.Name:
					this.m_name = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.ExprHostID:
					this.m_exprHostID = reader.ReadInt32();
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
			if (memberReferencesCollection.TryGetValue(MapFieldName.m_Declaration.ObjectType, out list))
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
			return AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapFieldName;
		}

		public string EvaluateName(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapFieldNameNameExpression(this, this.m_map.Name);
		}
	}
}
