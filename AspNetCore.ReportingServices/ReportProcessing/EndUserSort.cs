using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class EndUserSort
	{
		private int m_dataSetID = -1;

		[Reference]
		private ISortFilterScope m_sortExpressionScope;

		[Reference]
		private GroupingList m_groupsInSortTarget;

		[Reference]
		private ISortFilterScope m_sortTarget;

		private int m_sortExpressionIndex = -1;

		private SubReportList m_detailScopeSubReports;

		[NonSerialized]
		private ExpressionInfo m_sortExpression;

		[NonSerialized]
		private int m_sortExpressionScopeID = -1;

		[NonSerialized]
		private IntList m_groupInSortTargetIDs;

		[NonSerialized]
		private int m_sortTargetID = -1;

		[NonSerialized]
		private string m_sortExpressionScopeString;

		[NonSerialized]
		private string m_sortTargetString;

		[NonSerialized]
		private bool m_foundSortExpressionScope;

		public int DataSetID
		{
			get
			{
				return this.m_dataSetID;
			}
			set
			{
				this.m_dataSetID = value;
			}
		}

		public ISortFilterScope SortExpressionScope
		{
			get
			{
				return this.m_sortExpressionScope;
			}
			set
			{
				this.m_sortExpressionScope = value;
			}
		}

		public GroupingList GroupsInSortTarget
		{
			get
			{
				return this.m_groupsInSortTarget;
			}
			set
			{
				this.m_groupsInSortTarget = value;
			}
		}

		public ISortFilterScope SortTarget
		{
			get
			{
				return this.m_sortTarget;
			}
			set
			{
				this.m_sortTarget = value;
			}
		}

		public int SortExpressionIndex
		{
			get
			{
				return this.m_sortExpressionIndex;
			}
			set
			{
				this.m_sortExpressionIndex = value;
			}
		}

		public SubReportList DetailScopeSubReports
		{
			get
			{
				return this.m_detailScopeSubReports;
			}
			set
			{
				this.m_detailScopeSubReports = value;
			}
		}

		public ExpressionInfo SortExpression
		{
			get
			{
				return this.m_sortExpression;
			}
			set
			{
				this.m_sortExpression = value;
			}
		}

		public int SortExpressionScopeID
		{
			get
			{
				return this.m_sortExpressionScopeID;
			}
			set
			{
				this.m_sortExpressionScopeID = value;
			}
		}

		public IntList GroupInSortTargetIDs
		{
			get
			{
				return this.m_groupInSortTargetIDs;
			}
			set
			{
				this.m_groupInSortTargetIDs = value;
			}
		}

		public int SortTargetID
		{
			get
			{
				return this.m_sortTargetID;
			}
			set
			{
				this.m_sortTargetID = value;
			}
		}

		public string SortExpressionScopeString
		{
			get
			{
				return this.m_sortExpressionScopeString;
			}
			set
			{
				this.m_sortExpressionScopeString = value;
			}
		}

		public string SortTargetString
		{
			get
			{
				return this.m_sortTargetString;
			}
			set
			{
				this.m_sortTargetString = value;
			}
		}

		public bool FoundSortExpressionScope
		{
			get
			{
				return this.m_foundSortExpressionScope;
			}
			set
			{
				this.m_foundSortExpressionScope = value;
			}
		}

		public void SetSortTarget(ISortFilterScope target)
		{
			Global.Tracer.Assert(null != target);
			this.m_sortTarget = target;
			if (target.UserSortExpressions == null)
			{
				target.UserSortExpressions = new ExpressionInfoList();
			}
			this.m_sortExpressionIndex = target.UserSortExpressions.Count;
			target.UserSortExpressions.Add(this.m_sortExpression);
		}

		public static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.DataSetID, Token.Int32));
			memberInfoList.Add(new MemberInfo(MemberName.SortExpressionScope, Token.Reference, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ISortFilterScope));
			memberInfoList.Add(new MemberInfo(MemberName.GroupsInSortTarget, Token.Reference, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.GroupingList));
			memberInfoList.Add(new MemberInfo(MemberName.SortTarget, Token.Reference, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.ISortFilterScope));
			memberInfoList.Add(new MemberInfo(MemberName.SortExpressionIndex, Token.Int32));
			memberInfoList.Add(new MemberInfo(MemberName.DetailScopeSubReports, Token.Reference, AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.SubReportList));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.None, memberInfoList);
		}
	}
}
