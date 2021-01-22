using System;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;

namespace AspNetCore.Reporting.Map.WebForms
{
	public class CommonElements
	{
		private MapGraphics graph;

		public IServiceContainer container;

		public bool processModePaint = true;

		public bool processModeRegions;

		private int width;

		private int height;

		private MapCore mapCore;

		public Size Size
		{
			get
			{
				return new Size(this.width, this.height);
			}
		}

		public bool ProcessModePaint
		{
			get
			{
				return this.processModePaint;
			}
		}

		public bool ProcessModeRegions
		{
			get
			{
				return this.processModeRegions;
			}
		}

		public ImageLoader ImageLoader
		{
			get
			{
				return (ImageLoader)this.container.GetService(typeof(ImageLoader));
			}
		}

		public MapCore MapCore
		{
			get
			{
				if (this.mapCore == null)
				{
					this.mapCore = (MapCore)this.container.GetService(typeof(MapCore));
				}
				return this.mapCore;
			}
		}

		public MapControl MapControl
		{
			get
			{
				return this.MapCore.MapControl;
			}
		}

		public bool IsGraphicsInitialized
		{
			get
			{
				return this.graph != null;
			}
		}

		public MapGraphics Graph
		{
			get
			{
				if (this.graph != null)
				{
					return this.graph;
				}
				throw new ApplicationException(SR.gdi_noninitialized);
			}
			set
			{
				this.graph = value;
			}
		}

		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		public BorderTypeRegistry BorderTypeRegistry
		{
			get
			{
				return (BorderTypeRegistry)this.container.GetService(typeof(BorderTypeRegistry));
			}
		}

		public CommonElements(IServiceContainer container)
		{
			this.container = container;
		}

		public void InvokePrePaint(NamedElement sender)
		{
			this.MapControl.OnPrePaint(this.MapControl, new MapPaintEventArgs(this.MapControl, sender, this.graph));
		}

		public void InvokePostPaint(NamedElement sender)
		{
			this.MapControl.OnPostPaint(this.MapControl, new MapPaintEventArgs(this.MapControl, sender, this.graph));
		}

		public void InvokeElementAdded(NamedElement sender)
		{
			this.MapControl.OnElementAdded(this.MapControl, new ElementEventArgs(this.MapControl, sender));
		}

		public void InvokeElementRemoved(NamedElement sender)
		{
			this.MapControl.OnElementRemoved(this.MapControl, new ElementEventArgs(this.MapControl, sender));
		}

		public static double ParseDouble(string stringToParse)
		{
			double num = 0.0;
			try
			{
				return double.Parse(stringToParse, CultureInfo.InvariantCulture);
			}
			catch
			{
				return double.Parse(stringToParse, CultureInfo.CurrentCulture);
			}
		}

		public static float ParseFloat(string stringToParse)
		{
			float num = 0f;
			try
			{
				return float.Parse(stringToParse, CultureInfo.InvariantCulture);
			}
			catch
			{
				return float.Parse(stringToParse, CultureInfo.CurrentCulture);
			}
		}
	}
}
