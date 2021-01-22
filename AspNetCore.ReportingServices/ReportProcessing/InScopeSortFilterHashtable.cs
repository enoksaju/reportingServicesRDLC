using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class InScopeSortFilterHashtable : Hashtable
	{
		public IntList this[int index]
		{
			get
			{
				return (IntList)base[index];
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
