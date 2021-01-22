using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.Common
{
	public abstract class SerializableValue : IXmlSerializable
	{
		public const string TYPECODE = "TypeCode";

		public const string VALUE = "Value";

		private object m_value;

		private DataTypeCode m_dataTypeCode = DataTypeCode.Unknown;

		public DataTypeCode TypeCode
		{
			get
			{
				return this.m_dataTypeCode;
			}
		}

		public object Value
		{
			get
			{
				return this.m_value;
			}
		}

		protected SerializableValue()
		{
		}

		protected SerializableValue(object value)
		{
			this.m_value = value;
		}

		protected SerializableValue(object value, DataTypeCode dataTypeCode)
		{
			this.m_value = value;
			this.m_dataTypeCode = dataTypeCode;
		}

		public XmlSchema GetSchema()
		{
			throw new NotImplementedException();
		}

		protected abstract void ReadDerivedXml(XmlReader xmlReader);

		public void ReadXml(XmlReader xmlReader)
		{
			while (xmlReader.Read())
			{
				XmlNodeType nodeType = xmlReader.NodeType;
				if (nodeType == XmlNodeType.Element)
				{
					switch (xmlReader.Name)
					{
					case "TypeCode":
						break;
					case "Value":
						goto IL_005d;
					default:
						goto IL_0078;
					}
					xmlReader.Read();
					this.m_dataTypeCode = (DataTypeCode)Enum.Parse(typeof(DataTypeCode), xmlReader.Value, false);
					continue;
				}
				goto IL_0078;
				IL_005d:
				xmlReader.Read();
				this.m_value = ObjectSerializer.Read(xmlReader, this.m_dataTypeCode);
				continue;
				IL_0078:
				this.ReadDerivedXml(xmlReader);
			}
		}

		public abstract void WriteXml(XmlWriter writer);

		protected void WriteBaseXml(XmlWriter writer)
		{
			writer.WriteElementString("TypeCode", this.m_dataTypeCode.ToString());
			if (this.m_value != null)
			{
				writer.WriteStartElement("Value");
				ObjectSerializer.Write(writer, this.m_value, this.m_dataTypeCode);
				writer.WriteEndElement();
			}
		}
	}
}
