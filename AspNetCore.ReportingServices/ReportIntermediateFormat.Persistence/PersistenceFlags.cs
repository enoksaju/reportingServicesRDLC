using System;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence
{
	[Flags]
	public enum PersistenceFlags
	{
		None = 0,
		Seekable = 1,
		CompatVersioned = 2
	}
}
