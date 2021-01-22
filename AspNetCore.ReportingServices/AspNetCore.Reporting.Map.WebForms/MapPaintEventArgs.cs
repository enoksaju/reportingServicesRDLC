namespace AspNetCore.Reporting.Map.WebForms
{
	public class MapPaintEventArgs
	{
		private MapControl control;

		private NamedElement mapElement;

		private MapGraphics graphics;

		public MapControl MapControl
		{
			get
			{
				return this.control;
			}
		}

		public NamedElement MapElement
		{
			get
			{
				return this.mapElement;
			}
		}

		public MapGraphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		public MapPaintEventArgs(MapControl control, NamedElement mapElement, MapGraphics graphics)
		{
			this.control = control;
			this.mapElement = mapElement;
			this.graphics = graphics;
		}
	}
}
