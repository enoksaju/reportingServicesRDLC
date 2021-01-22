using System;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public sealed class SkipMemberStaticValidationAttribute : Attribute
	{
		private MemberName m_member;

		public MemberName Member
		{
			get
			{
				return this.m_member;
			}
		}

		public SkipMemberStaticValidationAttribute(MemberName member)
		{
			this.m_member = member;
		}
	}
}
