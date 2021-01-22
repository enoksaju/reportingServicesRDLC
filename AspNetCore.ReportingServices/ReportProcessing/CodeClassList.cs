using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class CodeClassList : ArrayList
	{
		public new CodeClass this[int index]
		{
			get
			{
				return (CodeClass)base[index];
			}
		}

		public CodeClassList()
		{
		}

		public CodeClassList(int capacity)
			: base(capacity)
		{
		}
	}
}
