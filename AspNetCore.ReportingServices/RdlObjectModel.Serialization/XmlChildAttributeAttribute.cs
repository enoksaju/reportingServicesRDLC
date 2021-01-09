using System;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel.Serialization
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	internal sealed class XmlChildAttributeAttribute : XmlAttributeAttribute
	{
		private string m_elementName;

		public string ElementName
		{
			get
			{
				return this.m_elementName;
			}
		}

		public XmlChildAttributeAttribute(string elementName, string attributeName)
			: this(elementName, attributeName, null)
		{
		}

		public XmlChildAttributeAttribute(string elementName, string attributeName, string namespaceUri)
			: base(attributeName)
		{
			this.m_elementName = elementName;
			base.Namespace = namespaceUri;
		}
	}
}
