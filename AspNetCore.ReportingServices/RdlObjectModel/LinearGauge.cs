namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class LinearGauge : Gauge
	{
		public new class Definition : DefinitionStore<LinearGauge, Definition.Properties>
		{
			public enum Properties
			{
				Name,
				Style,
				Top,
				Left,
				Height,
				Width,
				ZIndex,
				Hidden,
				ToolTip,
				ActionInfo,
				ParentItem,
				GaugeScales,
				BackFrame,
				ClipContent,
				TopImage,
				AspectRatio,
				Orientation,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		[ReportExpressionDefaultValue(typeof(Orientations), Orientations.Auto)]
		public ReportExpression<Orientations> Orientation
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<Orientations>>(16);
			}
			set
			{
				base.PropertyStore.SetObject(16, value);
			}
		}

		public LinearGauge()
		{
		}

		public LinearGauge(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
