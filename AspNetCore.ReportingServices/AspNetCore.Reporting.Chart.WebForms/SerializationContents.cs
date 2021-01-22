using System;

namespace AspNetCore.Reporting.Chart.WebForms
{
	[Flags]
	public enum SerializationContents
	{
		Default = 1,
		Data = 2,
		Appearance = 4,
		All = 7
	}
}
