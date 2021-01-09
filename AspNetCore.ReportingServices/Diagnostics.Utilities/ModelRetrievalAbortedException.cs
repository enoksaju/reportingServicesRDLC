using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	internal sealed class ModelRetrievalAbortedException : ReportCatalogException
	{
		internal enum CancelationTrigger
		{
			None,
			ModelResolutionAfterConnectionOpen,
			ModelResolutionBeforeConnectionOpen,
			ModelResolutionDuringConnectionOpenException,
			ModelResolutionDuringConnectionOpenRSException,
			ModelResolutionDuringModelResolution,
			ModelResolutionDuringModelResolutionException,
			ModelResolutionDuringModelResolutionRSException,
			ServerDataSourceResolverAfterDataSourceResolution,
			ServerDataSourceResolverBeforeDataSourceResolution,
			ServerDataSourceResolverDuringModelResolution,
			ServerDataSourceResolverDuringModelResolutionException,
			ServerDataSourceResolverDuringModelResolutionRSException
		}

		private readonly CancelationTrigger m_cancelationTrigger;

		internal CancelationTrigger Trigger
		{
			get
			{
				return this.m_cancelationTrigger;
			}
		}

		public ModelRetrievalAbortedException(CancelationTrigger cancelationTrigger)
			: base(ErrorCode.rsModelRetrievalCanceled, ErrorStrings.rsModelRetrievalCanceled, null, ModelRetrievalAbortedException.CreateAdditionalTraceMessage(cancelationTrigger))
		{
			this.m_cancelationTrigger = cancelationTrigger;
		}

		private ModelRetrievalAbortedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private static string CreateAdditionalTraceMessage(CancelationTrigger trigger)
		{
			return string.Format(CultureInfo.InvariantCulture, "[{0}]", trigger.ToString());
		}
	}
}
