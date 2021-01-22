using AspNetCore.ReportingServices.ReportProcessing.ExprHostObjectModel;
using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class CustomReportItemHeading : TablixHeading, IRunningValueHolder
	{
		private bool m_static;

		private CustomReportItemHeadingList m_innerHeadings;

		private DataValueList m_customProperties;

		private int m_exprHostID = -1;

		private RunningValueInfoList m_runningValues;

		[NonSerialized]
		private DataGroupingExprHost m_exprHost;

		public bool Static
		{
			get
			{
				return this.m_static;
			}
			set
			{
				this.m_static = value;
			}
		}

		public CustomReportItemHeadingList InnerHeadings
		{
			get
			{
				return this.m_innerHeadings;
			}
			set
			{
				this.m_innerHeadings = value;
			}
		}

		public DataValueList CustomProperties
		{
			get
			{
				return this.m_customProperties;
			}
			set
			{
				this.m_customProperties = value;
			}
		}

		public int ExprHostID
		{
			get
			{
				return this.m_exprHostID;
			}
			set
			{
				this.m_exprHostID = value;
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

		public DataGroupingExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		public CustomReportItemHeading()
		{
		}

		public CustomReportItemHeading(int id, CustomReportItem crItem)
			: base(id, crItem)
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

		public bool Initialize(int level, CustomReportItemHeadingList peerHeadings, int headingIndex, DataCellsList dataRowCells, ref int currentIndex, ref int maxLevel, InitializationContext context)
		{
			base.m_level = level;
			if (level > maxLevel)
			{
				maxLevel = level;
			}
			context.ExprHostBuilder.DataGroupingStart(base.m_isColumn);
			if (this.m_static)
			{
				Global.Tracer.Assert(!base.m_subtotal);
				if (base.m_grouping != null)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsInvalidStaticDataGrouping, Severity.Error, context.ObjectType, context.ObjectName, "DataGrouping");
					base.m_grouping = null;
				}
				else
				{
					base.m_sorting = null;
					this.CommonInitialize(level, dataRowCells, ref currentIndex, ref maxLevel, context);
				}
			}
			else
			{
				if ((context.Location & LocationFlags.InDetail) != 0)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsInvalidDetailDataGrouping, Severity.Error, context.ObjectType, context.ObjectName, "DataGrouping");
					return false;
				}
				if (base.m_grouping != null && base.m_grouping.CustomProperties != null)
				{
					if (this.m_customProperties == null)
					{
						this.m_customProperties = new DataValueList(base.m_grouping.CustomProperties.Count);
					}
					this.m_customProperties.AddRange(base.m_grouping.CustomProperties);
					base.m_grouping.CustomProperties = null;
				}
				if (base.m_subtotal)
				{
					if (base.m_grouping != null)
					{
						context.AggregateRewriteScopes = new Hashtable();
						context.AggregateRewriteScopes.Add(base.m_grouping.Name, null);
					}
					Global.Tracer.Assert(null != peerHeadings[headingIndex]);
					int num = currentIndex;
					CustomReportItemHeading customReportItemHeading = CustomReportItemHeading.HeadingClone(this, dataRowCells, ref num, base.m_headingSpan, context);
					customReportItemHeading.m_innerHeadings = CustomReportItemHeading.HeadingListClone(this.m_innerHeadings, dataRowCells, ref num, base.m_headingSpan, context);
					Global.Tracer.Assert(currentIndex + base.m_headingSpan == num);
					Global.Tracer.Assert(!customReportItemHeading.m_subtotal && base.m_subtotal);
					Global.Tracer.Assert(headingIndex < peerHeadings.Count);
					peerHeadings.Insert(headingIndex + 1, customReportItemHeading);
					context.AggregateRewriteScopes = null;
					context.AggregateRewriteMap = null;
				}
				if (base.m_grouping != null)
				{
					context.Location |= LocationFlags.InGrouping;
					context.RegisterGroupingScope(base.m_grouping.Name, base.m_grouping.SimpleGroupExpressions, base.m_grouping.Aggregates, base.m_grouping.PostSortAggregates, base.m_grouping.RecursiveAggregates, base.m_grouping);
					ObjectType objectType = context.ObjectType;
					string objectName = context.ObjectName;
					context.ObjectType = ObjectType.Grouping;
					context.ObjectName = base.m_grouping.Name;
					this.CommonInitialize(level, dataRowCells, ref currentIndex, ref maxLevel, context);
					context.ObjectType = objectType;
					context.ObjectName = objectName;
					context.UnRegisterGroupingScope(base.m_grouping.Name);
				}
				else
				{
					context.Location |= LocationFlags.InDetail;
					this.CommonInitialize(level, dataRowCells, ref currentIndex, ref maxLevel, context);
				}
			}
			this.m_exprHostID = context.ExprHostBuilder.DataGroupingEnd(base.m_isColumn);
			base.m_hasExprHost |= (this.m_exprHostID >= 0);
			return base.m_subtotal;
		}

		private void CommonInitialize(int level, DataCellsList dataRowCells, ref int currentIndex, ref int maxLevel, InitializationContext context)
		{
			base.Initialize(context);
			if (this.m_customProperties != null)
			{
				context.RegisterRunningValues(this.m_runningValues);
				this.m_customProperties.Initialize(null, true, context);
				context.UnRegisterRunningValues(this.m_runningValues);
			}
			if (this.m_innerHeadings != null)
			{
				Global.Tracer.Assert(null != context.AggregateEscalateScopes);
				if (base.m_grouping != null)
				{
					context.AggregateEscalateScopes.Add(base.m_grouping.Name);
				}
				base.m_headingSpan += this.m_innerHeadings.Initialize(level + 1, dataRowCells, ref currentIndex, ref maxLevel, context);
				if (base.m_grouping != null)
				{
					context.AggregateEscalateScopes.RemoveAt(context.AggregateEscalateScopes.Count - 1);
				}
			}
			else
			{
				currentIndex++;
			}
		}

		private static CustomReportItemHeading HeadingClone(CustomReportItemHeading heading, DataCellsList dataRowCells, ref int currentIndex, int headingSpan, InitializationContext context)
		{
			Global.Tracer.Assert(null != heading);
			CustomReportItemHeading customReportItemHeading = new CustomReportItemHeading(context.GenerateSubtotalID(), (CustomReportItem)heading.DataRegionDef);
			customReportItemHeading.m_isColumn = heading.m_isColumn;
			customReportItemHeading.m_level = heading.m_level;
			customReportItemHeading.m_static = true;
			customReportItemHeading.m_subtotal = false;
			customReportItemHeading.m_headingSpan = heading.m_headingSpan;
			if (heading.m_customProperties != null)
			{
				customReportItemHeading.m_customProperties = heading.m_customProperties.DeepClone(context);
			}
			if (heading.m_innerHeadings == null)
			{
				if (heading.m_isColumn)
				{
					int count = dataRowCells.Count;
					for (int i = 0; i < count; i++)
					{
						DataCellList dataCellList = dataRowCells[i];
						Global.Tracer.Assert(currentIndex + headingSpan <= dataCellList.Count);
						dataCellList.Insert(currentIndex + headingSpan, dataCellList[currentIndex].DeepClone(context));
					}
				}
				else
				{
					Global.Tracer.Assert(currentIndex + headingSpan <= dataRowCells.Count);
					DataCellList dataCellList2 = dataRowCells[currentIndex];
					int count2 = dataCellList2.Count;
					DataCellList dataCellList3 = new DataCellList(count2);
					dataRowCells.Insert(currentIndex + headingSpan, dataCellList3);
					for (int j = 0; j < count2; j++)
					{
						dataCellList3.Add(dataCellList2[j].DeepClone(context));
					}
				}
				currentIndex++;
			}
			return customReportItemHeading;
		}

		private static CustomReportItemHeadingList HeadingListClone(CustomReportItemHeadingList headings, DataCellsList dataRowCells, ref int currentIndex, int headingSpan, InitializationContext context)
		{
			if (headings == null)
			{
				return null;
			}
			int count = headings.Count;
			Global.Tracer.Assert(1 <= count);
			CustomReportItemHeadingList customReportItemHeadingList = new CustomReportItemHeadingList(count);
			for (int i = 0; i < count; i++)
			{
				CustomReportItemHeading customReportItemHeading = headings[i];
				if (customReportItemHeading.m_grouping != null)
				{
					context.AggregateRewriteScopes.Add(customReportItemHeading.m_grouping.Name, null);
				}
				CustomReportItemHeading customReportItemHeading2 = CustomReportItemHeading.HeadingClone(customReportItemHeading, dataRowCells, ref currentIndex, headingSpan, context);
				if (customReportItemHeading.m_innerHeadings != null)
				{
					customReportItemHeading2.m_innerHeadings = CustomReportItemHeading.HeadingListClone(customReportItemHeading.m_innerHeadings, dataRowCells, ref currentIndex, headingSpan, context);
				}
				if (customReportItemHeading.m_grouping != null)
				{
					context.AggregateRewriteScopes.Remove(customReportItemHeading.m_grouping.Name);
				}
				customReportItemHeadingList.Add(customReportItemHeading2);
			}
			return customReportItemHeadingList;
		}

		public static bool ValidateProcessingRestrictions(CustomReportItemHeadingList headings, bool isColumn, bool hasStatic, InitializationContext context)
		{
			bool flag = true;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			string propertyName = isColumn ? "column" : "row";
			if (headings != null)
			{
				for (int i = 0; i < headings.Count; i++)
				{
					CustomReportItemHeading customReportItemHeading = headings[i];
					if (!customReportItemHeading.Static && customReportItemHeading.Grouping == null)
					{
						context.ErrorContext.Register(ProcessingErrorCode.rsInvalidGrouping, Severity.Error, context.ObjectType, context.ObjectName, propertyName);
						flag = false;
					}
					if (customReportItemHeading.Subtotal)
					{
						context.ErrorContext.Register(ProcessingErrorCode.rsCRISubtotalNotSupported, Severity.Error, context.ObjectType, context.ObjectName, propertyName);
						flag = false;
					}
					if (customReportItemHeading.Static && hasStatic)
					{
						flag3 = true;
					}
					if (customReportItemHeading.Static && customReportItemHeading.InnerHeadings != null)
					{
						flag4 = true;
					}
					if (!customReportItemHeading.Static && headings.Count > 1)
					{
						flag2 = true;
					}
					if (flag && !flag2 && !flag3 && !flag4 && customReportItemHeading.InnerHeadings != null && !CustomReportItemHeading.ValidateProcessingRestrictions(customReportItemHeading.InnerHeadings, isColumn, customReportItemHeading.Static, context))
					{
						flag = false;
					}
				}
			}
			if (flag3)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsCRIMultiStaticColumnsOrRows, Severity.Error, context.ObjectType, context.ObjectName, propertyName);
				flag = false;
			}
			if (flag4)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsCRIStaticWithSubgroups, Severity.Error, context.ObjectType, context.ObjectName, propertyName);
				flag = false;
			}
			if (flag2)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsCRIMultiNonStaticGroups, Severity.Error, context.ObjectType, context.ObjectName, propertyName);
				flag = false;
			}
			return flag;
		}

		public void CopySubHeadingAggregates()
		{
			if (this.m_innerHeadings != null)
			{
				int count = this.m_innerHeadings.Count;
				for (int i = 0; i < count; i++)
				{
					CustomReportItemHeading customReportItemHeading = this.m_innerHeadings[i];
					customReportItemHeading.CopySubHeadingAggregates();
					Tablix.CopyAggregates(customReportItemHeading.Aggregates, base.m_aggregates);
					Tablix.CopyAggregates(customReportItemHeading.PostSortAggregates, base.m_postSortAggregates);
					Tablix.CopyAggregates(customReportItemHeading.RecursiveAggregates, base.m_aggregates);
				}
			}
		}

		public void TransferHeadingAggregates()
		{
			if (this.m_innerHeadings != null)
			{
				this.m_innerHeadings.TransferHeadingAggregates();
			}
			if (base.m_grouping != null)
			{
				for (int i = 0; i < base.m_aggregates.Count; i++)
				{
					base.m_grouping.Aggregates.Add(base.m_aggregates[i]);
				}
			}
			base.m_aggregates = null;
			if (base.m_grouping != null)
			{
				for (int j = 0; j < base.m_postSortAggregates.Count; j++)
				{
					base.m_grouping.PostSortAggregates.Add(base.m_postSortAggregates[j]);
				}
			}
			base.m_postSortAggregates = null;
			if (base.m_grouping != null)
			{
				for (int k = 0; k < base.m_recursiveAggregates.Count; k++)
				{
					base.m_grouping.RecursiveAggregates.Add(base.m_recursiveAggregates[k]);
				}
			}
			base.m_recursiveAggregates = null;
		}

		public void SetExprHost(IList<DataGroupingExprHost> dataGroupingHosts, ObjectModelImpl reportObjectModel)
		{
			if (this.m_exprHostID >= 0)
			{
				Global.Tracer.Assert(dataGroupingHosts != null && dataGroupingHosts.Count > this.m_exprHostID && reportObjectModel != null);
				this.m_exprHost = dataGroupingHosts[this.m_exprHostID];
				this.m_exprHost.SetReportObjectModel(reportObjectModel);
				if (this.m_exprHost.GroupingHost != null)
				{
					Global.Tracer.Assert(null != base.m_grouping);
					base.m_grouping.SetExprHost(this.m_exprHost.GroupingHost, reportObjectModel);
				}
				if (this.m_exprHost.SortingHost != null)
				{
					Global.Tracer.Assert(null != base.m_sorting);
					base.m_sorting.SetExprHost(this.m_exprHost.SortingHost, reportObjectModel);
				}
				if (this.m_customProperties != null)
				{
					Global.Tracer.Assert(null != this.m_customProperties);
					this.m_customProperties.SetExprHost(this.m_exprHost.CustomPropertyHostsRemotable, reportObjectModel);
				}
			}
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.Static, Token.Boolean));
			memberInfoList.Add(new MemberInfo(MemberName.InnerHeadings, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.CustomReportItemHeadingList));
			memberInfoList.Add(new MemberInfo(MemberName.CustomProperties, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.DataValueList));
			memberInfoList.Add(new MemberInfo(MemberName.ExprHostID, Token.Int32));
			memberInfoList.Add(new MemberInfo(MemberName.RunningValues, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.RunningValueInfoList));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.TablixHeading, memberInfoList);
		}
	}
}
