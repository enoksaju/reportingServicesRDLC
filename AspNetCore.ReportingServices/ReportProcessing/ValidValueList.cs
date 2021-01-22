using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ValidValueList : ArrayList
	{
		public new ValidValue this[int index]
		{
			get
			{
				return (ValidValue)base[index];
			}
		}

		public ValidValueList()
		{
		}

		public ValidValueList(int capacity)
			: base(capacity)
		{
		}
	}
}
