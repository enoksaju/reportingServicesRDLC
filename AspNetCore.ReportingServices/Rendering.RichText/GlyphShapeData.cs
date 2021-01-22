using System;

namespace AspNetCore.ReportingServices.Rendering.RichText
{
	public sealed class GlyphShapeData
	{
		public int GlyphCount;

		public short[] Glyphs;

		public short[] Clusters;

		public SCRIPT_VISATTR[] VisAttrs;

		public GlyphShapeData(int maxglyphs, int numChars)
		{
			this.Glyphs = new short[maxglyphs];
			this.Clusters = new short[numChars];
			this.VisAttrs = new SCRIPT_VISATTR[maxglyphs];
		}

		public void TrimToGlyphCount()
		{
			if (this.GlyphCount < this.Glyphs.Length)
			{
				Array.Resize<short>(ref this.Glyphs, this.GlyphCount);
				Array.Resize<SCRIPT_VISATTR>(ref this.VisAttrs, this.GlyphCount);
			}
		}
	}
}
