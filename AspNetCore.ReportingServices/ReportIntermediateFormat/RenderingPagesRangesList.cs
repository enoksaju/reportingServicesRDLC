using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	[Serializable]
	internal sealed class RenderingPagesRangesList : ArrayList
	{
		internal new RenderingPagesRanges this[int index]
		{
			get
			{
				return (RenderingPagesRanges)base[index];
			}
		}

		public RenderingPagesRangesList()
		{
		}

		internal RenderingPagesRangesList(int capacity)
			: base(capacity)
		{
		}

		internal void MoveAllToFirstPage(int totalCount)
		{
			int count = this.Count;
			if (count != 0)
			{
				if (count > 1)
				{
					base.RemoveRange(1, count - 1);
				}
				RenderingPagesRanges renderingPagesRanges = (RenderingPagesRanges)base[0];
				renderingPagesRanges.NumberOfDetails = totalCount;
				renderingPagesRanges.EndPage = renderingPagesRanges.StartPage;
			}
		}
	}
}
