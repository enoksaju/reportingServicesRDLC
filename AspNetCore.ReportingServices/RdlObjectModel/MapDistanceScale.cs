using System.Globalization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class MapDistanceScale : MapDockableSubItem
	{
		public new class Definition : DefinitionStore<MapDistanceScale, Definition.Properties>
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
				ScaleColor,
				ScaleBorderColor,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		[ReportExpressionDefaultValue(typeof(ReportColor), "White")]
		public ReportExpression<ReportColor> ScaleColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(13);
			}
			set
			{
				base.PropertyStore.SetObject(13, value);
			}
		}

		[ReportExpressionDefaultValue(typeof(ReportColor), "DarkGray")]
		public ReportExpression<ReportColor> ScaleBorderColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(14);
			}
			set
			{
				base.PropertyStore.SetObject(14, value);
			}
		}

		public MapDistanceScale()
		{
		}

		public MapDistanceScale(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.ScaleColor = new ReportExpression<ReportColor>("White", CultureInfo.InvariantCulture);
			this.ScaleBorderColor = new ReportExpression<ReportColor>("DarkGray", CultureInfo.InvariantCulture);
		}
	}
}
