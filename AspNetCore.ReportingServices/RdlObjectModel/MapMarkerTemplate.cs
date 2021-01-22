namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class MapMarkerTemplate : MapPointTemplate
	{
		public new class Definition : DefinitionStore<MapMarkerTemplate, Definition.Properties>
		{
			public enum Properties
			{
				Style,
				ActionInfo,
				Hidden,
				OffsetX,
				OffsetY,
				Label,
				ToolTip,
				DataElementName,
				DataElementOutput,
				DataElementLabel,
				Size,
				LabelPlacement,
				MapMarker,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		public MapMarker MapMarker
		{
			get
			{
				return (MapMarker)base.PropertyStore.GetObject(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		public MapMarkerTemplate()
		{
		}

		public MapMarkerTemplate(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
		}
	}
}
