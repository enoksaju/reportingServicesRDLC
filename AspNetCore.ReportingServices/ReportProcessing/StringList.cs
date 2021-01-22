using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class StringList : ArrayList
	{
		public new string this[int index]
		{
			get
			{
				return (string)base[index];
			}
			set
			{
				base[index] = value;
			}
		}

		public StringList()
		{
		}

		public StringList(int capacity)
			: base(capacity)
		{
		}
	}
}
