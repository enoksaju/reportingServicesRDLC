using System;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public sealed class StaticReferenceAttribute : Attribute
	{
	}
}
