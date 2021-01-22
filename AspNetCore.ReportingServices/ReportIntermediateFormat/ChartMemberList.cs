using System;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	[Serializable]
	public sealed class ChartMemberList : HierarchyNodeList
	{
		public new ChartMember this[int index]
		{
			get
			{
				return (ChartMember)base[index];
			}
		}

		public ChartMemberList()
		{
		}

		public ChartMemberList(int capacity)
			: base(capacity)
		{
		}
	}
}
