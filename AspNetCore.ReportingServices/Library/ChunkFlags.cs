using System;

namespace AspNetCore.ReportingServices.Library
{
	[Flags]
	public enum ChunkFlags
	{
		None = 0,
		Compressed = 1,
		FileSystem = 2,
		CrossDatabaseSharing = 4
	}
}
