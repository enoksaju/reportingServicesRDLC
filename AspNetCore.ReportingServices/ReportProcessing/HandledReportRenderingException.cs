using AspNetCore.ReportingServices.Diagnostics.Utilities;
using AspNetCore.ReportingServices.OnDemandReportRendering;
using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	internal sealed class HandledReportRenderingException : RSException
	{
		private HandledReportRenderingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		internal HandledReportRenderingException(ReportRenderingException innerException)
			: base(innerException.ErrorCode, innerException.Message, innerException, Global.RenderingTracer, null)
		{
		}

		internal HandledReportRenderingException(ErrorCode errCode, string message)
			: base(errCode, message, null, Global.RenderingTracer, null)
		{
		}

		internal HandledReportRenderingException(ErrorCode errCode, Exception innerException)
			: base(errCode, innerException.Message, innerException, Global.RenderingTracer, null)
		{
		}
	}
}
