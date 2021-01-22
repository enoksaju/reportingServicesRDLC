namespace AspNetCore.Reporting.Map.WebForms
{
	public struct PathSegment
	{
		public SegmentType Type;

		public int Length;

		public MapPoint MinimumExtent;

		public MapPoint MaximumExtent;

		public double SegmentLength;
	}
}
