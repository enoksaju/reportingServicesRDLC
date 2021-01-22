namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class PageBreakHelper
	{
		public static PageBreakLocation GetPageBreakLocation(bool pageBreakAtStart, bool pageBreakAtEnd)
		{
			if (pageBreakAtStart && pageBreakAtEnd)
			{
				return PageBreakLocation.StartAndEnd;
			}
			if (pageBreakAtStart)
			{
				return PageBreakLocation.Start;
			}
			if (pageBreakAtEnd)
			{
				return PageBreakLocation.End;
			}
			return PageBreakLocation.None;
		}

		public static PageBreakLocation MergePageBreakLocations(PageBreakLocation outer, PageBreakLocation inner)
		{
			if (outer == inner)
			{
				return outer;
			}
			if (PageBreakLocation.StartAndEnd != outer && inner != 0)
			{
				if (PageBreakLocation.StartAndEnd != inner && outer != 0)
				{
					if (PageBreakLocation.End == outer && PageBreakLocation.Start == inner)
					{
						return PageBreakLocation.StartAndEnd;
					}
					if (PageBreakLocation.Start == outer && PageBreakLocation.End == inner)
					{
						return PageBreakLocation.StartAndEnd;
					}
					return PageBreakLocation.None;
				}
				return inner;
			}
			return outer;
		}

		public static bool HasPageBreakAtStart(PageBreakLocation pageBreakLoc)
		{
			if (pageBreakLoc != PageBreakLocation.Start)
			{
				return pageBreakLoc == PageBreakLocation.StartAndEnd;
			}
			return true;
		}

		public static bool HasPageBreakAtEnd(PageBreakLocation pageBreakLoc)
		{
			if (pageBreakLoc != PageBreakLocation.End)
			{
				return pageBreakLoc == PageBreakLocation.StartAndEnd;
			}
			return true;
		}
	}
}
