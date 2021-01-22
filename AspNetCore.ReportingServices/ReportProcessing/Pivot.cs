using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using AspNetCore.ReportingServices.ReportRendering;
using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public abstract class Pivot : DataRegion, IAggregateHolder, IRunningValueHolder
	{
		public enum ProcessingInnerGroupings
		{
			Column,
			Row
		}

		private int m_columnCount;

		private int m_rowCount;

		protected DataAggregateInfoList m_cellAggregates;

		protected ProcessingInnerGroupings m_processingInnerGrouping;

		protected RunningValueInfoList m_runningValues;

		protected DataAggregateInfoList m_cellPostSortAggregates;

		private DataElementOutputTypes m_cellDataElementOutput;

		[NonSerialized]
		protected ReportProcessing.RuntimePivotGroupRootObj m_currentOuterHeadingGroupRoot;

		[NonSerialized]
		protected int m_innermostRowFilterLevel = -1;

		[NonSerialized]
		protected int m_innermostColumnFilterLevel = -1;

		[NonSerialized]
		protected int[] m_outerGroupingIndexes;

		[NonSerialized]
		protected ReportProcessing.AggregateRowInfo[] m_outerGroupingAggregateRowInfo;

		[NonSerialized]
		protected ReportProcessing.AggregateRowInfo m_pivotAggregateRowInfo;

		[NonSerialized]
		protected bool m_processCellRunningValues;

		[NonSerialized]
		protected bool m_processOutermostSTCellRunningValues;

		public abstract PivotHeading PivotColumns
		{
			get;
		}

		public abstract PivotHeading PivotRows
		{
			get;
		}

		public int ColumnCount
		{
			get
			{
				return this.m_columnCount;
			}
			set
			{
				this.m_columnCount = value;
			}
		}

		public int RowCount
		{
			get
			{
				return this.m_rowCount;
			}
			set
			{
				this.m_rowCount = value;
			}
		}

		public DataAggregateInfoList CellAggregates
		{
			get
			{
				return this.m_cellAggregates;
			}
			set
			{
				this.m_cellAggregates = value;
			}
		}

		public DataAggregateInfoList CellPostSortAggregates
		{
			get
			{
				return this.m_cellPostSortAggregates;
			}
			set
			{
				this.m_cellPostSortAggregates = value;
			}
		}

		public abstract RunningValueInfoList PivotCellRunningValues
		{
			get;
		}

		public ProcessingInnerGroupings ProcessingInnerGrouping
		{
			get
			{
				return this.m_processingInnerGrouping;
			}
			set
			{
				this.m_processingInnerGrouping = value;
			}
		}

		public abstract PivotHeading PivotStaticColumns
		{
			get;
		}

		public abstract PivotHeading PivotStaticRows
		{
			get;
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

		public DataElementOutputTypes CellDataElementOutput
		{
			get
			{
				return this.m_cellDataElementOutput;
			}
			set
			{
				this.m_cellDataElementOutput = value;
			}
		}

		public int InnermostRowFilterLevel
		{
			get
			{
				return this.m_innermostRowFilterLevel;
			}
			set
			{
				this.m_innermostRowFilterLevel = value;
			}
		}

		public int InnermostColumnFilterLevel
		{
			get
			{
				return this.m_innermostColumnFilterLevel;
			}
			set
			{
				this.m_innermostColumnFilterLevel = value;
			}
		}

		public int[] OuterGroupingIndexes
		{
			get
			{
				return this.m_outerGroupingIndexes;
			}
		}

		public bool ProcessCellRunningValues
		{
			get
			{
				return this.m_processCellRunningValues;
			}
			set
			{
				this.m_processCellRunningValues = value;
			}
		}

		public bool ProcessOutermostSTCellRunningValues
		{
			get
			{
				return this.m_processOutermostSTCellRunningValues;
			}
			set
			{
				this.m_processOutermostSTCellRunningValues = value;
			}
		}

		public ReportProcessing.RuntimePivotGroupRootObj CurrentOuterHeadingGroupRoot
		{
			get
			{
				return this.m_currentOuterHeadingGroupRoot;
			}
			set
			{
				this.m_currentOuterHeadingGroupRoot = value;
			}
		}

		public Pivot(ReportItem parent)
			: base(parent)
		{
		}

		public Pivot(int id, ReportItem parent)
			: base(id, parent)
		{
			this.m_runningValues = new RunningValueInfoList();
			this.m_cellAggregates = new DataAggregateInfoList();
			this.m_cellPostSortAggregates = new DataAggregateInfoList();
		}

		public void CopyHeadingAggregates(PivotHeading heading)
		{
			if (heading != null)
			{
				heading.CopySubHeadingAggregates();
				Pivot.CopyAggregates(heading.Aggregates, base.m_aggregates);
				Pivot.CopyAggregates(heading.PostSortAggregates, base.m_postSortAggregates);
				Pivot.CopyAggregates(heading.RecursiveAggregates, base.m_aggregates);
			}
		}

		public static void CopyAggregates(DataAggregateInfoList srcAggregates, DataAggregateInfoList targetAggregates)
		{
			for (int i = 0; i < srcAggregates.Count; i++)
			{
				DataAggregateInfo dataAggregateInfo = srcAggregates[i];
				targetAggregates.Add(dataAggregateInfo);
				dataAggregateInfo.IsCopied = true;
			}
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

		DataAggregateInfoList[] IAggregateHolder.GetAggregateLists()
		{
			return new DataAggregateInfoList[2]
			{
				base.m_aggregates,
				this.m_cellAggregates
			};
		}

		DataAggregateInfoList[] IAggregateHolder.GetPostSortAggregateLists()
		{
			return new DataAggregateInfoList[2]
			{
				base.m_postSortAggregates,
				this.m_cellPostSortAggregates
			};
		}

		void IAggregateHolder.ClearIfEmpty()
		{
			Global.Tracer.Assert(null != base.m_aggregates);
			if (base.m_aggregates.Count == 0)
			{
				base.m_aggregates = null;
			}
			Global.Tracer.Assert(null != base.m_postSortAggregates);
			if (base.m_postSortAggregates.Count == 0)
			{
				base.m_postSortAggregates = null;
			}
			Global.Tracer.Assert(null != this.m_cellAggregates);
			if (this.m_cellAggregates.Count == 0)
			{
				this.m_cellAggregates = null;
			}
			Global.Tracer.Assert(null != this.m_cellPostSortAggregates);
			if (this.m_cellPostSortAggregates.Count == 0)
			{
				this.m_cellPostSortAggregates = null;
			}
		}

		public bool SubtotalInInnerHeading(ref PivotHeading innerHeading, ref PivotHeading staticHeading)
		{
			this.SkipStaticHeading(ref innerHeading, ref staticHeading);
			if (innerHeading != null && innerHeading.Subtotal != null)
			{
				return true;
			}
			return false;
		}

		public void SkipStaticHeading(ref PivotHeading pivotHeading, ref PivotHeading staticHeading)
		{
			if (pivotHeading != null && pivotHeading.Grouping == null)
			{
				staticHeading = pivotHeading;
				pivotHeading = (PivotHeading)pivotHeading.InnerHierarchy;
			}
			else
			{
				staticHeading = null;
			}
		}

		public void GetHeadingDefState(out PivotHeading outermostColumn, out bool outermostColumnSubtotal, out PivotHeading staticColumn, out PivotHeading outermostRow, out bool outermostRowSubtotal, out PivotHeading staticRow)
		{
			outermostRowSubtotal = false;
			outermostColumnSubtotal = false;
			staticRow = null;
			outermostRow = this.PivotRows;
			outermostRowSubtotal = this.SubtotalInInnerHeading(ref outermostRow, ref staticRow);
			if (outermostRow == null)
			{
				outermostRowSubtotal = true;
			}
			staticColumn = null;
			outermostColumn = this.PivotColumns;
			outermostColumnSubtotal = this.SubtotalInInnerHeading(ref outermostColumn, ref staticColumn);
			if (outermostColumn == null)
			{
				outermostColumnSubtotal = true;
			}
		}

		public PivotHeading GetPivotHeading(bool outerHeading)
		{
			if (outerHeading && this.m_processingInnerGrouping == ProcessingInnerGroupings.Column)
			{
				goto IL_0017;
			}
			if (!outerHeading && this.m_processingInnerGrouping == ProcessingInnerGroupings.Row)
			{
				goto IL_0017;
			}
			return this.PivotColumns;
			IL_0017:
			return this.PivotRows;
		}

		public PivotHeading GetOuterHeading(int level)
		{
			PivotHeading pivotHeading = this.GetPivotHeading(true);
			PivotHeading pivotHeading2 = null;
			for (int i = 0; i <= level; i++)
			{
				this.SkipStaticHeading(ref pivotHeading, ref pivotHeading2);
				if (pivotHeading != null && i < level)
				{
					pivotHeading = (PivotHeading)pivotHeading.InnerHierarchy;
				}
			}
			return pivotHeading;
		}

		public int GetDynamicHeadingCount(bool outerGroupings)
		{
			if (outerGroupings && this.m_processingInnerGrouping == ProcessingInnerGroupings.Column)
			{
				goto IL_0017;
			}
			if (!outerGroupings && this.m_processingInnerGrouping == ProcessingInnerGroupings.Row)
			{
				goto IL_0017;
			}
			int num = this.m_columnCount;
			if (this.PivotStaticColumns != null)
			{
				num--;
			}
			goto IL_003f;
			IL_0017:
			num = this.m_rowCount;
			if (this.PivotStaticRows != null)
			{
				num--;
			}
			goto IL_003f;
			IL_003f:
			return num;
		}

		public int CreateOuterGroupingIndexList()
		{
			int dynamicHeadingCount = this.GetDynamicHeadingCount(true);
			if (this.m_outerGroupingIndexes == null)
			{
				this.m_outerGroupingIndexes = new int[dynamicHeadingCount];
				this.m_outerGroupingAggregateRowInfo = new ReportProcessing.AggregateRowInfo[dynamicHeadingCount];
			}
			return dynamicHeadingCount;
		}

		public Hashtable GetOuterScopeNames(int dynamicLevel)
		{
			Hashtable hashtable = new Hashtable();
			PivotHeading pivotHeading = this.GetPivotHeading(true);
			int num = 0;
			while (num <= dynamicLevel && pivotHeading != null)
			{
				if (pivotHeading.Grouping != null)
				{
					hashtable.Add(pivotHeading.Grouping.Name, pivotHeading.Grouping);
					num++;
				}
				pivotHeading = (PivotHeading)pivotHeading.InnerHierarchy;
			}
			return hashtable;
		}

		public void SavePivotAggregateRowInfo(ReportProcessing.ProcessingContext pc)
		{
			if (this.m_pivotAggregateRowInfo == null)
			{
				this.m_pivotAggregateRowInfo = new ReportProcessing.AggregateRowInfo();
			}
			this.m_pivotAggregateRowInfo.SaveAggregateInfo(pc);
		}

		public void RestorePivotAggregateRowInfo(ReportProcessing.ProcessingContext pc)
		{
			if (this.m_pivotAggregateRowInfo != null)
			{
				this.m_pivotAggregateRowInfo.RestoreAggregateInfo(pc);
			}
		}

		public void SaveOuterGroupingAggregateRowInfo(int headingLevel, ReportProcessing.ProcessingContext pc)
		{
			Global.Tracer.Assert(null != this.m_outerGroupingAggregateRowInfo);
			if (this.m_outerGroupingAggregateRowInfo[headingLevel] == null)
			{
				this.m_outerGroupingAggregateRowInfo[headingLevel] = new ReportProcessing.AggregateRowInfo();
			}
			this.m_outerGroupingAggregateRowInfo[headingLevel].SaveAggregateInfo(pc);
		}

		public void SetCellAggregateRowInfo(int headingLevel, ReportProcessing.ProcessingContext pc)
		{
			Global.Tracer.Assert(this.m_outerGroupingAggregateRowInfo != null && null != this.m_pivotAggregateRowInfo);
			this.m_pivotAggregateRowInfo.CombineAggregateInfo(pc, this.m_outerGroupingAggregateRowInfo[headingLevel]);
		}

		public void ResetOutergGroupingAggregateRowInfo()
		{
			Global.Tracer.Assert(null != this.m_outerGroupingAggregateRowInfo);
			for (int i = 0; i < this.m_outerGroupingAggregateRowInfo.Length; i++)
			{
				this.m_outerGroupingAggregateRowInfo[i] = null;
			}
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.ColumnCount, Token.Int32));
			memberInfoList.Add(new MemberInfo(MemberName.RowCount, Token.Int32));
			memberInfoList.Add(new MemberInfo(MemberName.CellAggregates, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.DataAggregateInfoList));
			memberInfoList.Add(new MemberInfo(MemberName.ProcessingInnerGrouping, Token.Enum));
			memberInfoList.Add(new MemberInfo(MemberName.RunningValues, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.RunningValueInfoList));
			memberInfoList.Add(new MemberInfo(MemberName.CellPostSortAggregates, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.DataAggregateInfoList));
			memberInfoList.Add(new MemberInfo(MemberName.CellDataElementOutput, Token.Enum));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.DataRegion, memberInfoList);
		}
	}
}
