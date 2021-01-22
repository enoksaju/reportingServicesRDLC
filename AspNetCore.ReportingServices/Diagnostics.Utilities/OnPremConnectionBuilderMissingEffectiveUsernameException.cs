using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class OnPremConnectionBuilderMissingEffectiveUsernameException : ReportCatalogException
	{
		public OnPremConnectionBuilderMissingEffectiveUsernameException()
			: base(ErrorCode.rsOnPremConnectionBuilderMissingEffectiveUsername, ErrorStrings.rsOnPremConnectionBuilderMissingEffectiveUsername, null, null)
		{
		}

		private OnPremConnectionBuilderMissingEffectiveUsernameException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
