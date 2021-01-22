using System;

namespace AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing
{
	[Flags]
	public enum AggregateUpdateFlags
	{
		None = 0,
		ScopedAggregates = 1,
		RowAggregates = 2,
		Both = 3
	}
}
