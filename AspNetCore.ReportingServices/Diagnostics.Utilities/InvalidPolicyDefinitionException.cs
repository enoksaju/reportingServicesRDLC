using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class InvalidPolicyDefinitionException : ReportCatalogException
	{
		public InvalidPolicyDefinitionException(string principalName)
			: base(ErrorCode.rsInvalidPolicyDefinition, ErrorStrings.rsInvalidPolicyDefinition(principalName), null, null)
		{
		}

		private InvalidPolicyDefinitionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
