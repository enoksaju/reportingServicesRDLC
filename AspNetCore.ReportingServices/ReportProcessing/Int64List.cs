using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class Int64List : ArrayList
	{
		public new long this[int index]
		{
			get
			{
				return (long)base[index];
			}
			set
			{
				base[index] = value;
			}
		}

		public Int64List()
		{
		}

		public Int64List(int capacity)
			: base(capacity)
		{
		}
	}
}
