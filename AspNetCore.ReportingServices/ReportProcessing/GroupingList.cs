using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class GroupingList : ArrayList
	{
		public new Grouping this[int index]
		{
			get
			{
				return (Grouping)base[index];
			}
		}

		public Grouping LastEntry
		{
			get
			{
				if (this.Count == 0)
				{
					return null;
				}
				return this[this.Count - 1];
			}
		}

		public GroupingList()
		{
		}

		public GroupingList(int capacity)
			: base(capacity)
		{
		}

		public new GroupingList Clone()
		{
			int count = this.Count;
			GroupingList groupingList = new GroupingList(count);
			for (int i = 0; i < count; i++)
			{
				groupingList.Add(this[i]);
			}
			return groupingList;
		}
	}
}
