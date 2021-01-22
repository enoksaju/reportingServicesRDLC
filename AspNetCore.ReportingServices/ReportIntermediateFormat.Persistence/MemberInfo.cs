namespace AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence
{
	public class MemberInfo
	{
		private MemberName m_name;

		private Token m_token = Token.Object;

		private ObjectType m_type = ObjectType.None;

		private ObjectType m_containedType = ObjectType.None;

		private Lifetime m_lifetime = Lifetime.Unspecified;

		public MemberName MemberName
		{
			get
			{
				return this.m_name;
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
				return this.m_type;
			}
		}

		public ObjectType ContainedType
		{
			get
			{
				return this.m_containedType;
			}
		}

		public Lifetime Lifetime
		{
			get
			{
				return this.m_lifetime;
			}
		}

		public MemberInfo(MemberName name, Token token)
		{
			this.m_name = name;
			this.m_token = token;
		}

		public MemberInfo(MemberName name, Token token, Lifetime lifetime)
		{
			this.m_name = name;
			this.m_token = token;
			this.m_lifetime = lifetime;
		}

		public MemberInfo(MemberName name, ObjectType type)
		{
			this.m_name = name;
			this.m_type = type;
		}

		public MemberInfo(MemberName name, ObjectType type, Lifetime lifetime)
		{
			this.m_name = name;
			this.m_type = type;
			this.m_lifetime = lifetime;
		}

		public MemberInfo(MemberName name, ObjectType type, ObjectType containedType)
		{
			this.m_name = name;
			this.m_type = type;
			this.m_containedType = containedType;
		}

		public MemberInfo(MemberName name, ObjectType type, ObjectType containedType, Lifetime lifetime)
		{
			this.m_name = name;
			this.m_type = type;
			this.m_containedType = containedType;
			this.m_lifetime = lifetime;
		}

		public MemberInfo(MemberName name, ObjectType type, Token token)
		{
			this.m_name = name;
			this.m_token = token;
			this.m_type = type;
		}

		public MemberInfo(MemberName name, ObjectType type, Token token, Lifetime lifetime)
		{
			this.m_name = name;
			this.m_token = token;
			this.m_type = type;
			this.m_lifetime = lifetime;
		}

		public MemberInfo(MemberName name, ObjectType type, Token token, ObjectType containedType)
		{
			this.m_name = name;
			this.m_token = token;
			this.m_type = type;
			this.m_containedType = containedType;
		}

		public MemberInfo(MemberName name, ObjectType type, Token token, ObjectType containedType, Lifetime lifetime)
		{
			this.m_name = name;
			this.m_token = token;
			this.m_type = type;
			this.m_containedType = containedType;
			this.m_lifetime = lifetime;
		}

		public virtual bool IsWrittenForCompatVersion(int compatVersion)
		{
			return this.m_lifetime.IncludesVersion(compatVersion);
		}

		public override int GetHashCode()
		{
			return (int)this.m_name ^ (int)((uint)this.m_token << 8) ^ (int)this.m_type << 16 ^ (int)this.m_containedType << 24;
		}

		public override bool Equals(object obj)
		{
			if (obj != null && obj is MemberInfo)
			{
				return this.Equals((MemberInfo)obj);
			}
			return false;
		}

		public bool Equals(MemberInfo otherMember)
		{
			if (otherMember != null && this.m_name == otherMember.m_name && this.m_token == otherMember.m_token && this.m_type == otherMember.m_type && this.m_containedType == otherMember.m_containedType)
			{
				return true;
			}
			return false;
		}
	}
}
