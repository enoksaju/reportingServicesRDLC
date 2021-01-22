namespace AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence
{
	public class ReadOnlyMemberInfo : MemberInfo
	{
		public ReadOnlyMemberInfo(MemberName name, Token token)
			: base(name, token)
		{
		}

		public ReadOnlyMemberInfo(MemberName name, ObjectType type)
			: base(name, type)
		{
		}

		public ReadOnlyMemberInfo(MemberName name, ObjectType type, ObjectType containedType)
			: base(name, type, containedType)
		{
		}

		public ReadOnlyMemberInfo(MemberName name, ObjectType type, Token token)
			: base(name, type, token)
		{
		}

		public ReadOnlyMemberInfo(MemberName name, ObjectType type, Token token, ObjectType containedType)
			: base(name, type, token, containedType)
		{
		}

		public override bool IsWrittenForCompatVersion(int compatVersion)
		{
			return false;
		}
	}
}
