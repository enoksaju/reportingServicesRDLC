using System;
using System.Globalization;
using System.Text;
using System.Web.Services.Protocols;
using System.Xml;

namespace AspNetCore.ReportingServices.Common
{
	public static class SoapUtil
	{
		public const string DefaultServerErrorNamespace = "http://www.microsoft.com/sql/reportingservices";

		public const string DefaultNamespacePrefix = "msrs";

		public const string HelpLinkFormat = "https://go.microsoft.com/fwlink/?LinkId=20476&EvtSrc={0}&EvtID={1}&ProdName=Microsoft%20SQL%20Server%20Reporting%20Services&ProdVer={2}";

		public const string HelpLinkTag = "HelpLink";

		public const string XmlMoreInfoElement = "MoreInformation";

		public const string XmlMoreInfoSource = "Source";

		public const string XmlMoreInfoMessage = "Message";

		public const string XmlErrorCode = "ErrorCode";

		public const string XmlWarningsElement = "Warnings";

		public const string XmlWarningElement = "Warning";

		public const string XmlWarningCodeElement = "Code";

		public const string XmlWarningSeverityElement = "Severity";

		public const string XmlWarningObjectNameElement = "ObjectName";

		public const string XmlWarningObjectTypeElement = "ObjectType";

		public const string XmlWarningMessageElement = "Message";

		public const string SoapExceptionHttpStatus = "400";

		public static string RemoveInvalidXmlChars(string origText)
		{
			if (string.IsNullOrEmpty(origText))
			{
				return origText;
			}
			string result = origText;
			try
			{
				bool flag = false;
				StringBuilder stringBuilder = new StringBuilder(origText);
				for (int i = 0; i < stringBuilder.Length; i++)
				{
					char c = stringBuilder[i];
					if (c > '�' || (c < ' ' && c != '\t' && c != '\n' && c != '\r'))
					{
						stringBuilder[i] = ' ';
						flag = true;
					}
				}
				if (flag)
				{
					result = stringBuilder.ToString();
					return result;
				}
				return result;
			}
			catch (Exception)
			{
				return result;
			}
		}

		public static XmlNode CreateExceptionDetailsNode(XmlDocument doc, string code, string detailedMsg, string helpLink, string productName, string productVersion, int productLocaleId, string operatingSystem, int countryLocaleId)
		{
			XmlNode xmlNode = doc.CreateNode(XmlNodeType.Element, SoapException.DetailElementName.Name, SoapException.DetailElementName.Namespace);
			XmlNode xmlNode2 = SoapUtil.CreateNode(doc, "ErrorCode");
			xmlNode2.InnerText = code;
			xmlNode.AppendChild(xmlNode2);
			XmlNode xmlNode3 = SoapUtil.CreateNode(doc, "HttpStatus");
			xmlNode3.InnerText = "400";
			xmlNode.AppendChild(xmlNode3);
			XmlNode xmlNode4 = SoapUtil.CreateNode(doc, "Message");
			xmlNode4.InnerText = detailedMsg;
			xmlNode.AppendChild(xmlNode4);
			XmlNode xmlNode5 = SoapUtil.CreateNode(doc, "HelpLink");
			xmlNode5.InnerText = helpLink;
			xmlNode.AppendChild(xmlNode5);
			XmlNode xmlNode6 = SoapUtil.CreateNode(doc, "ProductName");
			xmlNode6.InnerText = productName;
			xmlNode.AppendChild(xmlNode6);
			XmlNode xmlNode7 = SoapUtil.CreateNode(doc, "ProductVersion");
			xmlNode7.InnerText = productVersion;
			xmlNode.AppendChild(xmlNode7);
			XmlNode xmlNode8 = SoapUtil.CreateNode(doc, "ProductLocaleId");
			xmlNode8.InnerText = productLocaleId.ToString(CultureInfo.InvariantCulture);
			xmlNode.AppendChild(xmlNode8);
			XmlNode xmlNode9 = SoapUtil.CreateNode(doc, "OperatingSystem");
			xmlNode9.InnerText = operatingSystem;
			xmlNode.AppendChild(xmlNode9);
			XmlNode xmlNode10 = SoapUtil.CreateNode(doc, "CountryLocaleId");
			xmlNode10.InnerText = countryLocaleId.ToString(CultureInfo.InvariantCulture);
			xmlNode.AppendChild(xmlNode10);
			return xmlNode;
		}

		public static XmlNode CreateNode(XmlDocument doc, string name)
		{
			return doc.CreateNode(XmlNodeType.Element, name, "http://www.microsoft.com/sql/reportingservices");
		}

		public static XmlNode CreateWarningNode(XmlDocument doc)
		{
			return SoapUtil.CreateNode(doc, "Warnings");
		}

		public static XmlNode CreateWarningCodeNode(XmlDocument doc)
		{
			return SoapUtil.CreateNode(doc, "Code");
		}

		public static XmlNode CreateWarningSeverityNode(XmlDocument doc)
		{
			return SoapUtil.CreateNode(doc, "Severity");
		}

		public static XmlNode CreateWarningObjectNameNode(XmlDocument doc)
		{
			return SoapUtil.CreateNode(doc, "ObjectName");
		}

		public static XmlNode CreateWarningObjectTypeNode(XmlDocument doc)
		{
			return SoapUtil.CreateNode(doc, "ObjectType");
		}

		public static XmlNode CreateWarningMessageNode(XmlDocument doc)
		{
			return SoapUtil.CreateNode(doc, "Message");
		}

		public static XmlNode CreateMoreInfoNode(XmlDocument doc)
		{
			return SoapUtil.CreateNode(doc, "MoreInformation");
		}

		public static XmlNode CreateMoreInfoSourceNode(XmlDocument doc)
		{
			return SoapUtil.CreateNode(doc, "Source");
		}

		public static XmlNode CreateMoreInfoMessageNode(XmlDocument doc)
		{
			return SoapUtil.CreateNode(doc, "Message");
		}

		public static XmlAttribute CreateErrorCodeAttr(XmlDocument doc)
		{
			return doc.CreateAttribute("msrs", "ErrorCode", "http://www.microsoft.com/sql/reportingservices");
		}

		public static XmlAttribute CreateHelpLinkTagAttr(XmlDocument doc)
		{
			return doc.CreateAttribute("msrs", "HelpLink", "http://www.microsoft.com/sql/reportingservices");
		}
	}
}
