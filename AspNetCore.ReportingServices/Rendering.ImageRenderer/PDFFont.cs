using AspNetCore.ReportingServices.Rendering.RichText;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace AspNetCore.ReportingServices.Rendering.ImageRenderer
{
	public sealed class PDFFont
	{
		public sealed class GlyphData : IComparable
		{
			public ushort Glyph;

			public float Width;

			public char? Character;

			public GlyphData(ushort glyph, float width)
			{
				this.Glyph = glyph;
				this.Width = width;
				this.Character = null;
			}

			int IComparable.CompareTo(object o1)
			{
				GlyphData glyphData = (GlyphData)o1;
				if (this.Glyph < glyphData.Glyph)
				{
					return -1;
				}
				if (this.Glyph > glyphData.Glyph)
				{
					return 1;
				}
				return 0;
			}
		}

		public CachedFont CachedFont;

		public readonly string FontFamily;

		public string FontPDFFamily;

		public int FontId = -1;

		public List<GlyphData> UniqueGlyphs = new List<GlyphData>();

		public string FontCMap;

		public string Registry;

		public string Ordering;

		public string Supplement;

		public readonly FontStyle GDIFontStyle;

		public readonly int EMHeight;

		public readonly float GridHeight;

		public readonly float EMGridConversion;

		public readonly bool InternalFont;

		public readonly bool SimulateItalic;

		public readonly bool SimulateBold;

		public EmbeddedFont EmbeddedFont;

		public bool IsComposite
		{
			get
			{
				return !string.IsNullOrEmpty(this.FontCMap);
			}
		}

		public PDFFont(CachedFont cachedFont, string fontFamily, string pdfFontFamily, string fontCMap, string registry, string ordering, string supplement, FontStyle gdiFontStyle, int emHeight, float gridHeight, bool internalFont, bool simulateItalic, bool simulateBold)
		{
			this.CachedFont = cachedFont;
			this.FontFamily = fontFamily;
			this.FontPDFFamily = pdfFontFamily;
			this.FontCMap = fontCMap;
			this.Registry = registry;
			this.Ordering = ordering;
			this.Supplement = supplement;
			this.GDIFontStyle = gdiFontStyle;
			this.EMHeight = emHeight;
			this.GridHeight = gridHeight;
			this.InternalFont = internalFont;
			this.EMGridConversion = (float)(1000.0 / (float)emHeight);
			this.SimulateItalic = simulateItalic;
			this.SimulateBold = simulateBold;
		}

		public GlyphData AddUniqueGlyph(ushort glyph, float width)
		{
			GlyphData glyphData = new GlyphData(glyph, width);
			if (this.UniqueGlyphs.BinarySearch(glyphData) >= 0)
			{
				return null;
			}
			int num = 0;
			while (num < this.UniqueGlyphs.Count)
			{
				if (glyphData.Glyph >= this.UniqueGlyphs[num].Glyph)
				{
					num++;
					continue;
				}
				this.UniqueGlyphs.Insert(num, glyphData);
				break;
			}
			if (num == this.UniqueGlyphs.Count)
			{
				this.UniqueGlyphs.Add(glyphData);
			}
			return glyphData;
		}
	}
}
