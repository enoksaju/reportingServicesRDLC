using System.Collections.Generic;
using System.IO;

namespace AspNetCore.ReportingServices.Rendering.WordRenderer.WordOpenXmlRenderer.Parser
{
	public interface IOoxmlComplexType
	{
		void WriteAsRoot(TextWriter s, string tagName, Dictionary<string, string> namespaces);

		void Write(TextWriter s, string tagName);

		void WriteOpenTag(TextWriter s, string tagName, Dictionary<string, string> namespaces);

		void WriteCloseTag(TextWriter s, string tagName);

		void WriteAttributes(TextWriter s);

		void WriteElements(TextWriter s);
	}
}
