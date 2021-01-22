using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Internal
{
    public sealed class SerializableDictionary : Dictionary<string, int>, IXmlSerializable
	{
		public SerializableDictionary()
		{
		}

		public SerializableDictionary(IDictionary<string, int> dictionary)
			: base(dictionary)
		{
		}

		public SerializableDictionary(IEqualityComparer<string> comparer)
			: base(comparer)
		{
		}

		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			foreach (KeyValuePair<string, int> item in this)
			{
				writer.WriteStartElement(item.Key);
				writer.WriteValue(item.Value);
				writer.WriteEndElement();
			}
		}

		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			throw new NotImplementedException();
		}

		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}
	}
}
