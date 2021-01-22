namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public interface IShimDataRegionMember
	{
		bool SetNewContext(int index);

		void ResetContext();
	}
}
