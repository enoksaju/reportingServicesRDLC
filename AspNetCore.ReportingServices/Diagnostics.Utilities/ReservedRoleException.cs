using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class ReservedRoleException : ReportCatalogException
	{
		public ReservedRoleException(string roleName)
			: base(ErrorCode.rsReservedRole, ErrorStrings.rsReservedRole(roleName), null, null)
		{
		}

		private ReservedRoleException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
