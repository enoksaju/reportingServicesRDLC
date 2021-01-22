namespace AspNetCore.Reporting.Map.WebForms
{
	public interface IDefaultValueProvider
	{
		object GetDefaultValue(string prop, object currentValue);
	}
}
