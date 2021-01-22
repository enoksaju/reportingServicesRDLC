using AspNetCore.ReportingServices.RdlObjectModel.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class Border : ReportObject, IShouldSerialize
	{
		public class Definition : DefinitionStore<Border, Definition.Properties>
		{
			public enum Properties
			{
				Color,
				Style,
				Width
			}

			private Definition()
			{
			}
		}

		[ReportExpressionDefaultValueConstant(typeof(ReportColor), "DefaultBorderColor")]
		public ReportExpression<ReportColor> Color
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		[ReportExpressionDefaultValue(typeof(BorderStyles), BorderStyles.Default)]
		public ReportExpression<BorderStyles> Style
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<BorderStyles>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		[ReportExpressionDefaultValueConstant(typeof(ReportSize), "DefaultBorderWidth")]
		public ReportExpression<ReportSize> Width
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		public Border()
		{
		}

		public Border(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		bool IShouldSerialize.ShouldSerializeThis()
		{
			return true;
		}

		SerializationMethod IShouldSerialize.ShouldSerializeProperty(string property)
		{
			if (property == "Style" && !this.Style.IsExpression && this.Style.Value == BorderStyles.Default)
			{
				return SerializationMethod.Never;
			}
			if (base.Parent is Style && ((Style)base.Parent).Border != this)
			{
				return SerializationMethod.Always;
			}
			return SerializationMethod.Auto;
		}
	}
}
