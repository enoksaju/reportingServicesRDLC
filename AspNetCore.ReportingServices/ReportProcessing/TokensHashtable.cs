using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class TokensHashtable : HashtableInstanceInfo
	{
		public object this[int key]
		{
			get
			{
				return base.m_hashtable[key];
			}
			set
			{
				base.m_hashtable[key] = value;
			}
		}

		public TokensHashtable()
		{
		}

		public TokensHashtable(int capacity)
			: base(capacity)
		{
		}

		public void Add(int tokenID, object tokenValue)
		{
			base.m_hashtable.Add(tokenID, tokenValue);
		}
	}
}
