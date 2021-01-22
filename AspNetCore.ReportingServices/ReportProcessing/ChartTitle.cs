using AspNetCore.ReportingServices.ReportProcessing.ExprHostObjectModel;
using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ChartTitle
	{
		public enum Positions
		{
			Center,
			Near,
			Far
		}

		private ExpressionInfo m_caption;

		private Style m_styleClass;

		private Positions m_position;

		[NonSerialized]
		private ChartTitleExprHost m_exprHost;

		public ExpressionInfo Caption
		{
			get
			{
				return this.m_caption;
			}
			set
			{
				this.m_caption = value;
			}
		}

		public Style StyleClass
		{
			get
			{
				return this.m_styleClass;
			}
			set
			{
				this.m_styleClass = value;
			}
		}

		public Positions Position
		{
			get
			{
				return this.m_position;
			}
			set
			{
				this.m_position = value;
			}
		}

		public ChartTitleExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		public void SetExprHost(ChartTitleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			if (this.m_styleClass != null)
			{
				this.m_styleClass.SetStyleExprHost(this.m_exprHost);
			}
		}

		public void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.ChartTitleStart();
			if (this.m_caption != null)
			{
				this.m_caption.Initialize("Caption", context);
				context.ExprHostBuilder.ChartCaption(this.m_caption);
			}
			if (this.m_styleClass != null)
			{
				this.m_styleClass.Initialize(context);
			}
			context.ExprHostBuilder.ChartTitleEnd();
		}

		public static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.Caption, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ExpressionInfo));
			memberInfoList.Add(new MemberInfo(MemberName.StyleClass, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.Style));
			memberInfoList.Add(new MemberInfo(MemberName.Position, Token.Enum));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.None, memberInfoList);
		}
	}
}
