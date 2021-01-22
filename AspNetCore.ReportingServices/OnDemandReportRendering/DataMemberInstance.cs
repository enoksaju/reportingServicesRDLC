namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public class DataMemberInstance : BaseInstance
	{
		protected CustomReportItem m_owner;

		protected DataMember m_memberDef;

		public DataMemberInstance(CustomReportItem owner, DataMember memberDef)
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
