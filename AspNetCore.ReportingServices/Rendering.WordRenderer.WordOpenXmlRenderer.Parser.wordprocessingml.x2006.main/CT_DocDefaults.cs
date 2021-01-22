using System.Collections.Generic;
using System.IO;

namespace AspNetCore.ReportingServices.Rendering.WordRenderer.WordOpenXmlRenderer.Parser.wordprocessingml.x2006.main
{
	public class CT_DocDefaults : OoxmlComplexType, IOoxmlComplexType
	{
		private CT_RPrDefault _rPrDefault;

		public CT_RPrDefault RPrDefault
		{
			get
			{
				return this._rPrDefault;
			}
			set
			{
				this._rPrDefault = value;
			}
		}

		public static string RPrDefaultElementName
		{
			get
			{
				return "rPrDefault";
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
		}

		public override void WriteElements(TextWriter s)
		{
			this.Write_rPrDefault(s);
		}

		public void Write_rPrDefault(TextWriter s)
		{
			if (this._rPrDefault != null)
			{
				this._rPrDefault.Write(s, "rPrDefault");
			}
		}
	}
}
