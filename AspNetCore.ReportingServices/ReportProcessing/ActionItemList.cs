using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ActionItemList : ArrayList
	{
		public new ActionItem this[int index]
		{
			get
			{
				return (ActionItem)base[index];
			}
		}

		public ActionItemList()
		{
		}

		public ActionItemList(int capacity)
			: base(capacity)
		{
		}
	}
}
