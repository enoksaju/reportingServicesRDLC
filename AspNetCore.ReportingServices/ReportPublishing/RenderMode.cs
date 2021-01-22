using System;

namespace AspNetCore.ReportingServices.ReportPublishing
{
	[Flags]
	public enum RenderMode
	{
		RenderEdit = 1,
		FullOdp = 2,
		Both = 3
	}
}
