using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ActionItemInstanceList : ArrayList
	{
		public new ActionItemInstance this[int index]
		{
			get
			{
				return (ActionItemInstance)base[index];
			}
		}

		public ActionItemInstanceList()
		{
		}

		public ActionItemInstanceList(int capacity)
			: base(capacity)
		{
		}
	}
}
