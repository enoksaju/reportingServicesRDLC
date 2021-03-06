using AspNetCore.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using AspNetCore.ReportingServices.ReportProcessing;
using AspNetCore.ReportingServices.ReportPublishing;
using System;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	[Serializable]
	public sealed class MapDataBoundView : MapView, IPersistable
	{
		[NonSerialized]
		private static readonly Declaration m_Declaration = MapDataBoundView.GetDeclaration();

		public new MapDataBoundViewExprHost ExprHost
		{
			get
			{
				return (MapDataBoundViewExprHost)base.m_exprHost;
			}
		}

		public MapDataBoundView()
		{
		}

		public MapDataBoundView(Map map)
			: base(map)
		{
		}

		public override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapDataBoundViewStart();
			base.Initialize(context);
			context.ExprHostBuilder.MapDataBoundViewEnd();
		}

		public override object PublishClone(AutomaticSubtotalContext context)
		{
			return (MapDataBoundView)base.PublishClone(context);
		}

		public new static Declaration GetDeclaration()
		{
			List<MemberInfo> memberInfoList = new List<MemberInfo>();
			return new Declaration(AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapDataBoundView, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapView, memberInfoList);
		}

		public override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapDataBoundView.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		public override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapDataBoundView.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		public override AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapDataBoundView;
		}
	}
}
