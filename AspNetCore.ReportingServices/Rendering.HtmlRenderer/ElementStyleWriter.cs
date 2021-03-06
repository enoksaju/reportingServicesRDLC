using AspNetCore.ReportingServices.Rendering.RPLProcessing;

namespace AspNetCore.ReportingServices.Rendering.HtmlRenderer
{
	public abstract class ElementStyleWriter
	{
		protected IHtmlReportWriter m_renderer;

		public ElementStyleWriter(IHtmlReportWriter renderer)
		{
			this.m_renderer = renderer;
		}

		public abstract bool NeedsToWriteNullStyle(StyleWriterMode mode);

		public abstract void WriteStyles(StyleWriterMode mode, IRPLStyle style);

		protected void WriteStream(string s)
		{
			this.m_renderer.WriteStream(s);
		}

		protected void WriteStream(byte[] value)
		{
			this.m_renderer.WriteStream(value);
		}

		protected void WriteStyle(byte[] text, object value)
		{
			if (value != null)
			{
				this.WriteStream(text);
				this.WriteStream(value.ToString());
				this.WriteStream(HTML4Renderer.m_semiColon);
			}
		}

		protected void WriteStyle(byte[] text, object nonShared, object shared)
		{
			object obj = nonShared;
			if (obj == null)
			{
				obj = shared;
			}
			this.WriteStyle(text, obj);
		}
	}
}
