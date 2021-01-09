using System.Collections.Generic;
using System.IO;

namespace AspNetCore.ReportingServices.Rendering.ExcelOpenXmlRenderer.Parser.spreadsheetml.x2006.main
{
	internal class CT_PageMargins : OoxmlComplexType
	{
		private double _left_attr;

		private double _right_attr;

		private double _top_attr;

		private double _bottom_attr;

		private double _header_attr;

		private double _footer_attr;

		public double Left_Attr
		{
			get
			{
				return this._left_attr;
			}
			set
			{
				this._left_attr = value;
			}
		}

		public double Right_Attr
		{
			get
			{
				return this._right_attr;
			}
			set
			{
				this._right_attr = value;
			}
		}

		public double Top_Attr
		{
			get
			{
				return this._top_attr;
			}
			set
			{
				this._top_attr = value;
			}
		}

		public double Bottom_Attr
		{
			get
			{
				return this._bottom_attr;
			}
			set
			{
				this._bottom_attr = value;
			}
		}

		public double Header_Attr
		{
			get
			{
				return this._header_attr;
			}
			set
			{
				this._header_attr = value;
			}
		}

		public double Footer_Attr
		{
			get
			{
				return this._footer_attr;
			}
			set
			{
				this._footer_attr = value;
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

		public override void WriteAsRoot(TextWriter s, string tagName, int depth, Dictionary<string, string> namespaces)
		{
			this.WriteOpenTag(s, tagName, depth, namespaces, true);
			this.WriteElements(s, depth, namespaces);
			this.WriteCloseTag(s, tagName, depth, namespaces);
		}

		public override void Write(TextWriter s, string tagName, int depth, Dictionary<string, string> namespaces)
		{
			this.WriteOpenTag(s, tagName, depth, namespaces, false);
			this.WriteElements(s, depth, namespaces);
			this.WriteCloseTag(s, tagName, depth, namespaces);
		}

		public override void WriteOpenTag(TextWriter s, string tagName, int depth, Dictionary<string, string> namespaces, bool root)
		{
			s.Write("<");
			OoxmlComplexType.WriteXmlPrefix(s, namespaces, "http://schemas.openxmlformats.org/spreadsheetml/2006/main");
			s.Write(tagName);
			this.WriteAttributes(s);
			if (root)
			{
				foreach (string key in namespaces.Keys)
				{
					s.Write(" xmlns");
					if (namespaces[key] != "")
					{
						s.Write(":");
						s.Write(namespaces[key]);
					}
					s.Write("=\"");
					s.Write(key);
					s.Write("\"");
				}
			}
			s.Write(">");
		}

		public override void WriteCloseTag(TextWriter s, string tagName, int depth, Dictionary<string, string> namespaces)
		{
			s.Write("</");
			OoxmlComplexType.WriteXmlPrefix(s, namespaces, "http://schemas.openxmlformats.org/spreadsheetml/2006/main");
			s.Write(tagName);
			s.Write(">");
		}

		public override void WriteAttributes(TextWriter s)
		{
			s.Write(" left=\"");
			OoxmlComplexType.WriteData(s, this._left_attr);
			s.Write("\"");
			s.Write(" right=\"");
			OoxmlComplexType.WriteData(s, this._right_attr);
			s.Write("\"");
			s.Write(" top=\"");
			OoxmlComplexType.WriteData(s, this._top_attr);
			s.Write("\"");
			s.Write(" bottom=\"");
			OoxmlComplexType.WriteData(s, this._bottom_attr);
			s.Write("\"");
			s.Write(" header=\"");
			OoxmlComplexType.WriteData(s, this._header_attr);
			s.Write("\"");
			s.Write(" footer=\"");
			OoxmlComplexType.WriteData(s, this._footer_attr);
			s.Write("\"");
		}

		public override void WriteElements(TextWriter s, int depth, Dictionary<string, string> namespaces)
		{
		}
	}
}
