using System;

namespace AspNetCore.ReportingServices.Common
{
	[Flags]
	public enum ScopeIDType
	{
		None = 0,
		SortValues = 1,
		GroupValues = 2,
		SortGroup = 3
	}
}
