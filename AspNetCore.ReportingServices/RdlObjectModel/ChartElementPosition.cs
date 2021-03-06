namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class ChartElementPosition : ReportObject
	{
		public class Definition : DefinitionStore<ChartElementPosition, Definition.Properties>
		{
			public enum Properties
			{
				Top,
				Left,
				Height,
				Width
			}

			private Definition()
			{
			}
		}

		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> Top
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> Left
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> Height
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> Width
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		public ChartElementPosition()
		{
		}

		public ChartElementPosition(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
