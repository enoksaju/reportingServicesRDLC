using AspNetCore.ReportingServices.ReportProcessing.ExprHostObjectModel;
using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class GridLines
	{
		private bool m_showGridLines;

		private Style m_styleClass;

		public bool ShowGridLines
		{
			get
			{
				return this.m_showGridLines;
			}
			set
			{
				this.m_showGridLines = value;
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

		public void Initialize(InitializationContext context)
		{
			if (this.m_styleClass != null)
			{
				this.m_styleClass.Initialize(context);
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

		public static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.ShowGridLines, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.StyleClass, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.Style));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.None, memberInfoList);
		}
	}
}
