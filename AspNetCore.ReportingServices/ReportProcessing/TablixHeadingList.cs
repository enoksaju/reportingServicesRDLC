using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public abstract class TablixHeadingList : ArrayList
	{
		public new TablixHeading this[int index]
		{
			get
			{
				return (TablixHeading)base[index];
			}
		}

		public TablixHeadingList()
		{
		}

		public TablixHeadingList(int capacity)
			: base(capacity)
		{
		}

		public abstract TablixHeadingList InnerHeadings();
	}
}
