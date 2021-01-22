using AspNetCore.ReportingServices.Rendering.RichText;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace AspNetCore.ReportingServices.Rendering.ImageRenderer
{
	public class GraphicsBase : IDisposable
	{
		public delegate void SynchronizedOperation();

		protected System.Drawing.Graphics m_graphicsBase;

		protected Bitmap m_imageBase;

		private int m_dpiX = 96;

		private int m_dpiY = 96;

		private Win32DCSafeHandle m_hdc = Win32DCSafeHandle.Zero;

		public int DpiX
		{
			get
			{
				return this.m_dpiX;
			}
			set
			{
				this.m_dpiX = value;
			}
		}

		public int DpiY
		{
			get
			{
				return this.m_dpiY;
			}
			set
			{
				this.m_dpiY = value;
			}
		}

		public Win32DCSafeHandle Hdc
		{
			get
			{
				return this.m_hdc;
			}
			set
			{
				this.m_hdc = value;
			}
		}

		public System.Drawing.Graphics SystemGraphics
		{
			get
			{
				return this.m_graphicsBase;
			}
		}

		public GraphicsBase(float dpiX, float dpiY)
		{
			this.m_dpiX = (int)dpiX;
			this.m_dpiY = (int)dpiY;
			this.m_imageBase = new Bitmap(2, 2);
			this.m_imageBase.SetResolution(dpiX, dpiY);
			this.m_graphicsBase = System.Drawing.Graphics.FromImage(this.m_imageBase);
			this.m_graphicsBase.CompositingMode = CompositingMode.SourceOver;
			this.m_graphicsBase.PageUnit = GraphicsUnit.Millimeter;
			this.m_graphicsBase.PixelOffsetMode = PixelOffsetMode.Default;
			this.m_graphicsBase.SmoothingMode = SmoothingMode.Default;
			this.m_graphicsBase.TextRenderingHint = TextRenderingHint.SystemDefault;
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.m_graphicsBase != null)
				{
					this.ReleaseCachedHdc(true);
					this.m_graphicsBase.Dispose();
					this.m_graphicsBase = null;
				}
				if (this.m_imageBase != null)
				{
					this.m_imageBase.Dispose();
					this.m_imageBase = null;
				}
			}
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		~GraphicsBase()
		{
			this.Dispose(false);
		}

		public virtual void CacheHdc(bool createNewHdc)
		{
			if (!createNewHdc && this.Hdc != Win32DCSafeHandle.Zero)
			{
				return;
			}
			this.GetHdc();
		}

		public virtual void ReleaseCachedHdc(bool releaseHdc)
		{
			if (releaseHdc)
			{
				this.ReleaseHdc();
			}
		}

		public virtual void ExecuteSync(SynchronizedOperation synchronizedOperation)
		{
			synchronizedOperation();
		}

		public Win32DCSafeHandle GetHdc()
		{
			this.ReleaseHdc();
			this.Hdc = new Win32DCSafeHandle(this.m_graphicsBase.GetHdc(), false);
			return this.Hdc;
		}

		public void ReleaseHdc()
		{
			if (!this.Hdc.IsInvalid)
			{
				this.m_graphicsBase.ReleaseHdc();
				this.Hdc = Win32DCSafeHandle.Zero;
			}
		}

		public float ConvertToMillimeters(int pixels)
		{
			return SharedRenderer.ConvertToMillimeters(pixels, (float)this.m_dpiX);
		}

		public int ConvertToPixels(float mm)
		{
			return SharedRenderer.ConvertToPixels(mm, (float)this.m_dpiX);
		}
	}
}
