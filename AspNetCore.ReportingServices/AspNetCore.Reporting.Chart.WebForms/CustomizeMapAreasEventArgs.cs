using System;

namespace AspNetCore.Reporting.Chart.WebForms
{
	public class CustomizeMapAreasEventArgs : EventArgs
	{
		private MapAreasCollection areaItems;

		public MapAreasCollection MapAreaItems
		{
			get
			{
				return this.areaItems;
			}
		}

		private CustomizeMapAreasEventArgs()
		{
		}

		public CustomizeMapAreasEventArgs(MapAreasCollection areaItems)
		{
			this.areaItems = areaItems;
		}
	}
}
