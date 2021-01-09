using AspNetCore.ReportingServices.ReportProcessing;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	internal sealed class VisibilityToggleInfo
	{
		internal ObjectType ObjectType;

		internal string ObjectName;

		internal Visibility Visibility;

		internal string GroupName;

		internal Hashtable GroupingSet;

		internal bool IsTablixMember;
	}
}
