namespace AspNetCore.Reporting.Map.WebForms
{
	public class SymbolDataBindingRuleConverter : CollectionItemTypeConverter
	{
		public SymbolDataBindingRuleConverter()
		{
			base.simpleType = typeof(SymbolDataBindingRule);
		}
	}
}
