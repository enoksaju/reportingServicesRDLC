using AspNetCore.ReportingServices.Diagnostics.Utilities;
using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class RenderingObjectModelException : RSException
	{
		private ProcessingErrorCode m_processingErrorCode;

		public ProcessingErrorCode ProcessingErrorCode
		{
			get
			{
				return this.m_processingErrorCode;
			}
		}

		private RenderingObjectModelException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public RenderingObjectModelException(string LocalizedErrorMessage)
			: base(ErrorCode.rrRenderingError, LocalizedErrorMessage, null, Global.RenderingTracer, null)
		{
		}

		public RenderingObjectModelException(Exception innerException)
			: base(ErrorCode.rrRenderingError, innerException.Message, innerException, Global.RenderingTracer, null)
		{
		}

		public RenderingObjectModelException(ProcessingErrorCode errCode)
			: base(ErrorCode.rrRenderingError, RPRes.Keys.GetString(errCode.ToString()), null, Global.RenderingTracer, null)
		{
			this.m_processingErrorCode = errCode;
		}

		public RenderingObjectModelException(ProcessingErrorCode errCode, params object[] arguments)
			: base(ErrorCode.rrRenderingError, string.Format(CultureInfo.CurrentCulture, RPRes.Keys.GetString(errCode.ToString()), arguments), null, Global.RenderingTracer, null)
		{
			this.m_processingErrorCode = errCode;
		}

		public RenderingObjectModelException(ErrorCode code, params object[] arguments)
			: base(code, string.Format(CultureInfo.CurrentCulture, RPRes.Keys.GetString(code.ToString()), arguments), null, Global.Tracer, null)
		{
		}

		public RenderingObjectModelException(ErrorCode code, Exception innerException, params object[] arguments)
			: base(code, string.Format(CultureInfo.CurrentCulture, RPRes.Keys.GetString(code.ToString()), arguments), innerException, Global.Tracer, null)
		{
		}
	}
}
