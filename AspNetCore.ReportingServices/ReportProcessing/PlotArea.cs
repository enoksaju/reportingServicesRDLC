using AspNetCore.ReportingServices.ReportProcessing.ExprHostObjectModel;
using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class PlotArea
	{
		public enum Origins
		{
			BottomLeft,
			TopLeft,
			TopRight,
			BottomRight
		}

		private Origins m_origin;

		private Style m_styleClass;

		public Origins Origin
		{
			get
			{
				return this.m_origin;
			}
			set
			{
				this.m_origin = value;
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
			context.ExprHostBuilder.ChartPlotAreaStart();
			if (this.m_styleClass != null)
			{
				this.m_styleClass.Initialize(context);
			}
			context.ExprHostBuilder.ChartPlotAreaEnd();
		}

		public static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.Origin, Token.Enum));
			memberInfoList.Add(new MemberInfo(MemberName.StyleClass, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.Style));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.None, memberInfoList);
		}
	}
}
