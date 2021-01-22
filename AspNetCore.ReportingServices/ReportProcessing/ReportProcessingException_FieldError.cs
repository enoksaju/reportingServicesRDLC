using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ReportProcessingException_FieldError : Exception
	{
		private DataFieldStatus m_status;

		public DataFieldStatus Status
		{
			get
			{
				return this.m_status;
			}
		}

		public ReportProcessingException_FieldError(DataFieldStatus status, string message)
			: base((message == null) ? "" : message, null)
		{
			this.m_status = status;
		}

		private ReportProcessingException_FieldError(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
