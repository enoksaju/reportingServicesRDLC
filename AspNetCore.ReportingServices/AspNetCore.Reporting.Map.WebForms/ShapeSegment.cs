namespace AspNetCore.Reporting.Map.WebForms
{
	public struct ShapeSegment
	{
		public SegmentType Type;

		public int Length;

		public MapPoint MinimumExtent;

		public MapPoint MaximumExtent;

		public double PolygonSignedArea;

		public MapPoint PolygonCentroid;
	}
}
