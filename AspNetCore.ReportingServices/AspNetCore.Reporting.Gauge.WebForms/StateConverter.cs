namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class StateConverter : CollectionItemTypeConverter
	{
		public StateConverter()
		{
			base.simpleType = typeof(State);
		}
	}
}
