using System;

namespace AspNetCore.ReportingServices.ReportProcessing.Persistence
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class HashtableOfReferencesAttribute : Attribute
	{
	}
}
