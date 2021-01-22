namespace AspNetCore.Reporting.Map.WebForms
{
	public class ElementEventArgs
	{
		private MapControl control;

		private NamedElement mapElement;

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

		public ElementEventArgs(MapControl control, NamedElement mapElement)
		{
			this.control = control;
			this.mapElement = mapElement;
		}
	}
}
