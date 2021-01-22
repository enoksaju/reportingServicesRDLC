using System;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel.Serialization
{
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
	public sealed class XmlElementClassAttribute : XmlElementAttribute
	{
		public XmlElementClassAttribute(string elementName)
			: base(elementName)
		{
		}

		public XmlElementClassAttribute(string elementName, Type type)
			: base(elementName, type)
		{
		}
	}
}
