namespace AspNetCore.ReportingServices.ReportProcessing.Persistence
{
	public sealed class MemberInfo
	{
		private MemberName m_memberName;

		private Token m_token;

		private ObjectType m_objectType;

		public MemberName MemberName
		{
			get
			{
				return this.m_memberName;
			}
			set
			{
				this.m_memberName = value;
			}
		}

		public Token Token
		{
			get
			{
				return this.m_token;
			}
		}

		public ObjectType ObjectType
		{
			get
			{
				return this.m_objectType;
			}
		}

		public MemberInfo(MemberName memberName, Token token)
		{
			this.m_memberName = memberName;
			this.m_token = token;
			this.m_objectType = ObjectType.None;
		}

		public MemberInfo(MemberName memberName, ObjectType objectType)
		{
			this.m_memberName = memberName;
			this.m_token = Token.Object;
			this.m_objectType = objectType;
		}

		public MemberInfo(MemberName memberName, Token token, ObjectType objectType)
		{
			this.m_memberName = memberName;
			this.m_token = token;
			this.m_objectType = objectType;
		}

		public static bool Equals(MemberInfo a, MemberInfo b)
		{
			if (a != null && b != null)
			{
				if (a.MemberName == b.MemberName && a.Token == b.Token)
				{
					return a.ObjectType == b.ObjectType;
				}
				return false;
			}
			return false;
		}
	}
}
