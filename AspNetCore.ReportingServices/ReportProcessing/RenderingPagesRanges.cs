using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public struct RenderingPagesRanges
	{
		private int m_startPage;

		private int m_endPage;

		public int StartPage
		{
			get
			{
				return this.m_startPage;
			}
			set
			{
				this.m_startPage = value;
			}
		}

		public int StartRow
		{
			get
			{
				return this.m_startPage;
			}
			set
			{
				this.m_startPage = value;
			}
		}

		public int EndPage
		{
			get
			{
				return this.m_endPage;
			}
			set
			{
				this.m_endPage = value;
			}
		}

		public int NumberOfDetails
		{
			get
			{
				return this.m_endPage;
			}
			set
			{
				this.m_endPage = value;
			}
		}

		public static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.StartPage, Token.Int32));
			memberInfoList.Add(new MemberInfo(MemberName.EndPage, Token.Int32));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.None, memberInfoList);
		}
	}
}
