using System.Collections;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class TextRunInstanceCollection : IEnumerable<TextRunInstance>, IEnumerable
	{
		private ParagraphInstance m_paragraphInstance;

		public TextRunInstanceCollection(ParagraphInstance paragraphInstance)
		{
			this.m_paragraphInstance = paragraphInstance;
		}

		public IEnumerator<TextRunInstance> GetEnumerator()
		{
			return new TextRunInstanceEnumerator(this.m_paragraphInstance);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
