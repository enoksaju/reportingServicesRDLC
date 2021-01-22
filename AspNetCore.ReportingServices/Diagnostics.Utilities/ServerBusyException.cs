using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class ServerBusyException : ReportCatalogException
	{
		public ServerBusyException()
			: base(ErrorCode.rsServerBusy, ErrorStrings.rsServerBusy, null, null)
		{
		}

		private ServerBusyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
