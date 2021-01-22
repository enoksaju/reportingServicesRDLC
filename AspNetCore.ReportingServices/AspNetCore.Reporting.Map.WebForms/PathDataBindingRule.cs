using System.ComponentModel;

namespace AspNetCore.Reporting.Map.WebForms
{
	[TypeConverter(typeof(PathDataBindingRuleConverter))]
	public class PathDataBindingRule : DataBindingRuleBase
	{
		[SRCategory("CategoryAttribute_Data")]
		[SRDescription("DescriptionAttributePathDataBindingRule_BindingField")]
		public override string BindingField
		{
			get
			{
				return base.BindingField;
			}
			set
			{
				base.BindingField = value;
			}
		}

		public PathDataBindingRule()
			: this(null)
		{
		}

		public PathDataBindingRule(CommonElements common)
			: base(common)
		{
		}

		public override void DataBind()
		{
			if (this.Common != null)
			{
				this.Common.MapCore.ExecuteDataBind(BindingType.Paths, this, base.DataSource, base.DataMember, this.BindingField);
			}
		}
	}
}
