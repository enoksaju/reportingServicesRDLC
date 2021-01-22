using AspNetCore.ReportingServices.ReportProcessing.ExprHostObjectModel;
using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class MultiChart : ReportHierarchyNode
	{
		public enum Layouts
		{
			Automatic,
			Horizontal,
			Vertical
		}

		private Layouts m_layout;

		private int m_maxCount;

		private bool m_syncScale;

		public Layouts Layout
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

		public int MaxCount
		{
			get
			{
				return this.m_maxCount;
			}
			set
			{
				this.m_maxCount = value;
			}
		}

		public bool SyncScale
		{
			get
			{
				return this.m_syncScale;
			}
			set
			{
				this.m_syncScale = value;
			}
		}

		public MultiChart()
		{
		}

		public MultiChart(int id, Chart chartDef)
			: base(id, chartDef)
		{
		}

		public void SetExprHost(MultiChartExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			exprHost.SetReportObjectModel(reportObjectModel);
			base.ReportHierarchyNodeSetExprHost(exprHost.GroupingHost, null, reportObjectModel);
		}

		public new void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MultiChartStart();
			base.Initialize(context);
			context.ExprHostBuilder.MultiChartEnd();
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.Layout, Token.Enum));
			memberInfoList.Add(new MemberInfo(MemberName.MaxCount, Token.Int32));
			memberInfoList.Add(new MemberInfo(MemberName.SyncScale, Token.Boolean));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportHierarchyNode, memberInfoList);
		}
	}
}
