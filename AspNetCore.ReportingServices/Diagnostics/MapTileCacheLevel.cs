namespace AspNetCore.ReportingServices.Diagnostics
{
	public enum MapTileCacheLevel
	{
		Default,
		BypassCache,
		CacheOnly,
		CacheIfAvailable,
		Revalidate,
		Reload,
		NoCacheNoStore
	}
}
