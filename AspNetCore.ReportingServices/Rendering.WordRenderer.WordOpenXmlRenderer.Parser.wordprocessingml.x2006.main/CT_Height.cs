using System.Collections.Generic;
using System.IO;

namespace AspNetCore.ReportingServices.Rendering.WordRenderer.WordOpenXmlRenderer.Parser.wordprocessingml.x2006.main
{
	public class CT_Height : OoxmlComplexType, IOoxmlComplexType
	{
		private string _val_attr;

		private ST_HeightRule _hRule_attr;

		public string Val_Attr
		{
			get
			{
				return this._val_attr;
			}
			set
			{
				this._val_attr = value;
			}
		}

		public ST_HeightRule HRule_Attr
		{
			get
			{
				return this._hRule_attr;
			}
			set
			{
				this._hRule_attr = value;
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
			base.WriteEmptyTag(s, tagName, "w");
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
			s.Write(" w:val=\"");
			OoxmlComplexType.WriteData(s, this._val_attr);
			s.Write("\"");
			s.Write(" w:hRule=\"");
			OoxmlComplexType.WriteData(s, this._hRule_attr);
			s.Write("\"");
		}

		public override void WriteElements(TextWriter s)
		{
		}
	}
}
