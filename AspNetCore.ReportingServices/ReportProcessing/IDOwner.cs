using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public abstract class IDOwner
	{
		protected int m_ID;

		public int ID
		{
			get
			{
				return this.m_ID;
			}
			set
			{
				this.m_ID = value;
			}
		}

		protected IDOwner()
		{
		}

		protected IDOwner(int id)
		{
			this.m_ID = id;
		}

		public static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.ID, Token.Int32));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.None, memberInfoList);
		}
	}
}
