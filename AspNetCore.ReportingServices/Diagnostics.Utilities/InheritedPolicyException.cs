using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class InheritedPolicyException : ReportCatalogException
	{
		public InheritedPolicyException(string itemPath)
			: base(ErrorCode.rsInheritedPolicy, ErrorStrings.rsInheritedPolicy(itemPath), null, null)
		{
		}

		public InheritedPolicyException(string itemPath, string itemID)
			: base(ErrorCode.rsInheritedPolicy, ErrorStrings.rsInheritedPolicyModelItem(itemPath, itemID), null, null)
		{
		}

		private InheritedPolicyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
