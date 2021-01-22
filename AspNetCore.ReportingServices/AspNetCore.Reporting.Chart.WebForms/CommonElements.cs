using AspNetCore.Reporting.Chart.WebForms.Borders3D;
using AspNetCore.Reporting.Chart.WebForms.ChartTypes;
using AspNetCore.Reporting.Chart.WebForms.Data;
using AspNetCore.Reporting.Chart.WebForms.Formulas;
using AspNetCore.Reporting.Chart.WebForms.Utilities;
using System.ComponentModel.Design;
using System.Globalization;

namespace AspNetCore.Reporting.Chart.WebForms
{
	public class CommonElements
	{
		public ChartAreaCollection chartAreaCollection;

		public ChartGraphics graph;

		public IServiceContainer container;

		public bool processModePaint = true;

		public bool processModeRegions;

		private int width;

		private int height;

		public DataManager DataManager
		{
			get
			{
				return (DataManager)this.container.GetService(typeof(DataManager));
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

		public HotRegionsList HotRegionsList
		{
			get
			{
				return this.ChartPicture.hotRegionsList;
			}
			set
			{
				this.ChartPicture.hotRegionsList = value;
			}
		}

		public DataManipulator DataManipulator
		{
			get
			{
				return this.ChartPicture.DataManipulator;
			}
		}

		public ImageLoader ImageLoader
		{
			get
			{
				return (ImageLoader)this.container.GetService(typeof(ImageLoader));
			}
		}

		public Chart Chart
		{
			get
			{
				return (Chart)this.container.GetService(typeof(Chart));
			}
		}

		public EventsManager EventsManager
		{
			get
			{
				return (EventsManager)this.container.GetService(typeof(EventsManager));
			}
		}

		public ChartTypeRegistry ChartTypeRegistry
		{
			get
			{
				return (ChartTypeRegistry)this.container.GetService(typeof(ChartTypeRegistry));
			}
		}

		public BorderTypeRegistry BorderTypeRegistry
		{
			get
			{
				return (BorderTypeRegistry)this.container.GetService(typeof(BorderTypeRegistry));
			}
		}

		public FormulaRegistry FormulaRegistry
		{
			get
			{
				return (FormulaRegistry)this.container.GetService(typeof(FormulaRegistry));
			}
		}

		public ChartImage ChartPicture
		{
			get
			{
				return (ChartImage)this.container.GetService(typeof(ChartImage));
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

		private CommonElements()
		{
		}

		public CommonElements(IServiceContainer container)
		{
			this.container = container;
		}

		public void TraceWrite(string category, string message)
		{
			if (this.container != null)
			{
				TraceManager traceManager = (TraceManager)this.container.GetService(typeof(TraceManager));
				if (traceManager != null)
				{
					traceManager.Write(category, message);
				}
			}
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
