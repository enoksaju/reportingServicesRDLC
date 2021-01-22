using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ParameterValueList : ArrayList
	{
		public new ParameterValue this[int index]
		{
			get
			{
				return (ParameterValue)base[index];
			}
		}

		public ParameterValueList()
		{
		}

		public ParameterValueList(int capacity)
			: base(capacity)
		{
		}
	}
}
