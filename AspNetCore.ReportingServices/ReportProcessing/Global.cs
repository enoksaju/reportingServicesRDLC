using AspNetCore.ReportingServices.Diagnostics.Utilities;
using System.Reflection;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	public sealed class Global
	{
		public static readonly string ReportProcessingNamespace = "AspNetCore.ReportingServices.ReportProcessing";

		public static RSTrace Tracer = RSTrace.ProcessingTracer;

		public static RSTrace RenderingTracer = RSTrace.RenderingTracer;

		public static string ReportProcessingLocation = Assembly.GetExecutingAssembly().Location;

		private Global()
		{
		}
	}
}
