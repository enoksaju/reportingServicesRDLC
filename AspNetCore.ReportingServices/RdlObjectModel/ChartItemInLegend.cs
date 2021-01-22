namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class ChartItemInLegend : ReportObject
	{
		public class Definition : DefinitionStore<ChartItemInLegend, Definition.Properties>
		{
			public enum Properties
			{
				LegendText,
				ToolTip,
				ActionInfo,
				Hidden,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		[ReportExpressionDefaultValue]
		public ReportExpression LegendText
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		[ReportExpressionDefaultValue]
		public ReportExpression ToolTip
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		public ActionInfo ActionInfo
		{
			get
			{
				return (ActionInfo)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Hidden
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		public ChartItemInLegend()
		{
		}

		public ChartItemInLegend(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
