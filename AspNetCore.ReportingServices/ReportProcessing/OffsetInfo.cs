using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class OffsetInfo : InfoBase
	{
		private long m_offset;

		public long Offset
		{
			get
			{
				return this.m_offset;
			}
			set
			{
				this.m_offset = value;
			}
		}

		public OffsetInfo()
		{
		}

		public OffsetInfo(long offset)
		{
			this.m_offset = offset;
		}

		public new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.Offset, Token.Int64));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.InfoBase, memberInfoList);
		}
	}
}
