using System;

namespace AspNetCore.Reporting.Gauge.WebForms
{
	[Serializable]
	public enum DurationType
	{
		Infinite,
		Milliseconds,
		Seconds,
		Minutes,
		Hours,
		Days,
		Count
	}
}
