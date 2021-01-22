using System;

namespace AspNetCore.Reporting.Gauge.WebForms
{
	[Serializable]
	public enum PeriodType
	{
		Milliseconds,
		Seconds,
		Minutes,
		Hours,
		Days
	}
}
