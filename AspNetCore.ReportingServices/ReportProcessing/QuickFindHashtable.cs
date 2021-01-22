using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	[HashtableOfReferences]
	public sealed class QuickFindHashtable : HashtableInstanceInfo
	{
		public ReportItemInstance this[int key]
		{
			get
			{
				return (ReportItemInstance)base.m_hashtable[key];
			}
		}

		public QuickFindHashtable()
		{
		}

		public QuickFindHashtable(int capacity)
			: base(capacity)
		{
		}

		public void Add(int key, ReportItemInstance val)
		{
			base.m_hashtable.Add(key, val);
		}
	}
}
