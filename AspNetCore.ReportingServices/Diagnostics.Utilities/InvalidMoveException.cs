using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class InvalidMoveException : ReportCatalogException
	{
		public InvalidMoveException(string itemPath, string targetPath)
			: base(ErrorCode.rsInvalidMove, ErrorStrings.rsInvalidMove(itemPath, targetPath), null, null)
		{
		}

		private InvalidMoveException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
