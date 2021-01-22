using System;
using System.Collections;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	[Serializable]
	public sealed class InScopeSortFilterHashtable : Hashtable
	{
		public List<int> this[int index]
		{
			get
			{
				return (List<int>)base[index];
			}
		}

		public InScopeSortFilterHashtable()
		{
		}

		public InScopeSortFilterHashtable(int capacity)
			: base(capacity)
		{
		}
	}
}
