namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class MapDataBoundViewInstance : MapViewInstance
	{
		private MapDataBoundView m_defObject;

		public MapDataBoundViewInstance(MapDataBoundView defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
		}
	}
}
