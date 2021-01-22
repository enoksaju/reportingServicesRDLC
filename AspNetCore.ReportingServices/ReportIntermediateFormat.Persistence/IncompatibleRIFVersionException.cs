using AspNetCore.ReportingServices.Diagnostics.Utilities;
using AspNetCore.ReportingServices.ReportProcessing;
using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence
{
	[Serializable]
	public sealed class IncompatibleRIFVersionException : RSException
	{
		public IncompatibleRIFVersionException(int documentCompatVersion, int codeCompatVersion)
			: base(ErrorCode.rsIncompatibleRIFVersion, string.Format(CultureInfo.InvariantCulture, "The RIF document is not compatible with this code version.  Document Version: {0} Code Version: {1}", documentCompatVersion, codeCompatVersion), null, Global.Tracer, null)
		{
		}

		public IncompatibleRIFVersionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public static void ThrowIfIncompatible(int documentCompatVersion, int codeCompatVersion)
		{
			if (documentCompatVersion == codeCompatVersion)
			{
				return;
			}
			if (documentCompatVersion == 0)
			{
				return;
			}
			if (documentCompatVersion <= codeCompatVersion)
			{
				return;
			}
			throw new IncompatibleRIFVersionException(documentCompatVersion, codeCompatVersion);
		}
	}
}
