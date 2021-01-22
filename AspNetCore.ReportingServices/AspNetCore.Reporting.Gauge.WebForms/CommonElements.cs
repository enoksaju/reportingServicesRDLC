using System;
using System.ComponentModel.Design;

namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class CommonElements
	{
		private GaugeGraphics graph;

		public IServiceContainer container;

		public bool processModePaint = true;

		public bool processModeRegions;

		public ObjectLinker objectLinker;

		private int width;

		private int height;

		private GaugeCore gaugeCore;

		public ImageLoader ImageLoader
		{
			get
			{
				return (ImageLoader)this.container.GetService(typeof(ImageLoader));
			}
		}

		public GaugeCore GaugeCore
		{
			get
			{
				if (this.gaugeCore == null)
				{
					this.gaugeCore = (GaugeCore)this.container.GetService(typeof(GaugeCore));
				}
				return this.gaugeCore;
			}
		}

		public GaugeContainer GaugeContainer
		{
			get
			{
				return this.GaugeCore.GaugeContainer;
			}
		}

		public GaugeGraphics Graph
		{
			get
			{
				if (this.graph != null)
				{
					return this.graph;
				}
				throw new ApplicationException(Utils.SRGetStr("ExceptionGdiNonInitialized"));
			}
			set
			{
				this.graph = value;
			}
		}

		public ObjectLinker ObjectLinker
		{
			get
			{
				return this.objectLinker;
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

		public CommonElements(IServiceContainer container)
		{
			this.container = container;
			this.objectLinker = new ObjectLinker(this.GaugeCore);
			this.objectLinker.Common = this;
		}

		public void InvokePrePaint(object sender)
		{
			this.GaugeContainer.OnPrePaint(sender, new GaugePaintEventArgs(this.GaugeContainer, this.graph));
		}

		public void InvokePostPaint(object sender)
		{
			this.GaugeContainer.OnPostPaint(sender, new GaugePaintEventArgs(this.GaugeContainer, this.graph));
		}
	}
}
