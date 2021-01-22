using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class UnknownUserNameException : ReportCatalogException
	{
		public UnknownUserNameException(string userName)
			: base(ErrorCode.rsUnknownUserName, ErrorStrings.rsUnknownUserName(userName), null, null)
		{
		}

		public UnknownUserNameException(string userName, Exception innerException)
			: base(ErrorCode.rsUnknownUserName, ErrorStrings.rsUnknownUserName(userName), innerException, null)
		{
		}

		private UnknownUserNameException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
