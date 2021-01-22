using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class RoleAlreadyExistsException : ReportCatalogException
	{
		public RoleAlreadyExistsException(string roleName)
			: base(ErrorCode.rsRoleAlreadyExists, ErrorStrings.rsRoleAlreadyExists(roleName), null, null)
		{
		}

		private RoleAlreadyExistsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
