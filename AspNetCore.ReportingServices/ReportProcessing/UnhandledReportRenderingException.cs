using AspNetCore.ReportingServices.Diagnostics.Utilities;
using AspNetCore.ReportingServices.OnDemandReportRendering;
using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class UnhandledReportRenderingException : RSException
	{
		private UnhandledReportRenderingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public UnhandledReportRenderingException(ReportRenderingException innerException)
			: base(innerException.ErrorCode, innerException.Message, innerException, Global.RenderingTracer, null)
		{
		}

		public UnhandledReportRenderingException(Exception innerException)
			: base(ErrorCode.rrRenderingError, RenderErrors.Keys.GetString(ErrorCode.rrRenderingError.ToString()), innerException, Global.RenderingTracer, null)
		{
		}
	}
}
