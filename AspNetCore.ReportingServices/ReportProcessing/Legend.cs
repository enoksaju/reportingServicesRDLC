using AspNetCore.ReportingServices.ReportProcessing.ExprHostObjectModel;
using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class Legend
	{
		public enum LegendLayout
		{
			Column,
			Row,
			Table
		}

		public enum Positions
		{
			RightTop,
			TopLeft,
			TopCenter,
			TopRight,
			LeftTop,
			LeftCenter,
			LeftBottom,
			RightCenter,
			RightBottom,
			BottomLeft,
			BottomCenter,
			BottomRight
		}

		private bool m_visible;

		private Style m_styleClass;

		private Positions m_position;

		private LegendLayout m_layout;

		private bool m_insidePlotArea;

		public bool Visible
		{
			get
			{
				return this.m_visible;
			}
			set
			{
				this.m_visible = value;
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

		public LegendLayout Layout
		{
			get
			{
				return this.m_layout;
			}
			set
			{
				this.m_layout = value;
			}
		}

		public bool InsidePlotArea
		{
			get
			{
				return this.m_insidePlotArea;
			}
			set
			{
				this.m_insidePlotArea = value;
			}
		}

		public void SetExprHost(StyleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && null != reportObjectModel);
			exprHost.SetReportObjectModel(reportObjectModel);
			if (this.m_styleClass != null)
			{
				this.m_styleClass.SetStyleExprHost(exprHost);
			}
		}

		public void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.ChartLegendStart();
			if (this.m_styleClass != null)
			{
				this.m_styleClass.Initialize(context);
			}
			context.ExprHostBuilder.ChartLegendEnd();
		}

		public static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.Visible, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.StyleClass, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.Style));
			memberInfoList.Add(new MemberInfo(MemberName.Position, Token.Enum));
			memberInfoList.Add(new MemberInfo(MemberName.Layout, Token.Enum));
			memberInfoList.Add(new MemberInfo(MemberName.InsidePlotArea, Token.Boolean));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.None, memberInfoList);
		}
	}
}
