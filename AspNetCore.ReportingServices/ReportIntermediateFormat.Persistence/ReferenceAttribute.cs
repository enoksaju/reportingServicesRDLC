using System;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public sealed class ReferenceAttribute : Attribute
	{
	}
}
