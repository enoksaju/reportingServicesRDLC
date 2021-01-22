using System.Net.Cache;

namespace AspNetCore.ReportingServices.Diagnostics
{
	public static class MapTileServerConsts
	{
		public const string MaxConnections = "MaxConnections";

		public const string Timeout = "Timeout";

		public const string AppID = "AppID";

		public const string CacheLevel = "CacheLevel";

		public const int MaxConnectionsDefault = 2;

		public const int TimeoutDefault = 10;

		public const string AppIDDefault = "(Default)";

		public const MapTileCacheLevel CacheLevelDefault = MapTileCacheLevel.Default;

		public const int MaxConnectionsMinValue = 1;

		public const int MaxConnectionsMaxValue = 2147483647;

		public const int TimeoutMinValue = 1;

		public const int TimeoutMaxValue = 2147483647;

		public static RequestCacheLevel ConvertFromMapTileCacheLevel(MapTileCacheLevel cacheLevel)
		{
			switch (cacheLevel)
			{
			case MapTileCacheLevel.BypassCache:
				return RequestCacheLevel.BypassCache;
			case MapTileCacheLevel.CacheIfAvailable:
				return RequestCacheLevel.CacheIfAvailable;
			case MapTileCacheLevel.CacheOnly:
				return RequestCacheLevel.CacheOnly;
			case MapTileCacheLevel.NoCacheNoStore:
				return RequestCacheLevel.NoCacheNoStore;
			case MapTileCacheLevel.Reload:
				return RequestCacheLevel.Reload;
			case MapTileCacheLevel.Revalidate:
				return RequestCacheLevel.Revalidate;
			default:
				return RequestCacheLevel.Default;
			}
		}
	}
}
