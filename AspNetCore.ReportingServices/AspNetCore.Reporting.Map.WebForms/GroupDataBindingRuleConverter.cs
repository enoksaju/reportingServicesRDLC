namespace AspNetCore.Reporting.Map.WebForms
{
	public class GroupDataBindingRuleConverter : CollectionItemTypeConverter
	{
		public GroupDataBindingRuleConverter()
		{
			base.simpleType = typeof(GroupDataBindingRule);
		}
	}
}
