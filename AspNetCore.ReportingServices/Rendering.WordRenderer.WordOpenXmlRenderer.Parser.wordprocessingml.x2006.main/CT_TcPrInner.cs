using System.Collections.Generic;
using System.IO;

namespace AspNetCore.ReportingServices.Rendering.WordRenderer.WordOpenXmlRenderer.Parser.wordprocessingml.x2006.main
{
	public class CT_TcPrInner : CT_TcPrBase, IOoxmlComplexType
	{
		public enum ChoiceBucket_0
		{
			cellIns,
			cellDel,
			cellMerge
		}

		private ChoiceBucket_0 _choice_0;

		public ChoiceBucket_0 Choice_0
		{
			get
			{
				return this._choice_0;
			}
			set
			{
				this._choice_0 = value;
			}
		}

		protected override void InitAttributes()
		{
		}

		protected override void InitElements()
		{
		}

		protected override void InitCollections()
		{
		}

		public override void Write(TextWriter s, string tagName)
		{
			this.WriteOpenTag(s, tagName, null);
			this.WriteElements(s);
			this.WriteCloseTag(s, tagName);
		}

		public override void WriteOpenTag(TextWriter s, string tagName, Dictionary<string, string> namespaces)
		{
			base.WriteOpenTag(s, tagName, "w", namespaces);
		}

		public override void WriteCloseTag(TextWriter s, string tagName)
		{
			s.Write("</w:");
			s.Write(tagName);
			s.Write(">");
		}

		public override void WriteAttributes(TextWriter s)
		{
			base.WriteAttributes(s);
		}

		public override void WriteElements(TextWriter s)
		{
			base.WriteElements(s);
		}
	}
}
