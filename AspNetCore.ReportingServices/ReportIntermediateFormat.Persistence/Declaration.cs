using System.Collections.Generic;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence
{
	public class Declaration
	{
		private List<MemberInfo> m_memberInfoList = new List<MemberInfo>();

		private ObjectType m_type;

		private ObjectType m_baseType;

		private Pair<bool, int>[] m_usableMembers;

		private bool m_hasSkippedMembers;

		public List<MemberInfo> MemberInfoList
		{
			get
			{
				return this.m_memberInfoList;
			}
		}

		public ObjectType ObjectType
		{
			get
			{
				return this.m_type;
			}
		}

		public ObjectType BaseObjectType
		{
			get
			{
				return this.m_baseType;
			}
		}

		public bool RegisteredCurrentDeclaration
		{
			get
			{
				return this.m_usableMembers != null;
			}
		}

		public bool HasSkippedMembers
		{
			get
			{
				return this.m_hasSkippedMembers;
			}
		}

		public Declaration(ObjectType type, ObjectType baseType, List<MemberInfo> memberInfoList)
		{
			this.m_type = type;
			this.m_baseType = baseType;
			this.m_memberInfoList = memberInfoList;
		}

		public bool IsMemberSkipped(int index)
		{
			if (this.m_hasSkippedMembers)
			{
				return this.m_usableMembers[index].First;
			}
			return false;
		}

		public int MembersToSkip(int index)
		{
			if (this.m_hasSkippedMembers)
			{
				return this.m_usableMembers[index].Second;
			}
			return 0;
		}

		public void RegisterCurrentDeclaration(Declaration currentDeclaration)
		{
			this.m_hasSkippedMembers = false;
			this.m_usableMembers = new Pair<bool, int>[this.m_memberInfoList.Count];
			int num = 0;
			for (int num2 = this.m_memberInfoList.Count - 1; num2 >= 0; num2--)
			{
				if (currentDeclaration.Contains(this.m_memberInfoList[num2]))
				{
					num = 0;
				}
				else
				{
					this.m_hasSkippedMembers = true;
					num++;
					this.m_usableMembers[num2].Second = num;
					this.m_usableMembers[num2].First = true;
				}
			}
			if (!this.m_hasSkippedMembers)
			{
				this.m_usableMembers = new Pair<bool, int>[0];
			}
		}

		private bool Contains(MemberInfo otherMember)
		{
			return this.m_memberInfoList.Contains(otherMember);
		}

		public Declaration CreateFilteredDeclarationForWriteVersion(int compatVersion)
		{
			List<MemberInfo> list = new List<MemberInfo>(this.m_memberInfoList.Count);
			for (int i = 0; i < this.m_memberInfoList.Count; i++)
			{
				MemberInfo memberInfo = this.m_memberInfoList[i];
				if (memberInfo.IsWrittenForCompatVersion(compatVersion))
				{
					list.Add(memberInfo);
				}
			}
			return new Declaration(this.m_type, this.m_baseType, list);
		}
	}
}
