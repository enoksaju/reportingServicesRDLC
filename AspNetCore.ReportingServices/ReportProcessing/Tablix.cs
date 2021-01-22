using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public abstract class Tablix : DataRegion, IAggregateHolder, IRunningValueHolder
	{
		private int m_columnCount;

		private int m_rowCount;

		protected DataAggregateInfoList m_cellAggregates;

		protected Pivot.ProcessingInnerGroupings m_processingInnerGrouping;

		protected RunningValueInfoList m_runningValues;

		protected DataAggregateInfoList m_cellPostSortAggregates;

		[NonSerialized]
		protected ReportProcessing.RuntimeTablixGroupRootObj m_currentOuterHeadingGroupRoot;

		[NonSerialized]
		protected int m_innermostRowFilterLevel = -1;

		[NonSerialized]
		protected int m_innermostColumnFilterLevel = -1;

		[NonSerialized]
		protected int[] m_outerGroupingIndexes;

		[NonSerialized]
		protected ReportProcessing.AggregateRowInfo[] m_outerGroupingAggregateRowInfo;

		[NonSerialized]
		protected ReportProcessing.AggregateRowInfo m_tablixAggregateRowInfo;

		[NonSerialized]
		protected bool m_processCellRunningValues;

		[NonSerialized]
		protected bool m_processOutermostSTCellRunningValues;

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

		public Pivot.ProcessingInnerGroupings ProcessingInnerGrouping
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

		public abstract TablixHeadingList TablixColumns
		{
			get;
		}

		public abstract TablixHeadingList TablixRows
		{
			get;
		}

		public abstract RunningValueInfoList TablixCellRunningValues
		{
			get;
		}

		public ReportProcessing.RuntimeTablixGroupRootObj CurrentOuterHeadingGroupRoot
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

		public Tablix(ReportItem parent)
			: base(parent)
		{
		}

		public Tablix(int id, ReportItem parent)
			: base(id, parent)
		{
			this.m_runningValues = new RunningValueInfoList();
			this.m_cellAggregates = new DataAggregateInfoList();
			this.m_cellPostSortAggregates = new DataAggregateInfoList();
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

		public void SkipStaticHeading(ref TablixHeadingList tablixHeading, ref TablixHeadingList staticHeading)
		{
			if (tablixHeading != null && tablixHeading[0].Grouping == null)
			{
				staticHeading = tablixHeading;
				tablixHeading = tablixHeading.InnerHeadings();
			}
			else
			{
				staticHeading = null;
			}
		}

		public TablixHeadingList GetOuterHeading()
		{
			if (this.m_processingInnerGrouping == Pivot.ProcessingInnerGroupings.Column)
			{
				return this.TablixRows;
			}
			return this.TablixColumns;
		}

		public abstract TablixHeadingList SkipStatics(TablixHeadingList headings);

		public abstract int GetDynamicHeadingCount(bool outerGroupings);

		public void GetHeadingDefState(out TablixHeadingList outermostColumns, out TablixHeadingList outermostRows, out TablixHeadingList staticColumns, out TablixHeadingList staticRows)
		{
			outermostColumns = this.TablixColumns;
			outermostRows = this.TablixRows;
			staticColumns = null;
			staticRows = null;
			this.SkipStaticHeading(ref outermostColumns, ref staticColumns);
			this.SkipStaticHeading(ref outermostRows, ref staticRows);
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

		public abstract Hashtable GetOuterScopeNames(int dynamicLevel);

		public void SaveTablixAggregateRowInfo(ReportProcessing.ProcessingContext pc)
		{
			if (this.m_tablixAggregateRowInfo == null)
			{
				this.m_tablixAggregateRowInfo = new ReportProcessing.AggregateRowInfo();
			}
			this.m_tablixAggregateRowInfo.SaveAggregateInfo(pc);
		}

		public void RestoreTablixAggregateRowInfo(ReportProcessing.ProcessingContext pc)
		{
			if (this.m_tablixAggregateRowInfo != null)
			{
				this.m_tablixAggregateRowInfo.RestoreAggregateInfo(pc);
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
			Global.Tracer.Assert(this.m_outerGroupingAggregateRowInfo != null && null != this.m_tablixAggregateRowInfo);
			this.m_tablixAggregateRowInfo.CombineAggregateInfo(pc, this.m_outerGroupingAggregateRowInfo[headingLevel]);
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
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.DataRegion, memberInfoList);
		}
	}
}
