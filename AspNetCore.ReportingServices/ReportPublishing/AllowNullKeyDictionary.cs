using System.Collections.Generic;

namespace AspNetCore.ReportingServices.ReportPublishing
{
	public sealed class AllowNullKeyDictionary<TKey, TValue> where TKey : class where TValue : class
	{
		private Dictionary<TKey, TValue> m_hashtable = new Dictionary<TKey, TValue>();

		private TValue m_valueForNullKey = null;

		public TValue this[TKey key]
		{
			get
			{
				TValue result = null;
				this.TryGetValue(key, out result);
				return result;
			}
			set
			{
				this.Add(key, value);
			}
		}

		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				this.m_valueForNullKey = value;
			}
			else
			{
				this.m_hashtable.Add(key, value);
			}
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			value = null;
			if (key == null)
			{
				if (this.m_valueForNullKey == null)
				{
					return false;
				}
				value = this.m_valueForNullKey;
				return true;
			}
			return this.m_hashtable.TryGetValue(key, out value);
		}
	}
}
