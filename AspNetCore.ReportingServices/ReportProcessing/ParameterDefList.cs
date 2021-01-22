using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ParameterDefList : ArrayList
	{
		public new ParameterDef this[int index]
		{
			get
			{
				return (ParameterDef)base[index];
			}
		}

		public ParameterDefList()
		{
		}

		public ParameterDefList(int capacity)
			: base(capacity)
		{
		}
	}
}
