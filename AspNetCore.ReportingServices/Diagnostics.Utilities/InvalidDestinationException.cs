using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class InvalidDestinationException : ReportCatalogException
	{
		public InvalidDestinationException(string sourcePath, string targetPath)
			: base(ErrorCode.rsInvalidDestination, ErrorStrings.rsInvalidDestination(sourcePath, targetPath), null, null)
		{
		}

		private InvalidDestinationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
