using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ReceiverInformationHashtable : HashtableInstanceInfo
	{
		public ReceiverInformation this[int key]
		{
			get
			{
				return (ReceiverInformation)base.m_hashtable[key];
			}
			set
			{
				base.m_hashtable[key] = value;
			}
		}

		public ReceiverInformationHashtable()
		{
		}

		public ReceiverInformationHashtable(int capacity)
			: base(capacity)
		{
		}

		public void Add(int key, ReceiverInformation receiver)
		{
			base.m_hashtable.Add(key, receiver);
		}
	}
}
