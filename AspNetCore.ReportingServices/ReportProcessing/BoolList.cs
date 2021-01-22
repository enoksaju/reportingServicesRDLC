using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class BoolList : ArrayList
	{
		public new bool this[int index]
		{
			get
			{
				return (bool)base[index];
			}
			set
			{
				base[index] = value;
			}
		}

		public BoolList()
		{
		}

		public BoolList(int capacity)
			: base(capacity)
		{
		}
	}
}
