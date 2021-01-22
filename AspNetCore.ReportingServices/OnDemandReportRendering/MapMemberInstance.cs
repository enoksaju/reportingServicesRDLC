namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public class MapMemberInstance : BaseInstance
	{
		protected MapDataRegion m_owner;

		protected MapMember m_memberDef;

		public MapMemberInstance(MapDataRegion owner, MapMember memberDef)
			: base(memberDef.ReportScope)
		{
			this.m_owner = owner;
			this.m_memberDef = memberDef;
		}

		protected override void ResetInstanceCache()
		{
		}
	}
}
