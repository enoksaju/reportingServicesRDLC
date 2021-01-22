using System;
using System.Runtime.Serialization;
using System.Xml;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class RDLUpgradeException : XmlException
	{
		public RDLUpgradeException(string msg)
			: base(msg)
		{
		}

		public RDLUpgradeException(string msg, Exception inner)
			: base(msg, inner)
		{
		}

		private RDLUpgradeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
