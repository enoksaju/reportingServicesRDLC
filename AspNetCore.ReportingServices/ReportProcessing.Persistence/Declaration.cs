namespace AspNetCore.ReportingServices.ReportProcessing.Persistence
{
	public sealed class Declaration
	{
		private ObjectType m_baseType;

		private MemberInfoList m_members;

		public ObjectType BaseType
		{
			get
			{
				return this.m_baseType;
			}
		}

		public MemberInfoList Members
		{
			get
			{
				return this.m_members;
			}
		}

		public Declaration(ObjectType baseType, MemberInfoList members)
		{
			this.m_baseType = baseType;
			Global.Tracer.Assert(null != members);
			this.m_members = members;
		}
	}
}
