using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class BookmarkInformation
	{
		private string m_id;

		private int m_page;

		public string Id
		{
			get
			{
				return this.m_id;
			}
			set
			{
				this.m_id = value;
			}
		}

		public int Page
		{
			get
			{
				return this.m_page;
			}
			set
			{
				this.m_page = value;
			}
		}

		public BookmarkInformation()
		{
		}

		public BookmarkInformation(string id, int page)
		{
			this.m_id = id;
			this.m_page = page;
		}

		public static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.Id, Token.String));
			memberInfoList.Add(new MemberInfo(MemberName.Page, Token.Int32));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.None, memberInfoList);
		}
	}
}
