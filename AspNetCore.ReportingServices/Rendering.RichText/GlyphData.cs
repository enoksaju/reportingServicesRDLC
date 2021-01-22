using System;

namespace AspNetCore.ReportingServices.Rendering.RichText
{
	public sealed class GlyphData
	{
		public GlyphShapeData GlyphScriptShapeData;

		private int[] m_advances;

		private GOFFSET[] m_gOffsets;

		public ABC ABC;

		private float m_scaleFactor = 1f;

		private bool m_needGlyphPlaceData;

		public bool NeedGlyphPlaceData
		{
			get
			{
				return this.m_needGlyphPlaceData;
			}
			set
			{
				this.m_needGlyphPlaceData = value;
			}
		}

		public float ScaleFactor
		{
			set
			{
				this.m_scaleFactor = value;
			}
		}

		public int[] RawAdvances
		{
			get
			{
				return this.m_advances;
			}
		}

		public int[] Advances
		{
			get
			{
				if (this.m_scaleFactor != 1.0)
				{
					int[] array = new int[this.m_advances.Length];
					for (int i = 0; i < this.m_advances.Length; i++)
					{
						array[i] = this.Scale(this.m_advances[i]);
					}
					return array;
				}
				return this.m_advances;
			}
		}

		public int[] ScaledAdvances
		{
			get
			{
				return this.m_advances;
			}
		}

		public GOFFSET[] RawGOffsets
		{
			get
			{
				return this.m_gOffsets;
			}
		}

		public GOFFSET[] GOffsets
		{
			get
			{
				if (this.m_scaleFactor != 1.0)
				{
					GOFFSET[] array = new GOFFSET[this.m_gOffsets.Length];
					for (int i = 0; i < this.m_gOffsets.Length; i++)
					{
						array[i] = default(GOFFSET);
						array[i].du = this.Scale(this.m_gOffsets[i].du);
						array[i].dv = this.Scale(this.m_gOffsets[i].dv);
					}
					return array;
				}
				return this.m_gOffsets;
			}
		}

		public GOFFSET[] ScaledGOffsets
		{
			get
			{
				return this.m_gOffsets;
			}
		}

		public int ScaledTotalWidth
		{
			get
			{
				return this.ABC.abcA + (int)this.ABC.abcB + this.ABC.abcC;
			}
		}

		public int ScaledTotalWidthAtLineEnd
		{
			get
			{
				return this.ABC.abcA + (int)this.ABC.abcB + Math.Abs(this.ABC.abcC);
			}
		}

		public bool Scaled
		{
			get
			{
				return this.m_scaleFactor != 1.0;
			}
		}

		public GlyphData(int maxglyphs, int numChars)
		{
			this.GlyphScriptShapeData = new GlyphShapeData(maxglyphs, numChars);
		}

		public GlyphData(GlyphShapeData glyphInfo)
		{
			this.m_needGlyphPlaceData = true;
			this.GlyphScriptShapeData = glyphInfo;
			this.m_advances = new int[glyphInfo.GlyphCount];
			this.m_gOffsets = new GOFFSET[glyphInfo.GlyphCount];
			this.ABC = default(ABC);
		}

		public int GetTotalWidth(bool isAtLineEnd)
		{
			int num = isAtLineEnd ? this.ScaledTotalWidthAtLineEnd : this.ScaledTotalWidth;
			if (this.m_scaleFactor != 1.0)
			{
				return this.Scale(num);
			}
			return num;
		}

		public int Scale(int value)
		{
			return (int)((float)value / this.m_scaleFactor + 0.5);
		}

		public void TrimToGlyphCount()
		{
			this.GlyphScriptShapeData.TrimToGlyphCount();
			this.m_advances = new int[this.GlyphScriptShapeData.GlyphCount];
			this.m_gOffsets = new GOFFSET[this.GlyphScriptShapeData.GlyphCount];
			this.ABC = default(ABC);
		}
	}
}
