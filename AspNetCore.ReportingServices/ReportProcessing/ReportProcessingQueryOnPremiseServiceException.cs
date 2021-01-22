using AspNetCore.ReportingServices.Diagnostics.Utilities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ReportProcessingQueryOnPremiseServiceException : ReportProcessingException
	{
		private const string OnPremiseServiceExceptionCode = "OnPremiseServiceException";

		public ReportProcessingQueryOnPremiseServiceException(ErrorCode errorCode, Exception innerException, params object[] arguments)
			: base(errorCode, innerException, arguments)
		{
		}

		private ReportProcessingQueryOnPremiseServiceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		protected override List<AdditionalMessage> GetAdditionalMessages()
		{
			return new List<AdditionalMessage>(new AdditionalMessage[1]
			{
				new AdditionalMessage("OnPremiseServiceException", "Error", base.InnerException.Message, null, null, null, null)
			});
		}
	}
}
