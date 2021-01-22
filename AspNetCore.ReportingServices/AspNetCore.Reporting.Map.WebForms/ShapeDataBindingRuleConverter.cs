namespace AspNetCore.Reporting.Map.WebForms
{
	public class ShapeDataBindingRuleConverter : CollectionItemTypeConverter
	{
		public ShapeDataBindingRuleConverter()
		{
			base.simpleType = typeof(ShapeDataBindingRule);
		}
	}
}
