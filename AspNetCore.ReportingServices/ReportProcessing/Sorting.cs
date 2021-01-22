using AspNetCore.ReportingServices.ReportProcessing.ExprHostObjectModel;
using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class Sorting
	{
		private ExpressionInfoList m_sortExpressions;

		private BoolList m_sortDirections;

		[NonSerialized]
		private SortingExprHost m_exprHost;

		public ExpressionInfoList SortExpressions
		{
			get
			{
				return this.m_sortExpressions;
			}
			set
			{
				this.m_sortExpressions = value;
			}
		}

		public BoolList SortDirections
		{
			get
			{
				return this.m_sortDirections;
			}
			set
			{
				this.m_sortDirections = value;
			}
		}

		public SortingExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		public Sorting(ConstructionPhase phase)
		{
			if (phase == ConstructionPhase.Publishing)
			{
				this.m_sortExpressions = new ExpressionInfoList();
				this.m_sortDirections = new BoolList();
			}
		}

		public void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.SortingStart();
			if (this.m_sortExpressions != null)
			{
				for (int i = 0; i < this.m_sortExpressions.Count; i++)
				{
					ExpressionInfo expressionInfo = this.m_sortExpressions[i];
					expressionInfo.Initialize("SortExpression", context);
					context.ExprHostBuilder.SortingExpression(expressionInfo);
				}
			}
			context.ExprHostBuilder.SortingEnd();
		}

		public void SetExprHost(SortingExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
		}

		public static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.SortExpressions, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ExpressionInfoList));
			memberInfoList.Add(new MemberInfo(MemberName.SortDirections, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.BoolList));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.None, memberInfoList);
		}
	}
}
