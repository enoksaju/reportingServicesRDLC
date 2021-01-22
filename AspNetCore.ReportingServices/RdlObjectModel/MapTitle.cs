using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class MapTitle : MapDockableSubItem, INamedObject
	{
		public new class Definition : DefinitionStore<MapTitle, Definition.Properties>
		{
			public enum Properties
			{
				Style,
				MapLocation,
				MapSize,
				LeftMargin,
				RightMargin,
				TopMargin,
				BottomMargin,
				ZIndex,
				ActionInfo,
				MapPosition,
				DockOutsideViewport,
				Hidden,
				ToolTip,
				Name,
				Text,
				Angle,
				TextShadowOffset,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		[XmlAttribute(typeof(string))]
		public string Name
		{
			get
			{
				return (string)base.PropertyStore.GetObject(13);
			}
			set
			{
				base.PropertyStore.SetObject(13, value);
			}
		}

		public ReportExpression Text
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(14);
			}
			set
			{
				base.PropertyStore.SetObject(14, value);
			}
		}

		[ReportExpressionDefaultValue(typeof(double), "0")]
		public ReportExpression<double> Angle
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

		public ReportExpression<ReportSize> TextShadowOffset
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(16);
			}
			set
			{
				base.PropertyStore.SetObject(16, value);
			}
		}

		public MapTitle()
		{
		}

		public MapTitle(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.Angle = 0.0;
		}
	}
}
