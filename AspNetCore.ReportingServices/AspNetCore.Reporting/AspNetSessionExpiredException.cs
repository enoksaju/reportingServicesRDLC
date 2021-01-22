using System;
using System.Runtime.Serialization;

namespace AspNetCore.Reporting
{
	[Serializable]
	public sealed class AspNetSessionExpiredException : ReportViewerException
	{
		public AspNetSessionExpiredException()
			: base(Errors.ASPNetSessionExpired)
		{
		}

		private AspNetSessionExpiredException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
