using AspNetCore.ReportingServices.ReportProcessing;
using AspNetCore.ReportingServices.ReportRendering;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class ShimListMemberCollection : ShimMemberCollection
	{
		private ShimRenderGroups m_renderGroups;

		public override TablixMember this[int index]
		{
			get
			{
				if (index >= 0 && index < this.Count)
				{
					if (base.m_children == null)
					{
						base.m_children = new DataRegionMember[this.Count];
					}
					TablixMember tablixMember = (TablixMember)base.m_children[index];
					if (tablixMember == null)
					{
						tablixMember = (TablixMember)(base.m_children[index] = new ShimListMember(this, base.OwnerTablix, this.m_renderGroups, index, base.m_isColumnGroup));
					}
					return tablixMember;
				}
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, index, 0, this.Count);
			}
		}

		public override int Count
		{
			get
			{
				return 1;
			}
		}

		public ShimListMemberCollection(IDefinitionPath parentDefinitionPath, Tablix owner)
			: base(parentDefinitionPath, owner, true)
		{
		}

		public ShimListMemberCollection(IDefinitionPath parentDefinitionPath, Tablix owner, ListContentCollection renderListContents)
			: base(parentDefinitionPath, owner, false)
		{
			this.m_renderGroups = new ShimRenderGroups(renderListContents);
		}

		public void UpdateContext(ListContentCollection renderListContents)
		{
			this.m_renderGroups = new ShimRenderGroups(renderListContents);
			if (base.m_children != null)
			{
				((ShimListMember)base.m_children[0]).ResetContext(this.m_renderGroups);
			}
		}
	}
}
