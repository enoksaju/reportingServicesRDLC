namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class ShimMemberVisibility : Visibility
	{
		public abstract bool GetInstanceHidden();

		public abstract bool GetInstanceStartHidden();
	}
}
