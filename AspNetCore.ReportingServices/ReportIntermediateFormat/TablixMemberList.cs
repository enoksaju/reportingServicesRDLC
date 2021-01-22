using System;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	[Serializable]
	public sealed class TablixMemberList : HierarchyNodeList
	{
		public new TablixMember this[int index]
		{
			get
			{
				return (TablixMember)base[index];
			}
		}

		public TablixMemberList()
		{
		}

		public TablixMemberList(int capacity)
			: base(capacity)
		{
		}
	}
}
