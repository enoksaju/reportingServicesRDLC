using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Flags]
	public enum UserProfileState
	{
		None = 0,
		InQuery = 1,
		InReport = 2,
		Both = 3,
		OnDemandExpressions = 8
	}
}
