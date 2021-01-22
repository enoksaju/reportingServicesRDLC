namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class ShimMemberCollection : TablixMemberCollection
	{
		protected bool m_isColumnGroup;

		public ShimMemberCollection(IDefinitionPath parentDefinitionPath, Tablix owner, bool isColumnGroup)
			: base(parentDefinitionPath, owner)
		{
			this.m_isColumnGroup = isColumnGroup;
		}
	}
}
