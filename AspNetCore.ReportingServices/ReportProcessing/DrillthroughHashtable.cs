using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class DrillthroughHashtable : HashtableInstanceInfo
	{
		public DrillthroughInformation this[string key]
		{
			get
			{
				return (DrillthroughInformation)base.m_hashtable[key];
			}
			set
			{
				base.m_hashtable[key] = value;
			}
		}

		public DrillthroughHashtable()
		{
		}

		public DrillthroughHashtable(int capacity)
			: base(capacity)
		{
		}

		public void Add(string drillthroughId, DrillthroughInformation drillthroughInfo)
		{
			base.m_hashtable.Add(drillthroughId, drillthroughInfo);
		}
	}
}
