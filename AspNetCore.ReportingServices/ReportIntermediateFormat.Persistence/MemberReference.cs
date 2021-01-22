namespace AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence
{
	public class MemberReference
	{
		private MemberName m_memberName;

		private int m_refID;

		public MemberName MemberName
		{
			get
			{
				return this.m_memberName;
			}
		}

		public int RefID
		{
			get
			{
				return this.m_refID;
			}
		}

		public MemberReference(MemberName memberName, int refID)
		{
			this.m_memberName = memberName;
			this.m_refID = refID;
		}
	}
}
