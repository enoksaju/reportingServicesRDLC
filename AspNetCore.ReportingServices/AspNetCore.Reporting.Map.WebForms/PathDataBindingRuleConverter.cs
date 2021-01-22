namespace AspNetCore.Reporting.Map.WebForms
{
	public class PathDataBindingRuleConverter : CollectionItemTypeConverter
	{
		public PathDataBindingRuleConverter()
		{
			base.simpleType = typeof(PathDataBindingRule);
		}
	}
}
