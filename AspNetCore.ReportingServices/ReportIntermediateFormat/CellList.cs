using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	[Serializable]
	public class CellList : ArrayList
	{
		public new Cell this[int index]
		{
			get
			{
				return (Cell)base[index];
			}
		}

		public CellList()
		{
		}

		public CellList(int capacity)
			: base(capacity)
		{
		}
	}
}
