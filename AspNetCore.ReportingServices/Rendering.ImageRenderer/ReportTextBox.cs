using AspNetCore.ReportingServices.Rendering.RichText;
using AspNetCore.ReportingServices.Rendering.RPLProcessing;
using System;
using System.Drawing;

namespace AspNetCore.ReportingServices.Rendering.ImageRenderer
{
	internal class ReportTextBox : ITextBoxProps
	{
		private RPLTextBoxProps m_source;

		private WriterBase m_writer;

		private bool m_spanPages;

		public RPLFormat.TextAlignments DefaultAlignment
		{
			get
			{
				TypeCode typeCode = ((RPLTextBoxPropsDef)this.m_source.Definition).SharedTypeCode;
				if (typeCode == TypeCode.Object)
				{
					typeCode = this.m_source.TypeCode;
				}
				return SharedRenderer.GetTextAlignForGeneral(typeCode, RPLFormat.Directions.LTR);
			}
		}

		public RPLFormat.Directions Direction
		{
			get
			{
				RPLFormat.Directions result = RPLFormat.Directions.LTR;
				object obj = this.m_source.Style[29];
				if (obj != null)
				{
					result = (RPLFormat.Directions)obj;
				}
				return result;
			}
		}

		public RPLFormat.WritingModes WritingMode
		{
			get
			{
				RPLFormat.WritingModes result = RPLFormat.WritingModes.Horizontal;
				object obj = this.m_source.Style[30];
				if (obj != null)
				{
					result = (RPLFormat.WritingModes)obj;
				}
				return result;
			}
		}

		public Color BackgroundColor
		{
			get
			{
				return Color.Empty;
			}
		}

		public bool CanGrow
		{
			get
			{
				if (this.m_spanPages)
				{
					return false;
				}
				return ((RPLTextBoxPropsDef)this.m_source.Definition).CanGrow;
			}
		}

		internal RPLFormat.VerticalAlignments VerticalAlignment
		{
			get
			{
				RPLFormat.VerticalAlignments result = RPLFormat.VerticalAlignments.Top;
				object obj = this.m_source.Style[26];
				if (obj != null)
				{
					result = (RPLFormat.VerticalAlignments)obj;
				}
				return result;
			}
		}

		internal bool SpanPages
		{
			set
			{
				this.m_spanPages = value;
			}
		}

		internal string UniqueName
		{
			get
			{
				return this.m_source.UniqueName;
			}
		}

		internal ReportTextBox(RPLTextBoxProps source, WriterBase writer)
		{
			this.m_source = source;
			this.m_writer = writer;
		}

		public void DrawTextRun(TextRun run, Paragraph paragraph, Win32DCSafeHandle hdc, float dpiX, FontCache fontCache, int x, int y, int baselineY, int lineHeight, Rectangle layoutRectangle)
		{
			TypeCode typeCode = ((RPLTextBoxPropsDef)this.m_source.Definition).SharedTypeCode;
			if (typeCode == TypeCode.Object)
			{
				typeCode = this.m_source.TypeCode;
			}
			ReportTextRun reportTextRun = run.TextRunProperties as ReportTextRun;
			if (reportTextRun != null && reportTextRun.ActionInfo != null && reportTextRun.ActionInfo.Actions.Length > 0)
			{
				RPLActionInfo actionInfo = reportTextRun.ActionInfo;
				RectangleF position = default(RectangleF);
				if (this.WritingMode == RPLFormat.WritingModes.Horizontal)
				{
					position.Width = SharedRenderer.ConvertToMillimeters(run.GetWidth(hdc, fontCache), fontCache.Dpi);
					position.Height = SharedRenderer.ConvertToMillimeters(run.GetHeight(hdc, fontCache), fontCache.Dpi);
					position.X = SharedRenderer.ConvertToMillimeters(layoutRectangle.X, fontCache.Dpi) + SharedRenderer.ConvertToMillimeters(x, fontCache.Dpi);
					position.Y = SharedRenderer.ConvertToMillimeters(layoutRectangle.Y, fontCache.Dpi) + (SharedRenderer.ConvertToMillimeters(y, fontCache.Dpi) - position.Height);
				}
				else
				{
					position.Width = SharedRenderer.ConvertToMillimeters(run.GetHeight(hdc, fontCache), fontCache.Dpi);
					position.Height = SharedRenderer.ConvertToMillimeters(run.GetWidth(hdc, fontCache), fontCache.Dpi);
					if (this.WritingMode == RPLFormat.WritingModes.Vertical)
					{
						position.X = SharedRenderer.ConvertToMillimeters(layoutRectangle.Right, fontCache.Dpi) - SharedRenderer.ConvertToMillimeters(y, fontCache.Dpi);
						position.Y = SharedRenderer.ConvertToMillimeters(layoutRectangle.Y, fontCache.Dpi) + SharedRenderer.ConvertToMillimeters(x, fontCache.Dpi);
					}
					else
					{
						position.X = SharedRenderer.ConvertToMillimeters(layoutRectangle.X, fontCache.Dpi) + SharedRenderer.ConvertToMillimeters(y, fontCache.Dpi) - position.Width;
						position.Y = SharedRenderer.ConvertToMillimeters(layoutRectangle.Bottom, fontCache.Dpi) - SharedRenderer.ConvertToMillimeters(x, fontCache.Dpi) - position.Height;
					}
				}
				this.m_writer.ProcessAction(reportTextRun.UniqueName, actionInfo, position);
			}
			this.m_writer.DrawTextRun(hdc, fontCache, this, run, typeCode, paragraph.ParagraphProps.Alignment, this.VerticalAlignment, this.WritingMode, this.Direction, new Point(x, y), layoutRectangle, lineHeight, baselineY);
		}

		public void DrawClippedTextRun(TextRun run, Paragraph paragraph, Win32DCSafeHandle hdc, float dpiX, FontCache fontCache, int x, int y, int baselineY, int lineHeight, Rectangle layoutRectangle, uint fontColorOverride, Rectangle clipRect)
		{
		}
	}
}
