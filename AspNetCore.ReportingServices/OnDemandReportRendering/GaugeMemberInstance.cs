namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public class GaugeMemberInstance : BaseInstance
	{
		protected GaugePanel m_owner;

		protected GaugeMember m_memberDef;

		public GaugeMemberInstance(GaugePanel owner, GaugeMember memberDef)
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
