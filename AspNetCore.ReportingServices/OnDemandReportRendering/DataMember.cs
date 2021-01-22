using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class DataMember : DataRegionMember
	{
		protected DataMemberCollection m_children;

		protected DataMemberInstance m_instance;

		public DataMember Parent
		{
			get
			{
				return base.m_parent as DataMember;
			}
		}

		public virtual DataMemberCollection Children
		{
			get
			{
				return this.m_children;
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

		public abstract AspNetCore.ReportingServices.ReportIntermediateFormat.DataMember MemberDefinition
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

		public CustomReportItem OwnerCri
		{
			get
			{
				return base.m_owner as CustomReportItem;
			}
		}

		public abstract DataMemberInstance Instance
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

		public DataMember(IDefinitionPath parentDefinitionPath, CustomReportItem owner, DataMember parent, int parentCollectionIndex)
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
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}
	}
}
