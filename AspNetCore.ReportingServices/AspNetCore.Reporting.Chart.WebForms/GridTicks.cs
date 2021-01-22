using System;

namespace AspNetCore.Reporting.Chart.WebForms
{
	[Flags]
	public enum GridTicks
	{
		None = 0,
		TickMark = 1,
		Gridline = 2,
		All = 3
	}
}
