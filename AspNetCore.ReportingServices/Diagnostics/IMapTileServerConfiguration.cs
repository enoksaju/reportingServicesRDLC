namespace AspNetCore.ReportingServices.Diagnostics
{
	public interface IMapTileServerConfiguration
	{
		int MaxConnections
		{
			get;
		}

		int Timeout
		{
			get;
		}

		string AppID
		{
			get;
		}

		MapTileCacheLevel CacheLevel
		{
			get;
		}
	}
}
