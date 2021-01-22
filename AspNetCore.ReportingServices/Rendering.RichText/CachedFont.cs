using System;
using System.Drawing;

namespace AspNetCore.ReportingServices.Rendering.RichText
{
	public sealed class CachedFont : IDisposable
	{
		private Win32ObjectSafeHandle m_hfont = Win32ObjectSafeHandle.Zero;

		private Font m_font;

		private Win32.TEXTMETRIC m_textMetric;

		private bool m_initialized;

		private float m_scaleFactor = 1f;

		public ScriptCacheSafeHandle ScriptCache = new ScriptCacheSafeHandle();

		private short? m_defaultGlyph = null;

		public Win32ObjectSafeHandle Hfont
		{
			get
			{
				return this.m_hfont;
			}
			set
			{
				this.m_hfont = value;
			}
		}

		public Font Font
		{
			get
			{
				return this.m_font;
			}
			set
			{
				this.m_font = value;
			}
		}

		public float ScaleFactor
		{
			get
			{
				return this.m_scaleFactor;
			}
			set
			{
				this.m_scaleFactor = value;
			}
		}

		public Win32.TEXTMETRIC TextMetric
		{
			get
			{
				return this.m_textMetric;
			}
		}

		public short? DefaultGlyph
		{
			get
			{
				return this.m_defaultGlyph;
			}
			set
			{
				this.m_defaultGlyph = value;
			}
		}

		public CachedFont()
		{
		}

		private void Dispose(bool disposing)
		{
			if (disposing && this.m_font != null)
			{
				this.m_font.Dispose();
				this.m_font = null;
			}
			if (this.m_hfont != null && !this.m_hfont.IsInvalid)
			{
				this.m_hfont.Close();
				this.m_hfont = null;
			}
			this.ScriptCache.Close();
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		~CachedFont()
		{
			this.Dispose(false);
		}

		private void Initialize(Win32DCSafeHandle hdc, FontCache fontCache)
		{
			fontCache.SelectFontObject(hdc, this.m_hfont);
			Win32.GetTextMetrics(hdc, out this.m_textMetric);
			if (this.ScaleFactor != 1.0)
			{
				this.m_textMetric.tmHeight = this.Scale(this.m_textMetric.tmHeight);
				this.m_textMetric.tmAscent = this.Scale(this.m_textMetric.tmAscent);
				this.m_textMetric.tmDescent = this.Scale(this.m_textMetric.tmDescent);
				this.m_textMetric.tminternalLeading = this.Scale(this.m_textMetric.tminternalLeading);
			}
			this.m_initialized = true;
		}

		private int Scale(int value)
		{
			return (int)((float)value / this.ScaleFactor + 0.5);
		}

		public int GetHeight(Win32DCSafeHandle hdc, FontCache fontCache)
		{
			if (!this.m_initialized)
			{
				this.Initialize(hdc, fontCache);
			}
			return this.m_textMetric.tmHeight;
		}

		public int GetAscent(Win32DCSafeHandle hdc, FontCache fontCache)
		{
			if (!this.m_initialized)
			{
				this.Initialize(hdc, fontCache);
			}
			return this.m_textMetric.tmAscent;
		}

		public int GetDescent(Win32DCSafeHandle hdc, FontCache fontCache)
		{
			if (!this.m_initialized)
			{
				this.Initialize(hdc, fontCache);
			}
			return this.m_textMetric.tmDescent;
		}

		public int GetLeading(Win32DCSafeHandle hdc, FontCache fontCache)
		{
			if (!this.m_initialized)
			{
				this.Initialize(hdc, fontCache);
			}
			return this.m_textMetric.tminternalLeading;
		}
	}
}
