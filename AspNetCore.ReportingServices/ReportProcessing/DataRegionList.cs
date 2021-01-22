using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	[ArrayOfReferences]
	public sealed class DataRegionList : ArrayList
	{
		public new DataRegion this[int index]
		{
			get
			{
				return (DataRegion)base[index];
			}
		}

		public DataRegionList()
		{
		}

		public DataRegionList(int capacity)
			: base(capacity)
		{
		}
	}
}
