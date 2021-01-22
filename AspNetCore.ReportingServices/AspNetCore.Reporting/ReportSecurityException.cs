using System;
using System.Runtime.Serialization;

namespace AspNetCore.Reporting
{
	[Serializable]
	public sealed class ReportSecurityException : ReportViewerException
	{
		public ReportSecurityException(string message)
			: base(message)
		{
		}

		private ReportSecurityException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
