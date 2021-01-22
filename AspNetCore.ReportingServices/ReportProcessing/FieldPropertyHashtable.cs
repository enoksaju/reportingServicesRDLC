using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class FieldPropertyHashtable
	{
		private Hashtable m_hashtable;

		public int Count
		{
			get
			{
				return this.m_hashtable.Count;
			}
		}

		public FieldPropertyHashtable()
		{
			this.m_hashtable = new Hashtable();
		}

		public FieldPropertyHashtable(int capacity)
		{
			this.m_hashtable = new Hashtable(capacity);
		}

		public void Add(string key)
		{
			this.m_hashtable.Add(key, null);
		}

		public bool ContainsKey(string key)
		{
			return this.m_hashtable.ContainsKey(key);
		}

		public IDictionaryEnumerator GetEnumerator()
		{
			return this.m_hashtable.GetEnumerator();
		}
	}
}
