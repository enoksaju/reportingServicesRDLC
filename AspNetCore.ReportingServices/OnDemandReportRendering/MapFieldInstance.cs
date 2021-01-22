namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class MapFieldInstance : BaseInstance
	{
		private MapField m_defObject;

		public MapFieldInstance(MapField defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		protected override void ResetInstanceCache()
		{
		}
	}
}
