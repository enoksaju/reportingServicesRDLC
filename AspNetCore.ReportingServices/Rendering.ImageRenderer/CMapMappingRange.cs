namespace AspNetCore.ReportingServices.Rendering.ImageRenderer
{
	public sealed class CMapMappingRange
	{
		public readonly CMapMapping Mapping;

		public readonly ushort Length;

		public CMapMappingRange(CMapMapping mapping, ushort length)
		{
			this.Mapping = mapping;
			this.Length = length;
		}
	}
}
