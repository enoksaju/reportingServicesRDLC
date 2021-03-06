using System;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class WindowsAuthzApiException : ReportCatalogException
	{
		public WindowsAuthzApiException(string methodName, string errorCode, string username)
			: base(ErrorCode.rsWinAuthzError, ErrorStrings.rsWinAuthz(methodName, errorCode, username), null, null)
		{
		}
	}
}
