using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class RSAddinVersionMismatchException : ReportCatalogException
	{
		public RSAddinVersionMismatchException()
			: base(ErrorCode.rsVersionMismatch, ErrorStrings.rsVersionMismatch, null, null)
		{
		}

		private RSAddinVersionMismatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
