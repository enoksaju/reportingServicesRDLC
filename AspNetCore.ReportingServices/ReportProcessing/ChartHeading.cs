using AspNetCore.ReportingServices.ReportProcessing.ExprHostObjectModel;
using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ChartHeading : PivotHeading, IRunningValueHolder
	{
		private ExpressionInfoList m_labels;

		private RunningValueInfoList m_runningValues;

		private bool m_chartGroupExpression;

		private BoolList m_plotTypesLine;

		[NonSerialized]
		private ChartDynamicGroupExprHost m_exprHost;

		public new ChartHeading SubHeading
		{
			get
			{
				return (ChartHeading)base.m_innerHierarchy;
			}
			set
			{
				base.m_innerHierarchy = value;
			}
		}

		public ExpressionInfoList Labels
		{
			get
			{
				return this.m_labels;
			}
			set
			{
				this.m_labels = value;
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

		public bool ChartGroupExpression
		{
			get
			{
				return this.m_chartGroupExpression;
			}
			set
			{
				this.m_chartGroupExpression = value;
			}
		}

		public BoolList PlotTypesLine
		{
			get
			{
				return this.m_plotTypesLine;
			}
			set
			{
				this.m_plotTypesLine = value;
			}
		}

		public ChartDynamicGroupExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		public ChartHeading()
		{
		}

		public ChartHeading(int id, Chart chartDef)
			: base(id, chartDef)
		{
			this.m_runningValues = new RunningValueInfoList();
		}

		RunningValueInfoList IRunningValueHolder.GetRunningValueList()
		{
			return this.m_runningValues;
		}

		void IRunningValueHolder.ClearIfEmpty()
		{
			Global.Tracer.Assert(null != this.m_runningValues);
			if (this.m_runningValues.Count == 0)
			{
				this.m_runningValues = null;
			}
		}

		public void SetExprHost(ChartDynamicGroupExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null && base.HasExprHost);
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			base.ReportHierarchyNodeSetExprHost(this.m_exprHost, reportObjectModel);
		}

		public void LabelCollectionInitialize(InitializationContext context, bool registerRunningValues, bool isStatic)
		{
			if (isStatic)
			{
				if (base.m_isColumn)
				{
					context.ExprHostBuilder.ChartStaticColumnLabelsStart();
				}
				else
				{
					context.ExprHostBuilder.ChartStaticRowLabelsStart();
				}
			}
			if (registerRunningValues)
			{
				context.RegisterRunningValues(this.m_runningValues);
			}
			Global.Tracer.Assert(null != this.m_labels);
			for (int i = 0; i < this.m_labels.Count; i++)
			{
				Global.Tracer.Assert(null != this.m_labels[i]);
				this.m_labels[i].Initialize("Label", context);
				if (isStatic)
				{
					context.ExprHostBuilder.ChartStaticColumnRowLabel(this.m_labels[i]);
				}
				else
				{
					context.ExprHostBuilder.ChartHeadingLabel(this.m_labels[i]);
				}
			}
			if (registerRunningValues)
			{
				context.UnRegisterRunningValues(this.m_runningValues);
			}
			if (isStatic)
			{
				if (base.m_isColumn)
				{
					context.ExprHostBuilder.ChartStaticColumnLabelsEnd();
				}
				else
				{
					context.ExprHostBuilder.ChartStaticRowLabelsEnd();
				}
			}
		}

		public int DynamicInitialize(bool column, int level, InitializationContext context)
		{
			base.m_level = level;
			base.m_isColumn = column;
			if (base.m_grouping == null)
			{
				if (this.SubHeading != null)
				{
					this.SubHeading.DynamicInitialize(column, ++level, context);
				}
				return 1;
			}
			context.ExprHostBuilder.ChartDynamicGroupStart(base.m_grouping.Name);
			if (base.m_subtotal != null)
			{
				base.m_subtotal.RegisterReportItems(context);
				base.m_subtotal.Initialize(context);
			}
			context.Location |= LocationFlags.InGrouping;
			context.RegisterGroupingScope(base.m_grouping.Name, base.m_grouping.SimpleGroupExpressions, base.m_grouping.Aggregates, base.m_grouping.PostSortAggregates, base.m_grouping.RecursiveAggregates, base.m_grouping);
			ObjectType objectType = context.ObjectType;
			string objectName = context.ObjectName;
			context.ObjectType = ObjectType.Grouping;
			context.ObjectName = base.m_grouping.Name;
			base.Initialize(context);
			if (base.m_visibility != null)
			{
				base.m_visibility.Initialize(context, true, false);
			}
			if (this.SubHeading != null)
			{
				base.m_subtotalSpan = this.SubHeading.DynamicInitialize(column, ++level, context);
			}
			else
			{
				base.m_subtotalSpan = 1;
			}
			if (this.m_labels != null)
			{
				this.LabelCollectionInitialize(context, true, false);
			}
			if (base.m_visibility != null)
			{
				base.m_visibility.UnRegisterReceiver(context);
			}
			context.ObjectType = objectType;
			context.ObjectName = objectName;
			context.UnRegisterGroupingScope(base.m_grouping.Name);
			if (base.m_subtotal != null)
			{
				base.m_subtotal.UnregisterReportItems(context);
			}
			base.m_hasExprHost = context.ExprHostBuilder.ChartDynamicGroupEnd(column);
			return base.m_subtotalSpan + 1;
		}

		public int StaticInitialize(InitializationContext context)
		{
			if (base.m_grouping != null)
			{
				int num = 1;
				if (this.SubHeading != null)
				{
					context.Location |= LocationFlags.InGrouping;
					context.RegisterGroupingScope(base.m_grouping.Name, base.m_grouping.SimpleGroupExpressions, base.m_aggregates, base.m_postSortAggregates, base.m_recursiveAggregates, base.m_grouping);
					num = this.SubHeading.StaticInitialize(context);
					context.UnRegisterGroupingScope(base.m_grouping.Name);
				}
				return num + 1;
			}
			if (this.SubHeading != null)
			{
				base.m_subtotalSpan = this.SubHeading.StaticInitialize(context);
			}
			else
			{
				base.m_subtotalSpan = 1;
			}
			if (this.m_labels != null)
			{
				this.LabelCollectionInitialize(context, true, true);
			}
			return 0;
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.Labels, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ExpressionInfoList));
			memberInfoList.Add(new MemberInfo(MemberName.RunningValues, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.RunningValueInfoList));
			memberInfoList.Add(new MemberInfo(MemberName.ChartGroupExpression, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.PlotTypesLine, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.BoolList));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.PivotHeading, memberInfoList);
		}
	}
}
