using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing.Persistence
{
	public sealed class MemberInfoList : ArrayList
	{
		public new MemberInfo this[int index]
		{
			get
			{
				return (MemberInfo)base[index];
			}
		}

		public MemberInfoList()
		{
		}

		public MemberInfoList(int capacity)
			: base(capacity)
		{
		}
	}
}
