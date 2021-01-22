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
	public sealed class Thermometer : GaugePanelStyleContainer, IPersistable
	{
		[NonSerialized]
		private ThermometerExprHost m_exprHost;

		[NonSerialized]
		private static readonly Declaration m_Declaration = Thermometer.GetDeclaration();

		private ExpressionInfo m_bulbOffset;

		private ExpressionInfo m_bulbSize;

		private ExpressionInfo m_thermometerStyle;

		public ExpressionInfo BulbOffset
		{
			get
			{
				return this.m_bulbOffset;
			}
			set
			{
				this.m_bulbOffset = value;
			}
		}

		public ExpressionInfo BulbSize
		{
			get
			{
				return this.m_bulbSize;
			}
			set
			{
				this.m_bulbSize = value;
			}
		}

		public ExpressionInfo ThermometerStyle
		{
			get
			{
				return this.m_thermometerStyle;
			}
			set
			{
				this.m_thermometerStyle = value;
			}
		}

		public string OwnerName
		{
			get
			{
				return base.m_gaugePanel.Name;
			}
		}

		public ThermometerExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		public Thermometer()
		{
		}

		public Thermometer(GaugePanel gaugePanel)
			: base(gaugePanel)
		{
		}

		public override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.ThermometerStart();
			base.Initialize(context);
			if (this.m_bulbOffset != null)
			{
				this.m_bulbOffset.Initialize("BulbOffset", context);
				context.ExprHostBuilder.ThermometerBulbOffset(this.m_bulbOffset);
			}
			if (this.m_bulbSize != null)
			{
				this.m_bulbSize.Initialize("BulbSize", context);
				context.ExprHostBuilder.ThermometerBulbSize(this.m_bulbSize);
			}
			if (this.m_thermometerStyle != null)
			{
				this.m_thermometerStyle.Initialize("ThermometerStyle", context);
				context.ExprHostBuilder.ThermometerThermometerStyle(this.m_thermometerStyle);
			}
			context.ExprHostBuilder.ThermometerEnd();
		}

		public override object PublishClone(AutomaticSubtotalContext context)
		{
			Thermometer thermometer = (Thermometer)base.PublishClone(context);
			if (this.m_bulbOffset != null)
			{
				thermometer.m_bulbOffset = (ExpressionInfo)this.m_bulbOffset.PublishClone(context);
			}
			if (this.m_bulbSize != null)
			{
				thermometer.m_bulbSize = (ExpressionInfo)this.m_bulbSize.PublishClone(context);
			}
			if (this.m_thermometerStyle != null)
			{
				thermometer.m_thermometerStyle = (ExpressionInfo)this.m_thermometerStyle.PublishClone(context);
			}
			return thermometer;
		}

		public void SetExprHost(ThermometerExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
		}

		public new static Declaration GetDeclaration()
		{
			List<MemberInfo> list = new List<MemberInfo>();
			list.Add(new MemberInfo(MemberName.BulbOffset, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo));
			list.Add(new MemberInfo(MemberName.BulbSize, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo));
			list.Add(new MemberInfo(MemberName.ThermometerStyle, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo));
			return new Declaration(AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Thermometer, AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanelStyleContainer, list);
		}

		public override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(Thermometer.m_Declaration);
			while (writer.NextMember())
			{
				switch (writer.CurrentMember.MemberName)
				{
				case MemberName.BulbOffset:
					writer.Write(this.m_bulbOffset);
					break;
				case MemberName.BulbSize:
					writer.Write(this.m_bulbSize);
					break;
				case MemberName.ThermometerStyle:
					writer.Write(this.m_thermometerStyle);
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
			reader.RegisterDeclaration(Thermometer.m_Declaration);
			while (reader.NextMember())
			{
				switch (reader.CurrentMember.MemberName)
				{
				case MemberName.BulbOffset:
					this.m_bulbOffset = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.BulbSize:
					this.m_bulbSize = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.ThermometerStyle:
					this.m_thermometerStyle = (ExpressionInfo)reader.ReadRIFObject();
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		public override AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Thermometer;
		}

		public double EvaluateBulbOffset(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateThermometerBulbOffsetExpression(this, base.m_gaugePanel.Name);
		}

		public double EvaluateBulbSize(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateThermometerBulbSizeExpression(this, base.m_gaugePanel.Name);
		}

		public GaugeThermometerStyles EvaluateThermometerStyle(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateGaugeThermometerStyles(context.ReportRuntime.EvaluateThermometerThermometerStyleExpression(this, base.m_gaugePanel.Name), context.ReportRuntime);
		}
	}
}
