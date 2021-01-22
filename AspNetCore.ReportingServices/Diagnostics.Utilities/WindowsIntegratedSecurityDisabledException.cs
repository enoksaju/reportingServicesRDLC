using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class WindowsIntegratedSecurityDisabledException : ReportCatalogException
	{
		public WindowsIntegratedSecurityDisabledException()
			: base(ErrorCode.rsWindowsIntegratedSecurityDisabled, ErrorStrings.rsWindowsIntegratedSecurityDisabled, null, null)
		{
		}

		private WindowsIntegratedSecurityDisabledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
