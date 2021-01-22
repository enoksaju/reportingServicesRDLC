using System;

namespace AspNetCore.ReportingServices.Rendering.SPBProcessing
{
	public sealed class TextBoxSharedInfo : IDisposable
	{
		private CanvasFont m_sharedFont;

		private int m_sharedState;

		private int m_pageNumber;

		public CanvasFont SharedFont
		{
			get
			{
				return this.m_sharedFont;
			}
			set
			{
				this.m_sharedFont = value;
			}
		}

		public int SharedState
		{
			get
			{
				return this.m_sharedState;
			}
			set
			{
				this.m_sharedState = value;
			}
		}

		public int PageNumber
		{
			get
			{
				return this.m_pageNumber;
			}
			set
			{
				this.m_pageNumber = value;
			}
		}

		public TextBoxSharedInfo(CanvasFont font, int sharedState)
		{
			this.m_sharedFont = font;
			this.m_sharedState = sharedState;
		}

		public TextBoxSharedInfo(int pageNumber)
		{
			this.m_pageNumber = pageNumber;
		}

		private void Dispose(bool disposing)
		{
			if (disposing && this.m_sharedFont != null)
			{
				this.m_sharedFont.Dispose();
				this.m_sharedFont = null;
			}
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
