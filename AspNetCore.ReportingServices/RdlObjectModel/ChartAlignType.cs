namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class ChartAlignType : ReportObject
	{
		public class Definition : DefinitionStore<ChartAlignType, Definition.Properties>
		{
			public enum Properties
			{
				AxesView,
				Cursor,
				Position,
				InnerPlotPosition
			}

			private Definition()
			{
			}
		}

		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> AxesView
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Cursor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Position
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> InnerPlotPosition
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

		public ChartAlignType()
		{
		}

		public ChartAlignType(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
