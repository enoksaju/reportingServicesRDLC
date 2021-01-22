using AspNetCore.ReportingServices.Rendering.SPBProcessing;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public static class InteractivityPaginationModuleFactory
	{
		public static IInteractivityPaginationModule CreateInteractivityPaginationModule()
		{
			return new SPBInteractivityProcessing();
		}
	}
}
