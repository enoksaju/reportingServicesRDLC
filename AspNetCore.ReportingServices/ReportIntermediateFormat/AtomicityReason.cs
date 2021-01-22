namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	public enum AtomicityReason
	{
		Filters,
		Sorts,
		NonNaturalSorts,
		NonNaturalGroup,
		DomainScope,
		RecursiveParent,
		Aggregates,
		RunningValues,
		Lookups,
		PeerChildScopes
	}
}
