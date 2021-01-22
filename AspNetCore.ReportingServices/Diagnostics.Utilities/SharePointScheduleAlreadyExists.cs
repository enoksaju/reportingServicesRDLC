using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class SharePointScheduleAlreadyExists : ReportCatalogException
	{
		public SharePointScheduleAlreadyExists(string name, string path)
			: base(ErrorCode.rsScheduleAlreadyExists, ErrorStrings.rsSharePoitScheduleAlreadyExists(name, path), null, null)
		{
		}

		private SharePointScheduleAlreadyExists(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
