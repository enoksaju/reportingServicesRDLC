using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class VariantList : ArrayList
	{
		public VariantList()
		{
		}

		public VariantList(int capacity)
			: base(capacity)
		{
		}
	}
}
