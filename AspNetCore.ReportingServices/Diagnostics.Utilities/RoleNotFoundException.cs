using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class RoleNotFoundException : ReportCatalogException
	{
		public RoleNotFoundException(string roleName)
			: base(ErrorCode.rsRoleNotFound, ErrorStrings.rsRoleNotFound(roleName), null, null)
		{
		}

		private RoleNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
