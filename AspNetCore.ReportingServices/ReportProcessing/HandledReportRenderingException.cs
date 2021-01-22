using AspNetCore.ReportingServices.Diagnostics.Utilities;
using AspNetCore.ReportingServices.OnDemandReportRendering;
using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class HandledReportRenderingException : RSException
	{
		private HandledReportRenderingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public HandledReportRenderingException(ReportRenderingException innerException)
			: base(innerException.ErrorCode, innerException.Message, innerException, Global.RenderingTracer, null)
		{
		}

		public HandledReportRenderingException(ErrorCode errCode, string message)
			: base(errCode, message, null, Global.RenderingTracer, null)
		{
		}

		public HandledReportRenderingException(ErrorCode errCode, Exception innerException)
			: base(errCode, innerException.Message, innerException, Global.RenderingTracer, null)
		{
		}
	}
}
