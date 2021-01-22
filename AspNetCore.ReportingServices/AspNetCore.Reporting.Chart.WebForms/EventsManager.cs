using System;
using System.ComponentModel.Design;

namespace AspNetCore.Reporting.Chart.WebForms
{
	public class EventsManager : IServiceProvider
	{
		public IServiceContainer serviceContainer;

		private Chart control;

		private EventsManager()
		{
		}

		public EventsManager(IServiceContainer container)
		{
			if (container == null)
			{
				throw new ArgumentNullException(SR.ExceptionInvalidServiceContainer);
			}
			this.serviceContainer = container;
		}

		public object GetService(Type serviceType)
		{
			if (serviceType == typeof(EventsManager))
			{
				return this;
			}
			throw new ArgumentException(SR.ExceptionEventManagerUnsupportedType(serviceType.ToString()));
		}

		public void OnBackPaint(object caller, ChartPaintEventArgs e)
		{
			if (this.control == null)
			{
				this.control = (Chart)this.serviceContainer.GetService(typeof(Chart));
			}
			if (this.control != null)
			{
				this.control.CallBackPaint(caller, e);
			}
		}

		public void OnPaint(object caller, ChartPaintEventArgs e)
		{
			if (this.control == null)
			{
				this.control = (Chart)this.serviceContainer.GetService(typeof(Chart));
			}
			if (this.control != null)
			{
				this.control.CallPaint(caller, e);
			}
		}

		public void OnCustomize()
		{
			if (this.control == null)
			{
				this.control = (Chart)this.serviceContainer.GetService(typeof(Chart));
			}
			if (this.control != null)
			{
				this.control.CallCustomize();
			}
		}

		public void OnCustomizeLegend(LegendItemsCollection legendItems, string legendName)
		{
			if (this.control == null)
			{
				this.control = (Chart)this.serviceContainer.GetService(typeof(Chart));
			}
			if (this.control != null)
			{
				this.control.CallCustomizeLegend(legendItems, legendName);
			}
		}

		public void OnCustomizeMapAreas(MapAreasCollection areaItems)
		{
			if (this.control == null)
			{
				this.control = (Chart)this.serviceContainer.GetService(typeof(Chart));
			}
			if (this.control != null)
			{
				this.control.CallCustomizeMapAreas(areaItems);
			}
		}
	}
}
