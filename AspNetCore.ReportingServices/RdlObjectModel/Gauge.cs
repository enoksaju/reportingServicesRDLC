using System.Collections.Generic;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class Gauge : GaugePanelItem
	{
		public new class Definition : DefinitionStore<Gauge, Definition.Properties>
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
				PropertyCount
			}

			private Definition()
			{
			}
		}

		[XmlElement(typeof(RdlCollection<GaugeScale>))]
		public IList<GaugeScale> GaugeScales
		{
			get
			{
				return (IList<GaugeScale>)base.PropertyStore.GetObject(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		public BackFrame BackFrame
		{
			get
			{
				return (BackFrame)base.PropertyStore.GetObject(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> ClipContent
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(13);
			}
			set
			{
				base.PropertyStore.SetObject(13, value);
			}
		}

		public TopImage TopImage
		{
			get
			{
				return (TopImage)base.PropertyStore.GetObject(14);
			}
			set
			{
				base.PropertyStore.SetObject(14, value);
			}
		}

		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> AspectRatio
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(15);
			}
			set
			{
				base.PropertyStore.SetObject(15, value);
			}
		}

		public Gauge()
		{
		}

		public Gauge(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.GaugeScales = new RdlCollection<GaugeScale>();
		}
	}
}
