using AspNetCore.ReportingServices.ReportProcessing;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	public sealed class VisibilityToggleInfo
	{
		public ObjectType ObjectType;

		public string ObjectName;

		public Visibility Visibility;

		public string GroupName;

		public Hashtable GroupingSet;

		public bool IsTablixMember;
	}
}
