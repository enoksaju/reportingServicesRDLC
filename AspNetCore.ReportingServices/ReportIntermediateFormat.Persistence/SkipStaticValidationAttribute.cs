using System;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class SkipStaticValidationAttribute : Attribute
	{
	}
}
