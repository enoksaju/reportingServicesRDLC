using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Flags]
	public enum PaginationMode
	{
		Progressive = 0,
		TotalPages = 1,
		Estimate = 2
	}
}
