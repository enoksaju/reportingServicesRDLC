namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class Line : ReportItem
	{
		public new class Definition : DefinitionStore<Line, Definition.Properties>
		{
			public enum Properties
			{
				Style,
				Name,
				ActionInfo,
				Top,
				Left,
				Height,
				Width,
				ZIndex,
				Visibility,
				ToolTip,
				ToolTipLocID,
				DocumentMapLabel,
				DocumentMapLabelLocID,
				Bookmark,
				RepeatWith,
				CustomProperties,
				DataElementName,
				DataElementOutput,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		public Line()
		{
		}

		public Line(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
