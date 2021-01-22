using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ImageMapAreaInstanceList : ArrayList
	{
		private int m_uniqueName;

		public new ImageMapAreaInstance this[int index]
		{
			get
			{
				return (ImageMapAreaInstance)base[index];
			}
		}

		public int UniqueName
		{
			get
			{
				return this.m_uniqueName;
			}
			set
			{
				this.m_uniqueName = value;
			}
		}

		public ImageMapAreaInstanceList()
		{
		}

		public ImageMapAreaInstanceList(int capacity)
			: base(capacity)
		{
		}

		public static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			memberInfoList.Add(new MemberInfo(MemberName.UniqueName, Token.Int32));
			return new Declaration(AspNetCore.ReportingServices.ReportProcessing.Persistence.ObjectType.None, memberInfoList);
		}
	}
}
