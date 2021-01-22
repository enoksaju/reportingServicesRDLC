using AspNetCore.ReportingServices.Rendering.RPLProcessing;

namespace AspNetCore.ReportingServices.Rendering.WordRenderer.WordOpenXmlRenderer
{
	public sealed class SizingIndependentImageHash : ImageHash
	{
		public SizingIndependentImageHash(byte[] md4)
			: base(md4, RPLFormat.Sizings.AutoSize, 0, 0)
		{
		}
	}
}
