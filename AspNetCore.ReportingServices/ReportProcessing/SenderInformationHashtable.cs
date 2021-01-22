using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class SenderInformationHashtable : HashtableInstanceInfo
	{
		public SenderInformation this[int key]
		{
			get
			{
				return (SenderInformation)base.m_hashtable[key];
			}
			set
			{
				base.m_hashtable[key] = value;
			}
		}

		public SenderInformationHashtable()
		{
		}

		public SenderInformationHashtable(int capacity)
			: base(capacity)
		{
		}

		public void Add(int key, SenderInformation sender)
		{
			base.m_hashtable.Add(key, sender);
		}
	}
}
