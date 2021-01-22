using System.Collections;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class ParagraphInstanceCollection : IEnumerable<ParagraphInstance>, IEnumerable
	{
		private TextBox m_textbox;

		public ParagraphInstanceCollection(TextBox textbox)
		{
			this.m_textbox = textbox;
		}

		public IEnumerator<ParagraphInstance> GetEnumerator()
		{
			return new ParagraphInstanceEnumerator(this.m_textbox);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
