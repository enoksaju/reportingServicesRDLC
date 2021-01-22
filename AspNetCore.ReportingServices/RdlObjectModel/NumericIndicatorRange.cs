using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class NumericIndicatorRange : ReportObject, INamedObject
	{
		public class Definition : DefinitionStore<NumericIndicatorRange, Definition.Properties>
		{
			public enum Properties
			{
				Name,
				StartValue,
				EndValue,
				DecimalDigitColor,
				DigitColor,
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
				return (string)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		public GaugeInputValue StartValue
		{
			get
			{
				return (GaugeInputValue)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		public GaugeInputValue EndValue
		{
			get
			{
				return (GaugeInputValue)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		[ReportExpressionDefaultValue(typeof(ReportColor))]
		public ReportExpression<ReportColor> DecimalDigitColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		[ReportExpressionDefaultValue(typeof(ReportColor))]
		public ReportExpression<ReportColor> DigitColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		public NumericIndicatorRange()
		{
		}

		public NumericIndicatorRange(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
