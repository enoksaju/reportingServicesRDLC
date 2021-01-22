using System;

namespace AspNetCore.ReportingServices.Diagnostics
{
	[Flags]
	public enum ItemPathOptions
	{
		None = 0,
		Validate = 1,
		Convert = 2,
		Translate = 4,
		AllowEditSessionSyntax = 8,
		IgnoreValidateEditSession = 0x10,
		Default = 7
	}
}
