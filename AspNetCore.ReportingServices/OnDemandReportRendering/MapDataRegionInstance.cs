namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class MapDataRegionInstance : DataRegionInstance
	{
		public MapDataRegionInstance(MapDataRegion reportItemDef)
			: base(reportItemDef)
		{
		}

		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
		}
	}
}
