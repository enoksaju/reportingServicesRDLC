using System;
using System.Xml;

namespace AspNetCore.ReportingServices.ProgressivePackaging
{
	public sealed class ImageRequestMessageElement : ImageMessageElement
	{
		public const string NameSpace2011 = "http://schemas.microsoft.com/sqlserver/reporting/2011/01/getexternalimages";

		public const string ClientRequestElement = "ClientRequest";

		public const string ExternalImagesElement = "ExternalImages";

		public const string ExternalImageElement = "ExternalImage";

		private const string UriElement = "Uri";

		private const string MaxWidthElement = "MaxWidth";

		private const string MaxHeightElement = "MaxHeight";

		public ImageRequestMessageElement()
		{
		}

		public ImageRequestMessageElement(string imageUrl, string imageWidth, string imageHeight)
			: base(imageUrl, imageWidth, imageHeight)
		{
		}

		public void Write(XmlWriter writer)
		{
			writer.WriteStartElement("ExternalImage");
			writer.WriteStartElement("Uri");
			writer.WriteString(base.ImageUrl);
			writer.WriteEndElement();
			writer.WriteStartElement("MaxWidth");
			writer.WriteValue(base.ImageWidth);
			writer.WriteEndElement();
			writer.WriteStartElement("MaxHeight");
			writer.WriteValue(base.ImageHeight);
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		public void Read(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Element)
				{
					if ("Uri".Equals(reader.Name, StringComparison.Ordinal))
					{
						base.ImageUrl = this.ReadStringValue(reader);
					}
					else if ("MaxWidth".Equals(reader.Name, StringComparison.Ordinal))
					{
						base.ImageWidth = this.ReadStringValue(reader);
					}
					else if ("MaxHeight".Equals(reader.Name, StringComparison.Ordinal))
					{
						base.ImageHeight = this.ReadStringValue(reader);
					}
				}
			}
		}

		private string ReadStringValue(XmlReader reader)
		{
			if (!reader.IsEmptyElement)
			{
				reader.Read();
			}
			return reader.Value;
		}
	}
}
