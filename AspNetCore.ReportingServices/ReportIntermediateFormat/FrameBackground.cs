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
	public sealed class FrameBackground : GaugePanelStyleContainer, IPersistable
	{
		[NonSerialized]
		private FrameBackgroundExprHost m_exprHost;

		[NonSerialized]
		private static readonly Declaration m_Declaration = FrameBackground.GetDeclaration();

		public string OwnerName
		{
			get
			{
				return base.m_gaugePanel.Name;
			}
		}

		public FrameBackgroundExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		public FrameBackground()
		{
		}

		public FrameBackground(GaugePanel gaugePanel)
			: base(gaugePanel)
		{
		}

		public override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.FrameBackgroundStart();
			base.Initialize(context);
			context.ExprHostBuilder.FrameBackgroundEnd();
		}

		public override object PublishClone(AutomaticSubtotalContext context)
		{
			return (FrameBackground)base.PublishClone(context);
		}

		public void SetExprHost(FrameBackgroundExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
		}

		public new static Declaration GetDeclaration()
		{
			List<MemberInfo> memberInfoList = new List<MemberInfo>();
			return new Declaration(AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.FrameBackground, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanelStyleContainer, memberInfoList);
		}

		public override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(FrameBackground.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		public override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(FrameBackground.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		public override AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.FrameBackground;
		}
	}
}
