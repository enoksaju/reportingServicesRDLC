using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public abstract class HashtableInstanceInfo : InstanceInfo
	{
		protected Hashtable m_hashtable;

		public int Count
		{
			get
			{
				return this.m_hashtable.Count;
			}
		}

		protected HashtableInstanceInfo()
		{
			this.m_hashtable = new Hashtable();
		}

		protected HashtableInstanceInfo(int capacity)
		{
			this.m_hashtable = new Hashtable(capacity);
		}

		public bool ContainsKey(int key)
		{
			return this.m_hashtable.ContainsKey(key);
		}

		public IDictionaryEnumerator GetEnumerator()
		{
			return this.m_hashtable.GetEnumerator();
		}
	}
}
