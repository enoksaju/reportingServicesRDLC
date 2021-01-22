using System.Collections.Generic;
using System.IO;

namespace AspNetCore.ReportingServices.Rendering.WordRenderer.WordOpenXmlRenderer.Parser.wordprocessingml.x2006.main
{
	public class CT_TblWidth : OoxmlComplexType, IOoxmlComplexType
	{
		private string _w_attr;

		private ST_TblWidth _type_attr;

		public string W_Attr
		{
			get
			{
				return this._w_attr;
			}
			set
			{
				this._w_attr = value;
			}
		}

		public ST_TblWidth Type_Attr
		{
			get
			{
				return this._type_attr;
			}
			set
			{
				this._type_attr = value;
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
			s.Write(" w:w=\"");
			OoxmlComplexType.WriteData(s, this._w_attr);
			s.Write("\"");
			s.Write(" w:type=\"");
			OoxmlComplexType.WriteData(s, this._type_attr);
			s.Write("\"");
		}

		public override void WriteElements(TextWriter s)
		{
		}
	}
}
