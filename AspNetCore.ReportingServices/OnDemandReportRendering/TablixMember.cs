using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class TablixMember : DataRegionMember
	{
		protected TablixMemberCollection m_children;

		protected Visibility m_visibility;

		protected TablixMemberInstance m_instance;

		protected TablixHeader m_header;

		public TablixMember Parent
		{
			get
			{
				return base.m_parent as TablixMember;
			}
		}

		public abstract string DataElementName
		{
			get;
		}

		public abstract DataElementOutputTypes DataElementOutput
		{
			get;
		}

		public abstract TablixHeader TablixHeader
		{
			get;
		}

		public abstract TablixMemberCollection Children
		{
			get;
		}

		public abstract bool FixedData
		{
			get;
		}

		public abstract KeepWithGroup KeepWithGroup
		{
			get;
		}

		public abstract bool RepeatOnNewPage
		{
			get;
		}

		public virtual bool KeepTogether
		{
			get
			{
				return false;
			}
		}

		public abstract bool IsColumn
		{
			get;
		}

		public abstract int RowSpan
		{
			get;
		}

		public abstract int ColSpan
		{
			get;
		}

		public abstract bool IsTotal
		{
			get;
		}

		public abstract PageBreakLocation PropagatedGroupBreak
		{
			get;
		}

		public abstract Visibility Visibility
		{
			get;
		}

		public abstract bool HideIfNoRows
		{
			get;
		}

		public abstract AspNetCore.ReportingServices.ReportIntermediateFormat.TablixMember MemberDefinition
		{
			get;
		}

		public override ReportHierarchyNode DataRegionMemberDefinition
		{
			get
			{
				return this.MemberDefinition;
			}
		}

		public Tablix OwnerTablix
		{
			get
			{
				return base.m_owner as Tablix;
			}
		}

		public abstract TablixMemberInstance Instance
		{
			get;
		}

		public override IDataRegionMemberCollection SubMembers
		{
			get
			{
				return this.m_children;
			}
		}

		public TablixMember(IDefinitionPath parentDefinitionPath, Tablix owner, TablixMember parent, int parentCollectionIndex)
			: base(parentDefinitionPath, owner, parent, parentCollectionIndex)
		{
		}

		public override bool GetIsColumn()
		{
			return this.IsColumn;
		}

		public override void SetNewContext(bool fromMoveNext)
		{
			base.SetNewContext(fromMoveNext);
			if (this.m_header != null)
			{
				this.m_header.SetNewContext();
			}
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			AspNetCore.ReportingServices.ReportIntermediateFormat.TablixMember memberDefinition = this.MemberDefinition;
			if (memberDefinition != null)
			{
				memberDefinition.ResetVisibilityComputationCache();
			}
		}
	}
}
