using AspNetCore.ReportingServices.ReportProcessing.ExprHostObjectModel;
using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class OWCChart : DataRegion, IRunningValueHolder
	{
		private ChartColumnList m_chartData;

		private string m_chartDefinition;

		private RunningValueInfoList m_detailRunningValues;

		private RunningValueInfoList m_runningValues;

		[NonSerialized]
		private OWCChartExprHost m_exprHost;

		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.OWCChart;
			}
		}

		public ChartColumnList ChartData
		{
			get
			{
				return this.m_chartData;
			}
			set
			{
				this.m_chartData = value;
			}
		}

		public string ChartDefinition
		{
			get
			{
				return this.m_chartDefinition;
			}
			set
			{
				this.m_chartDefinition = value;
			}
		}

		public RunningValueInfoList DetailRunningValues
		{
			get
			{
				return this.m_detailRunningValues;
			}
			set
			{
				this.m_detailRunningValues = value;
			}
		}

		public RunningValueInfoList RunningValues
		{
			get
			{
				return this.m_runningValues;
			}
			set
			{
				this.m_runningValues = value;
			}
		}

		public OWCChartExprHost OWCChartExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		protected override DataRegionExprHost DataRegionExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		public OWCChart(ReportItem parent)
			: base(parent)
		{
		}

		public OWCChart(int id, ReportItem parent)
			: base(id, parent)
		{
			this.m_chartData = new ChartColumnList();
			this.m_detailRunningValues = new RunningValueInfoList();
			this.m_runningValues = new RunningValueInfoList();
		}

		public override bool Initialize(InitializationContext context)
		{
			context.ObjectType = this.ObjectType;
			context.ObjectName = base.m_name;
			context.RegisterDataRegion(this);
			this.InternalInitialize(context);
			context.UnRegisterDataRegion(this);
			return false;
		}

		private void InternalInitialize(InitializationContext context)
		{
			context.Location = (context.Location | LocationFlags.InDataSet | LocationFlags.InDataRegion);
			context.ExprHostBuilder.OWCChartStart(base.m_name);
			base.Initialize(context);
			context.Location &= ~LocationFlags.InMatrixCellTopLevelItem;
			context.RegisterRunningValues(this.m_runningValues);
			if (base.m_visibility != null)
			{
				base.m_visibility.Initialize(context, false, false);
			}
			context.UnRegisterRunningValues(this.m_runningValues);
			context.RegisterRunningValues(this.m_detailRunningValues);
			if (this.m_chartData != null)
			{
				context.ExprHostBuilder.OWCChartColumnsStart();
				for (int i = 0; i < this.m_chartData.Count; i++)
				{
					this.m_chartData[i].Initialize(context);
				}
				context.ExprHostBuilder.OWCChartColumnsEnd();
			}
			context.UnRegisterRunningValues(this.m_detailRunningValues);
			base.ExprHostID = context.ExprHostBuilder.OWCChartEnd();
		}

		public override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null);
				this.m_exprHost = reportExprHost.OWCChartHostsRemotable[base.ExprHostID];
				base.DataRegionSetExprHost(this.m_exprHost, reportObjectModel);
			}
		}

		RunningValueInfoList IRunningValueHolder.GetRunningValueList()
		{
			return this.m_runningValues;
		}

		void IRunningValueHolder.ClearIfEmpty()
		{
			Global.Tracer.Assert(null != this.m_detailRunningValues);
			if (this.m_detailRunningValues.Count == 0)
			{
				this.m_detailRunningValues = null;
			}
			Global.Tracer.Assert(null != this.m_runningValues);
			if (this.m_runningValues.Count == 0)
			{
				this.m_runningValues = null;
			}
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.ChartData, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ChartColumnList));
			memberInfoList.Add(new MemberInfo(MemberName.ChartDefinition, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.DetailRunningValues, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.RunningValueInfoList));
			memberInfoList.Add(new MemberInfo(MemberName.RunningValues, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.RunningValueInfoList));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.DataRegion, memberInfoList);
		}
	}
}
