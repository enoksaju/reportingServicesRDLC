using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class BackupKeyPasswordInvalidException : ReportCatalogException
	{
		public BackupKeyPasswordInvalidException()
			: base(ErrorCode.rsBackupKeyPasswordInvalid, ErrorStrings.rsBackupKeyPasswordInvalid, null, null)
		{
		}

		private BackupKeyPasswordInvalidException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
