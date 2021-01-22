using System;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	[Serializable]
	public sealed class DataMemberList : HierarchyNodeList
	{
		public new DataMember this[int index]
		{
			get
			{
				return (DataMember)base[index];
			}
		}

		public DataMemberList()
		{
		}

		public DataMemberList(int capacity)
			: base(capacity)
		{
		}
	}
}
