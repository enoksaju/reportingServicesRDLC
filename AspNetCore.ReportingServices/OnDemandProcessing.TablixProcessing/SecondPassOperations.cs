using System;

namespace AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing
{
	[Flags]
	public enum SecondPassOperations
	{
		None = 0,
		Variables = 1,
		Sorting = 2,
		FilteringOrAggregatesOrDomainScope = 4
	}
}
