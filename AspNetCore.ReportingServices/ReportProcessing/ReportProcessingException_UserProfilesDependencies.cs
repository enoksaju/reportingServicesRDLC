using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ReportProcessingException_UserProfilesDependencies : Exception
	{
		public ReportProcessingException_UserProfilesDependencies()
		{
		}

		private ReportProcessingException_UserProfilesDependencies(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
