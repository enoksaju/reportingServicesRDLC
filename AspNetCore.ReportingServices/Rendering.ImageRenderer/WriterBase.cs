using AspNetCore.ReportingServices.Interfaces;
using AspNetCore.ReportingServices.Rendering.RichText;
using AspNetCore.ReportingServices.Rendering.RPLProcessing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace AspNetCore.ReportingServices.Rendering.ImageRenderer
{
	public abstract class WriterBase : IDisposable
	{
		private const int DPI = 96;

		protected Stream m_outputStream;

		protected GraphicsBase m_commonGraphics;

		protected Dictionary<string, GDIFont> m_gdiFonts = new Dictionary<string, GDIFont>();

		protected Renderer m_renderer;

		private bool m_disposeRenderer = true;

		private RectangleF m_pageSectionBounds = RectangleF.Empty;

		private CreateAndRegisterStream m_createAndRegisterStream;

		public RectangleF PageSectionBounds
		{
			get
			{
				return this.m_pageSectionBounds;
			}
		}

		public virtual float HalfPixelWidthY
		{
			get
			{
				return (float)(this.ConvertToMillimeters(1) / 2.0);
			}
		}

		public virtual float HalfPixelWidthX
		{
			get
			{
				return (float)(this.ConvertToMillimeters(1) / 2.0);
			}
		}

		public CreateAndRegisterStream CreateAndRegisterStream
		{
			get
			{
				return this.m_createAndRegisterStream;
			}
		}

		public GraphicsBase CommonGraphics
		{
			get
			{
				return this.m_commonGraphics;
			}
		}

		protected WriterBase(Renderer renderer, Stream stream, bool disposeRenderer, CreateAndRegisterStream createAndRegisterStream)
		{
			this.m_renderer = renderer;
			this.m_outputStream = stream;
			this.m_disposeRenderer = disposeRenderer;
			this.m_createAndRegisterStream = createAndRegisterStream;
			if (this.m_renderer != null)
			{
				this.m_renderer.Writer = this;
			}
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.m_commonGraphics != null)
				{
					this.m_commonGraphics.Dispose();
					this.m_commonGraphics = null;
				}
				if (this.m_gdiFonts != null)
				{
					foreach (string key in this.m_gdiFonts.Keys)
					{
						this.m_gdiFonts[key].Dispose();
					}
					this.m_gdiFonts = null;
				}
				if (this.m_disposeRenderer && this.m_renderer != null)
				{
					this.m_renderer.Dispose();
					this.m_renderer = null;
				}
			}
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		~WriterBase()
		{
			this.Dispose(false);
		}

		public virtual void BeginReport(int dpiX, int dpiY)
		{
			if (dpiX == 0)
			{
				dpiX = 96;
			}
			if (dpiY == 0)
			{
				dpiY = 96;
			}
			this.m_commonGraphics = new GraphicsBase((float)dpiX, (float)dpiY);
		}

		public virtual void BeginPage(float pageWidth, float pageHeight)
		{
		}

		public virtual void BeginPageSection(RectangleF bounds)
		{
			this.m_pageSectionBounds = bounds;
		}

		public virtual RectangleF CalculatePageBounds(RPLPageContent pageContent, out float pageWidth, out float pageHeight)
		{
			RPLPageLayout pageLayout = pageContent.PageLayout;
			pageWidth = pageLayout.PageWidth;
			pageHeight = pageLayout.PageHeight;
			float width = pageLayout.PageWidth - pageLayout.MarginLeft - pageLayout.MarginRight;
			float height = pageLayout.PageHeight - pageLayout.MarginTop - pageLayout.MarginBottom;
			return new RectangleF(pageLayout.MarginLeft, pageLayout.MarginTop, width, height);
		}

		public virtual RectangleF CalculateColumnBounds(RPLReportSection section, RPLPageLayout pageLayout, RPLItemMeasurement column, int columnNumber, float top, float columnHeight, float columnWidth)
		{
			return RectangleF.Empty;
		}

		public virtual RectangleF CalculateHeaderBounds(RPLReportSection section, RPLPageLayout pageLayout, float top, float width)
		{
			return RectangleF.Empty;
		}

		public virtual RectangleF CalculateFooterBounds(RPLReportSection section, RPLPageLayout pageLayout, float top, float width)
		{
			return RectangleF.Empty;
		}

		public virtual void DrawBackgroundImage(RPLImageData imageData, RPLFormat.BackgroundRepeatTypes repeat, PointF start, RectangleF position)
		{
		}

		public virtual void DrawLine(Color color, float size, RPLFormat.BorderStyles style, float x1, float y1, float x2, float y2)
		{
		}

		public virtual void DrawDynamicImage(string imageName, Stream imageStream, long imageDataOffset, RectangleF position)
		{
		}

		public virtual void DrawImage(RectangleF position, RPLImage image, RPLImageProps instanceProperties, RPLImagePropsDef definitionProperties)
		{
		}

		public virtual void DrawRectangle(Color color, float size, RPLFormat.BorderStyles style, RectangleF rectangle)
		{
		}

		public virtual void DrawTextRun(Win32DCSafeHandle hdc, FontCache fontCache, ReportTextBox textBox, TextRun run, TypeCode typeCode, RPLFormat.TextAlignments textAlign, RPLFormat.VerticalAlignments verticalAlign, RPLFormat.WritingModes writingMode, RPLFormat.Directions direction, Point position, Rectangle layoutRectangle, int lineHeight, int baselineY)
		{
		}

		public virtual void EndPageSection()
		{
		}

		public virtual void EndPage()
		{
		}

		public virtual void EndReport()
		{
		}

		public virtual void FillPolygon(Color color, PointF[] polygon)
		{
		}

		public virtual void FillRectangle(Color color, RectangleF rectangle)
		{
		}

		public virtual void PostProcessPage()
		{
		}

		public virtual void PostProcessReportItem(object state)
		{
		}

		public virtual void PreProcessPage(string uniqueName, RectangleF bounds)
		{
		}

		public virtual object PreProcessReportItem(RPLElement element, RPLElementProps instanceProperties, RectangleF position, bool hasLabel)
		{
			return null;
		}

		public virtual void ProcessAction(string uniqueName, RPLActionInfo actionInfo, RectangleF position)
		{
		}

		public virtual void ProcessAction(string uniqueName, RPLActionInfoWithImageMap actionInfo, RectangleF position)
		{
		}

		public virtual void ProcessBookmark(string uniqueName, PointF point)
		{
		}

		public virtual void ProcessFixedHeaders(RPLTablix tablix, RectangleF position, float[] rowStarts, float[] columnStarts)
		{
		}

		public virtual void ProcessLabel(string uniqueName, string label, PointF point)
		{
		}

		public virtual void ProcessSort(string uniqueName, RPLFormat.SortOptions sortState, ref RectangleF textPosition)
		{
		}

		public virtual void ProcessToggle(string uniqueName, bool toggleState, ref RectangleF textPosition)
		{
		}

		public void GetHdc(bool createNewHdc, out Win32DCSafeHandle hdc, out float dpiX)
		{
			if (this.m_commonGraphics != null)
			{
				this.m_commonGraphics.CacheHdc(createNewHdc);
				hdc = this.m_commonGraphics.Hdc;
				dpiX = (float)this.m_commonGraphics.DpiX;
			}
			else
			{
				hdc = Win32DCSafeHandle.Zero;
				dpiX = -1f;
			}
		}

		public void ReleaseHdc(bool release)
		{
			if (this.m_commonGraphics != null)
			{
				this.m_commonGraphics.ReleaseCachedHdc(release);
			}
		}

		public virtual void ClipTextboxRectangle(Win32DCSafeHandle hdc, RectangleF position)
		{
		}

		public virtual void UnClipTextboxRectangle(Win32DCSafeHandle hdc)
		{
		}

		public virtual float ConvertToMillimeters(int pixels)
		{
			return this.m_commonGraphics.ConvertToMillimeters(pixels);
		}

		public virtual int ConvertToPixels(float mm)
		{
			return this.m_commonGraphics.ConvertToPixels(mm);
		}
	}
}
