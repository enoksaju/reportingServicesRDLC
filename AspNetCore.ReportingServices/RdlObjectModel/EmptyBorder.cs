using AspNetCore.ReportingServices.RdlObjectModel.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	[XmlElementClass("Border")]
	internal class EmptyBorder : Border, IShouldSerialize
	{
		[ReportExpressionDefaultValue(typeof(ReportColor))]
		public new ReportExpression<ReportColor> Color
		{
			get
			{
				return base.Color;
			}
			set
			{
				base.Color = value;
			}
		}

		[ReportExpressionDefaultValue(typeof(BorderStyles), BorderStyles.Solid)]
		public new ReportExpression<BorderStyles> Style
		{
			get
			{
				return base.Style;
			}
			set
			{
				base.Style = value;
			}
		}

		public EmptyBorder()
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.Color = Constants.DefaultEmptyColor;
			this.Style = BorderStyles.Solid;
		}

		internal EmptyBorder(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		bool IShouldSerialize.ShouldSerializeThis()
		{
			return true;
		}

		SerializationMethod IShouldSerialize.ShouldSerializeProperty(string property)
		{
			if (property == "Style" && !this.Style.IsExpression && this.Style.Value == BorderStyles.Solid)
			{
				return SerializationMethod.Never;
			}
			if (base.Parent is EmptyColorStyle && ((EmptyColorStyle)base.Parent).Border != this)
			{
				return SerializationMethod.Always;
			}
			return SerializationMethod.Auto;
		}
	}
}
