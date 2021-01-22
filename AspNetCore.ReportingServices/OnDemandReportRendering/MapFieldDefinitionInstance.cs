namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class MapFieldDefinitionInstance : BaseInstance
	{
		private MapFieldDefinition m_defObject;

		public MapFieldDefinitionInstance(MapFieldDefinition defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		protected override void ResetInstanceCache()
		{
		}
	}
}
