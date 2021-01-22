using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class SortFilterEventInfoHashtable : HashtableInstanceInfo
	{
		public SortFilterEventInfo this[int key]
		{
			get
			{
				return (SortFilterEventInfo)base.m_hashtable[key];
			}
		}

		public SortFilterEventInfoHashtable()
		{
		}

		public SortFilterEventInfoHashtable(int capacity)
			: base(capacity)
		{
		}

		public void Add(int key, SortFilterEventInfo val)
		{
			base.m_hashtable.Add(key, val);
		}
	}
}
