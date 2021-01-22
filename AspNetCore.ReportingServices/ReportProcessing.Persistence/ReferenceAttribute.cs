using System;

namespace AspNetCore.ReportingServices.ReportProcessing.Persistence
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public sealed class ReferenceAttribute : Attribute
	{
	}
}
